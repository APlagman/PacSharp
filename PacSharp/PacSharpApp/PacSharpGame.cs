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

        private int displayedHighScore = 0;
        private List<(TimeSpan delay, Action action)> actionQueue = new List<(TimeSpan, Action)>();

        private ICollection<PelletObject> pellets;
        private ICollection<PowerPelletObject> powerPellets;
        private ISet<GhostObject> ghosts;
        private PacmanObject player;
        private IReadOnlyCollection<RectangleF> walls;

        internal PacSharpGame(IGameUI owner, Control gameArea)
            : base(owner, gameArea)
        { }
        
        private int DisplayedHighScore
        {
            get => displayedHighScore;
            set
            {
                displayedHighScore = value;
                Tiles.DrawInteger(1, 16, displayedHighScore);
            }
        }

        private protected override bool PreventUpdate => false;

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
                    CheckCollisions();
                    break;
                default:
                    break;
            }
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

        private void CheckCollisions()
        {
            CheckForWarp(player);
            WarpIfOffScreen(player);
            PushOutOfWalls(player);
            CheckForEatingPellet(player);
        }

        private void CheckForEatingPellet(PacmanObject pacman)
        {
            CheckForNormalPellets(pacman);
            CheckForPowerPellets(pacman);
        }

        private void CheckForPowerPellets(PacmanObject pacman)
        {
            List<PowerPelletObject> eaten =
                powerPellets.Where(pellet => pacman.MouthBounds.IntersectsWith(pellet.EdibleBounds))
                .ToList();
            foreach (var pellet in eaten)
            {
                Score += PowerPelletObject.Worth;
                // TODO Power Pellet effects
                GraphicsHandler.Unregister(pellet);
                powerPellets.Remove(pellet);
            }
        }

        private void CheckForNormalPellets(PacmanObject pacman)
        {
            List<PelletObject> eaten =
                pellets.Where(pellet => pacman.MouthBounds.IntersectsWith(pellet.EdibleBounds))
                .ToList();
            foreach (var pellet in eaten)
            {
                Score += PelletObject.Worth;
                GraphicsHandler.Unregister(pellet);
                pellets.Remove(pellet);
            }
        }

        private void CheckForWarp(PacmanObject pacman)
        {
            if (OutsideGameArea(pacman) && pacman.State is PacmanMovingState)
            {
                pacman.State = new PacmanWarpingState(pacman);
                actionQueue.Add((TimeSpan.FromMilliseconds(300), () => pacman.State = new PacmanMovingState(pacman)));
            }
        }

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
            if (collided && obj is PacmanObject)
            {
                var pacman = obj as PacmanObject;
                pacman.PerformTurn(
                    Enum.GetValues(typeof(Direction)).Cast<Direction>()
                    .Where(dir => dir != pacman.Orientation && dir != pacman.Orientation.GetOpposite() && PlayerCanTurnTo(PacmanObject.DirectionVelocity(dir))).First());
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
                AddScore();
            }
        }

        private protected override void ResetImpl()
        {
            Score = 0;
            State = GameState.Menu;
            Paused = false;
        }
        
        private protected override void LogPostUpdate()
        {
#pragma warning disable CS0162 // Unreachable code detected
#if !DEBUG
            return;
#endif
            if (!LoggingEnabled)
                return;
#pragma warning restore CS0162 // Unreachable code detected
        }

        private void BeginMainMenuChase()
        {
            GameObjects["EatenPellet"] = new PowerPelletObject(GraphicsHandler)
            {
                Position = Vector2FromTilePosition(4.75, 20)
            };
            actionQueue.Add((TimeSpan.FromMilliseconds(500), () => { GraphicsHandler.PreventAnimatedSpriteUpdates = false; }));
            GameObjects["PacMan"] = new PacmanObject(GraphicsHandler)
            {
                Position = Vector2FromTilePosition(30, 19)
            };
            GraphicsHandler.RotateFlip(GameObjects["PacMan"], RotateFlipType.RotateNoneFlipX);
            GameObjects["PacMan"].Behavior = new MenuPacmanAIBehavior(GameObjects);
        }

        private bool PlayerCanTurnTo(Vector2 velocity)
        {
            Vector2 temp = player.Position;
            player.Position.Round();
            player.Position += velocity;
            bool canTurn = !walls.Any(wall => player.Bounds.IntersectsWith(wall));
            player.Position = temp;
            return canTurn;
        }

        private void AddScore()
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

        private void StartGame()
        {
            State = GameState.Playing;
            Score = 0;
            DisplayedHighScore = 0;

            InitLevel();
            DelayStart();
        }

        private void DelayStart()
        {
            Tiles.DrawText(20, 11, "READY!", PaletteID.Pacman);
            Paused = true;
            actionQueue.Add((TimeSpan.FromSeconds(3), () =>
            {
                GraphicsHandler.PreventAnimatedSpriteUpdates = false;
                Paused = false;
                Tiles.DrawRange((20, 11), (20, 16), GraphicsID.TileEmpty, PaletteID.Empty);
                player.PerformTurn(Direction.Right);
            }));
        }

        private void InitLevel()
        {
            Maze level = Maze.Load(Resources.OriginalMaze);

            CreateLevelObjects(level);
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
            SpawnPlayer(level);
            CreatePellets(level);
            CreatePowerPellets(level);
            SpawnGhosts(level);
            CreateWalls(level);
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
            player = new PacmanObject(GraphicsHandler, PlayerCanTurnTo)
            {
                Position = new Vector2(level.PlayerSpawn.X + GraphicsConstants.TileWidth / 2, level.PlayerSpawn.Y + GraphicsConstants.TileWidth / 2)
            };
        }
    }
}
