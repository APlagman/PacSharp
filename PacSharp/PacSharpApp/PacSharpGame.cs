using System;
using System.Diagnostics;
using System.Windows.Forms;
using PacSharpApp.Graphics.Animation;

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
            UpdateTiles();
            CheckCollisions();
        }

        private protected override void StartAnimation()
        {
            switch (State)
            {
                case GameState.Menu:
                    Animation = new MainMenuAnimation(GraphicsHandler);
                    break;
                case GameState.Cutscene:
                    Animation = new CutsceneAnimation(GraphicsHandler);
                    break;
                default:
                    break;
            }
        }

        private void UpdateTiles()
        {
            switch (State)
            {   
                case GameState.Highscores:
                    break;
                case GameState.Playing:
                    break;
                default:
                    break;
            }
        private void BeginMainMenuChase()
        {
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
