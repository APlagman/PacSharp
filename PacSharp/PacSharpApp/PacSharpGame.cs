using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<PelletObject> pellets;
        private List<PowerPelletObject> powerPellets;
        private List<GhostObject> ghosts;
        private PacmanObject player;

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
                    {

                    }
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
            //throw new NotImplementedException();
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
            Debug.WriteLine(
$@"Game Area:
    Location: {GraphicsHandler.GameArea.Location}
    Size: {GraphicsHandler.GameArea.Size}");
            Debug.WriteLine("");
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

            Maze level = Maze.Load(Resources.OriginalMaze);

            CreateLevelObjects(level);
            Tiles.DrawRange((4, 0), (33, 27), GraphicsID.TileEmpty, PaletteID.Empty);
            GraphicsHandler.CommitTiles(Tiles);
            level.Draw(Tiles);
            Tiles.DrawText(20, 11, "READY!", PaletteID.Pacman);
            Paused = true;
            actionQueue.Add((TimeSpan.FromSeconds(3), () =>
            {
                GraphicsHandler.PreventAnimatedSpriteUpdates = false;
                Paused = false;
                Tiles.DrawRange((20, 11), (20, 16), GraphicsID.TileEmpty, PaletteID.Empty);
            }));
        }

        private void CreateLevelObjects(Maze level)
        {
            player = new PacmanObject(GraphicsHandler)
            {
                Position = new Vector2(level.PlayerSpawn.X + GraphicsConstants.TileWidth / 2, level.PlayerSpawn.Y + GraphicsConstants.TileWidth / 2)
            };
            pellets = new List<PelletObject>
                (level.Pellets.Select(position => new PelletObject(GraphicsHandler)
                {
                    Position = new Vector2(position.X + GraphicsConstants.TileWidth / 2, position.Y + GraphicsConstants.TileWidth / 2)
                }));
            powerPellets = new List<PowerPelletObject>
                (level.PowerPellets.Select(position => new PowerPelletObject(GraphicsHandler)
                {
                    Position = new Vector2(position.X + GraphicsConstants.TileWidth / 2, position.Y + GraphicsConstants.TileWidth / 2)
                }));
            ghosts = new List<GhostObject>
                (level.GhostSpawns.Select(spawn => new GhostObject(GraphicsHandler)
                {
                    Position = new Vector2(spawn.Value.X + GraphicsConstants.TileWidth / 2, spawn.Value.Y + GraphicsConstants.TileWidth / 2)
                }));
        }

        private void AddPellets()
        {

        }

        private void AddGhosts()
        {

        }
    }
}
