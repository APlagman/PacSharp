using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PacSharpApp.AI;
using PacSharpApp.Graphics;
using PacSharpApp.Graphics.Animation;
using PacSharpApp.Objects;
using PacSharpApp.Properties;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    /// <summary>
    /// Handles game logic
    /// </summary>
    sealed class PacSharpGame : Game
    {
        private const bool LoggingEnabled = false;
        private const double PlayerMovementSpeed = 1.0;
        private static readonly TimeSpan EatGhostPauseDuration = TimeSpan.FromSeconds(1);
        private static readonly TimeSpan VictoryPauseDuration = TimeSpan.FromMilliseconds(300);
        private static readonly TimeSpan WarpMovementDisabledDuration = TimeSpan.FromMilliseconds(300);
        private static readonly TimeSpan MainMenuAnimationsDisabledDuration = TimeSpan.FromMilliseconds(500);
        private static readonly TimeSpan InitialLevelPauseDuration = TimeSpan.FromSeconds(3);

        private int livesRemaining = 0;
        private int ghostsEaten = 0;
        private int levelNumber = 0;
        private int displayedHighScore = 0;
        private List<(TimeSpan delay, Action action)> actionQueue = new List<(TimeSpan, Action)>();

        private Maze level;
        private ICollection<PelletObject> pellets;
        private ICollection<PowerPelletObject> powerPellets;
        private ISet<GhostObject> ghosts;
        private PacmanObject player;
        private IReadOnlyCollection<RectangleF> walls;

        private bool victoryAlreadyReached = false;

        internal PacSharpGame(IGameUI owner, Control gameArea)
            : base(owner, gameArea)
        { }
        
        private int DisplayedHighScore
        {
            get => displayedHighScore;
            set { displayedHighScore = value; UpdateHighScore(); }
        }

        private protected override bool PreventUpdate => false;

        private bool VictoryConditionReached => pellets.Count == 0 && powerPellets.Count == 0;

        private int GhostScore => 200 << ghostsEaten;

        private protected override void HandleInput()
        {
            if (InputHandler.HeldKeys.Contains(Keys.R) && InputHandler.HeldKeys.Contains(Keys.ControlKey))
            {
                ScheduleReset();
                return;
            }
            switch (State)
            {
                case GameState.Menu:
                    {
                        if (InputHandler.HeldKeys.Contains(Keys.Enter))
                        {
                            StartGame();
                            break;
                        }
                    }
                    break;
                case GameState.Playing:
                    {
                        if (InputHandler.PressedKeys.Contains(Keys.P))
                            Paused = !Paused;
                        if (!Paused)
                            player.HandleInput(InputHandler);
                    }
                    break;
                default:
                    break;
            }
        }

        #region Update Logic
        private protected override void UpdateImpl(TimeSpan elapsedTime)
        {
            UpdateActionQueue(elapsedTime);
            UpdateObjects(elapsedTime);
            switch (State)
            {
                case GameState.Menu:
                    break;
                case GameState.Highscores:
                    break;
                case GameState.Cutscene:
                    break;
                case GameState.Playing:
                    HandleCollisions();
                    if (VictoryConditionReached && !victoryAlreadyReached)
                    {
                        victoryAlreadyReached = true;
                        actionQueue.Add((VictoryPauseDuration, () => StartNextLevel()));
                    }
                    break;
                default:
                    break;
            }
        }

        private void UpdateActionQueue(TimeSpan elapsedTime)
        {
            var finished = new List<(TimeSpan delay, Action action)>(
                actionQueue
                .FindAll(delayedAction => delayedAction.delay <= elapsedTime));
            foreach (var (delay, action) in finished)
                action();
            actionQueue = new List<(TimeSpan delay, Action action)>(
                actionQueue
                .FindAll(delayedAction => delayedAction.delay > elapsedTime)
                .Select(delayedAction => (delayedAction.delay - elapsedTime, delayedAction.action)));
        }

        private void UpdateObjects(TimeSpan elapsedTime)
        {
            foreach (var obj in GameObjects)
                obj.Value.Update(elapsedTime);
            if (State == GameState.Playing)
            {
                if (!Paused)
                    player?.Update(elapsedTime);
                foreach (var ghosts in ghosts)
                    ghosts.Update(elapsedTime);
                foreach (var pellet in pellets)
                    pellet.Update(elapsedTime);
                foreach (var pellet in powerPellets)
                    pellet.Update(elapsedTime);
            }
        }
        #endregion

        #region Collision
        private void HandleCollisions()
        {
            if (ShouldBeginWarping(player))
                HandleWarpBeginning(player);
            WarpIfOffScreen(player);
            PushOutOfWalls(player);
            HandleEatingPellets(player);
            HandleTouchingGhosts(player);
        }

        private void HandleTouchingGhosts(PacmanObject obj)
        {
            if (victoryAlreadyReached || !(obj.State is PacmanMovingState))
                return;
            bool playerDied = false;
            foreach (var touchedGhost in ghosts.Where(ghost => ghost.Bounds.IntersectsWith(obj.MouthBounds)))
            {
                if (touchedGhost.IsAfraid)
                    HandleGhostEaten(touchedGhost);
                else if (touchedGhost.IsNormal)
                {
                    if (livesRemaining > 0)
                    {
                        obj.State = new PacmanRespawningState(obj, RespawnPlayer);
                        --livesRemaining;
                    }
                    else
                    {
                        obj.State = new PacmanDyingState(obj);
                    }

                    playerDied = true;
                    break;
                }
            }
            if (playerDied)
            {
                foreach (var ghost in ghosts)
                    GraphicsHandler.Unregister(ghost);
                ghosts.Clear();
            }
        }

        private void HandleGhostEaten(GhostObject eaten)
        {
            Score += GhostScore;
            eaten.State = new RespawningGhostState(eaten);
            DisableMovement();
            ++ghostsEaten;
            actionQueue.Add((EatGhostPauseDuration, () =>
            {
                EnableMovement();
            }
            ));
        }

        private void HandleEatingPellets(PacmanObject obj)
        {
            HandleEatingNormalPellets(obj);
            HandleEatingPowerPellets(obj);
        }

        private void HandleEatingPowerPellets(PacmanObject obj)
        {
            List<PowerPelletObject> eaten =
                powerPellets.Where(pellet => obj.MouthBounds.IntersectsWith(pellet.EdibleBounds))
                .ToList();
            foreach (var pellet in eaten)
            {
                Score += PowerPelletObject.Worth;
                BeginPowerPelletEffects();
                GraphicsHandler.Unregister(pellet);
                powerPellets.Remove(pellet);
            }
        }

        private void HandleEatingNormalPellets(PacmanObject obj)
        {
            List<PelletObject> eaten =
                pellets.Where(pellet => obj.MouthBounds.IntersectsWith(pellet.EdibleBounds))
                .ToList();
            foreach (var pellet in eaten)
            {
                Score += PelletObject.Worth;
                GraphicsHandler.Unregister(pellet);
                pellets.Remove(pellet);
            }
        }

        private void HandleWarpBeginning(PacmanObject obj)
        {
            obj.State = new PacmanWarpingState(obj);
            actionQueue.Add((WarpMovementDisabledDuration, () => obj.State = new PacmanMovingState(obj)));
        }

        private bool ShouldBeginWarping(PacmanObject obj) => OutsideGameArea(obj) && obj.State is PacmanMovingState;

        private bool OutsideGameArea(GameObject obj)
        {
            return obj.RightSideRightOf(GraphicsHandler.GameArea.Right)
                || obj.LeftSideLeftOf(GraphicsHandler.GameArea.Left)
                || obj.TopAbove(GraphicsHandler.GameArea.Top)
                || obj.BottomBelow(GraphicsHandler.GameArea.Bottom);
        }

        private void PushOutOfWalls(GameObject obj)
        {
            bool collided = false;
            foreach (RectangleF wall in walls.Where(wall => obj.Bounds.IntersectsWith(wall)))
            {
                if (obj.CollidingFromAbove(wall))
                    obj.Position.Y -= (obj.Bottom - wall.Top);
                if (obj.CollidingFromBelow(wall))
                    obj.Position.Y -= (obj.Top - wall.Bottom);
                if (obj.CollidingFromLeft(wall))
                    obj.Position.X -= (obj.Right - wall.Left);
                if (obj.CollidingFromRight(wall))
                    obj.Position.X -= (obj.Left - wall.Right);
                collided = true;
            }
            TurnPlayerOnCollision(obj, collided);
        }
        #endregion Collision

        #region Gameplay Effects
        private void TurnPlayerOnCollision(GameObject obj, bool collided)
        {
            if (collided && obj is PacmanObject)
            {
                var pacman = obj as PacmanObject;
                pacman.PerformTurn(
                    Enum.GetValues(typeof(Direction)).Cast<Direction?>()
                    .Where(dir => dir != pacman.Orientation && dir != pacman.Orientation.GetOpposite() && player.CanTurnTo(PacmanObject.DirectionVelocity(dir.Value))).FirstOrDefault());
            }
        }

        private void WarpIfOffScreen(GameObject obj)
        {
            if (obj.Right < GraphicsHandler.GameArea.Left)
            {
                obj.Position = new Vector2(GraphicsHandler.GameArea.Right + obj.Size.Width / 2, obj.Position.Y);
            }
            if (obj.Left > GraphicsHandler.GameArea.Right)
            {
                obj.Position = new Vector2(GraphicsHandler.GameArea.Left - obj.Size.Width / 2, obj.Position.Y);
            }
            if (obj.Top > GraphicsHandler.GameArea.Bottom)
            {
                obj.Position = new Vector2(obj.Position.X, GraphicsHandler.GameArea.Top - obj.Size.Width / 2);
            }
            if (obj.Bottom < GraphicsHandler.GameArea.Top)
            {
                obj.Position = new Vector2(obj.Position.X, GraphicsHandler.GameArea.Bottom + obj.Size.Width / 2);
            }
        }

        private void StartNextLevel()
        {
            ++levelNumber;
            Paused = true;
            foreach (var ghost in ghosts)
                GraphicsHandler.Unregister(ghost);
            ghosts.Clear();
            GraphicsHandler.Unregister(player);
            player.PerformTurn(Direction.Right);
            player = null;
            InitLevel();
            DelayStart();
            victoryAlreadyReached = false;
        }

        private void RespawnPlayer()
        {
            GraphicsHandler.Unregister(player);
            SpawnPlayer(level);
            SpawnGhosts(level);
            DisableMovement();
            DelayStart();
        }

        private void BeginPowerPelletEffects()
        {
            foreach (var ghost in ghosts)
                if (!ghost.IsRespawning)
                    ghost.State = new AfraidGhostState(ghost);
            ghostsEaten = 0;
        }

        private void DisableMovement()
        {
            player.PreventMovement = true;
            foreach (var ghost in ghosts)
                ghost.PreventMovement = true;
        }

        private void EnableMovement()
        {
            player.PreventMovement = false;
            foreach (var ghost in ghosts)
                ghost.PreventMovement = false;
        }
        #endregion

        #region Game State
        private protected override void OnGameStateChanged()
        {
            base.OnGameStateChanged();
            if (State == GameState.Menu)
            {
                Score = 0;
                Animation = new MainMenuAnimation(GraphicsHandler, () => BeginMainMenuChase());
                AddCredit();
            }
            if (State == GameState.Menu || State == GameState.Playing)
            {
                AddTopScreenInfo();
            }
        }

        private protected override void ResetImpl()
        {
            Score = 0;
            State = GameState.Menu;
            Paused = false;
        }

        private void BeginMainMenuChase()
        {
            GameObjects["EatenPellet"] = new PowerPelletObject(GraphicsHandler)
            {
                Position = Vector2FromTilePosition(4.75, 20)
            };
            actionQueue.Add((MainMenuAnimationsDisabledDuration, () => { GraphicsHandler.PreventAnimatedSpriteUpdates = false; }));
            GameObjects["PacMan"] = new PacmanObject(GraphicsHandler, null)
            {
                Position = Vector2FromTilePosition(30, 19)
            };
            GraphicsHandler.RotateFlip(GameObjects["PacMan"], RotateFlipType.RotateNoneFlipX);
            GameObjects["PacMan"].Behavior = new MenuPacmanAIBehavior(GameObjects);
        }
        #endregion

        #region Tile Updates
        private void AddTopScreenInfo()
        {
            Tiles.DrawText(0, 3, "1UP");
            Tiles.DrawText(0, 9, "HIGHSCORE");
            Tiles.DrawText(0, 21, "2UP");
        }

        private void AddCredit()
        {
            Tiles.DrawText(35, 2, "CREDIT  0");
        }

        private protected override void UpdateScore()
        {
            Tiles.DrawInteger(1, 6, Score);
        }

        private void UpdateHighScore()
        {
            Tiles.DrawInteger(1, 16, displayedHighScore);
        }
        #endregion

        #region Game Start
        private void StartGame()
        {
            livesRemaining = 3;
            victoryAlreadyReached = false;
            levelNumber = 0;
            State = GameState.Playing;
            Score = 0;
            DisplayedHighScore = 0;

            InitLevel();
            DelayStart();
        }

        private void DelayStart()
        {
            Tiles.DrawText(20, 11, "READY!", PaletteID.Pacman);
            actionQueue.Add((InitialLevelPauseDuration, () =>
            {
                player.PreventMovement = false;
                foreach (var ghost in ghosts)
                    ghost.PreventMovement = false;
                GraphicsHandler.PreventAnimatedSpriteUpdates = false;
                Tiles.DrawRange((20, 11), (20, 16), GraphicsID.TileEmpty, PaletteID.Empty);
                player.PerformTurn(Direction.Right);
            }));
        }
        #endregion

        #region Level Loading
        private void InitLevel()
        {
            level = Maze.Load(Resources.OriginalMaze);

            CreateLevelObjects(level);
            DisableMovement();
            DrawLevel(level);
        }

        private void DrawLevel(Maze level)
        {
            Tiles.DrawRange((4, 0), (35, 27), GraphicsID.TileEmpty, PaletteID.Empty);
            GraphicsHandler.CommitTiles(Tiles);
            level.Draw(Tiles);
        }

        private void CreateLevelObjects(Maze level)
        {
            // Must create walls before spawning player due to turn conditions
            CreateWalls(level);
            CreatePellets(level);
            CreatePowerPellets(level);
            SpawnPlayer(level);
            SpawnGhosts(level);
        }

        private void CreateWalls(Maze level)
        {
            walls = level.Walls;
        }

        private void SpawnGhosts(Maze level)
        {
            ghosts = new HashSet<GhostObject>
                (level.GhostSpawns.Select(spawn => new GhostObject(GraphicsHandler, spawn.Key, player)
                {
                    Position = new Vector2(spawn.Value.X + GraphicsConstants.TileWidth / 2, spawn.Value.Y + GraphicsConstants.TileWidth / 2)
                }));
        }

        private void CreatePowerPellets(Maze level)
        {
            powerPellets = new List<PowerPelletObject>
                (level.PowerPellets.Select(position => new PowerPelletObject(GraphicsHandler)
                {
                    Position = new Vector2(position.X + GraphicsConstants.TileWidth / 2, position.Y + GraphicsConstants.TileWidth / 2)
                }));
        }

        private void CreatePellets(Maze level)
        {
            pellets = new List<PelletObject>
                (level.Pellets.Select(position => new PelletObject(GraphicsHandler)
                {
                    Position = new Vector2(position.X + GraphicsConstants.TileWidth / 2, position.Y + GraphicsConstants.TileWidth / 2)
                }));
        }

        private void SpawnPlayer(Maze level)
        {
            player = new PacmanObject(GraphicsHandler, walls)
            {
                Position = new Vector2(level.PlayerSpawn.X + GraphicsConstants.TileWidth / 2, level.PlayerSpawn.Y + GraphicsConstants.TileWidth / 2)
            };
        }
        #endregion
    }
}
