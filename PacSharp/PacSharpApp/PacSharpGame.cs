using System;
using System.Diagnostics;
using System.Windows.Forms;

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

        private Animation animation;

        internal PacSharpGame(GameForm owner, Control gameArea)
            : base(owner, gameArea)
        { }

        protected private override bool PreventUpdate => GameState.Playing == State && Paused;

        protected private override void HandleInput()
        {
            //throw new NotImplementedException();
        }

        protected private override void UpdateImpl(TimeSpan elapsedTime)
        {
            if (State == GameState.Cutscene || State == GameState.Menu)
                UpdateAnimation(elapsedTime);
            else
            {
                UpdateTiles();
                CheckCollisions();
            }
        }

        private void UpdateAnimation(TimeSpan elapsedTime)
        {
            if (animation == null)
                StartAnimation();
            if (animation.Update(elapsedTime, Tiles))
                TilesUpdated = true;
        }

        private void StartAnimation()
        {
            switch (State)
            {
                case GameState.Menu:
                    animation = new MainMenuAnimation();
                    break;
                case GameState.Cutscene:
                    animation = new CutsceneAnimation();
                    break;
                default:
                    break;
            }
        }

        private void UpdateTiles()
        {
            TilesUpdated = false;
            switch (State)
            {   
                case GameState.Highscores:
                    break;
                case GameState.Playing:
                    break;
                default:
                    break;
            }
        }

        private void CheckCollisions()
        {
            //throw new NotImplementedException();
        }

        protected private override void UpdateHighScore()
        {
            //throw new NotImplementedException();
        }

        protected internal override void Reset()
        {
            State = GameState.Menu;
            Paused = false;
        }
        
        protected private override void LogPostUpdate()
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
