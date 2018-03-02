using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PacSharpApp.AI;
using PacSharpApp.Graphics.Animation;
using PacSharpApp.Objects;

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

        private List<(TimeSpan delay, Action action)> actionQueue = new List<(TimeSpan, Action)>();

        internal PacSharpGame(GameUI owner, Control gameArea)
            : base(owner, gameArea)
        { }
        
        private protected override bool PreventUpdate => GameState.Playing == State && Paused;

        private protected override void HandleInput()
        {
            //throw new NotImplementedException();
        }

        private protected override void UpdateImpl(TimeSpan elapsedTime)
        {
            UpdateActionQueue(elapsedTime);
            foreach (var obj in GameObjects)
                obj.Value.Update(elapsedTime);
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

        private void CheckCollisions()
        {
            //throw new NotImplementedException();
        }

        private protected override void OnGameStateChanged()
        {
            base.OnGameStateChanged();
            if (State == GameState.Menu)
                Animation = new MainMenuAnimation(GraphicsHandler, () => BeginMainMenuChase());
        }

        private protected override void UpdateHighScore()
        {
            //throw new NotImplementedException();
        }

        protected internal override void Reset()
        {
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
    }
}
