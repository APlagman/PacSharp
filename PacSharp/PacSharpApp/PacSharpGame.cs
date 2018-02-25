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

        internal PacSharpGame(GameForm owner, Control gameArea)
            : base(owner, gameArea)
        { }

        private protected override bool PreventUpdate => GameState.Playing == State && Paused;

        private protected override void HandleInput()
        {
            throw new NotImplementedException();
        }

        private protected override void UpdateGameObjects(TimeSpan elapsedTime)
        {
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            throw new NotImplementedException();
        }

        private protected override void UpdateHighScore()
        {
            throw new NotImplementedException();
        }

        private protected override void Reset()
        {
            throw new NotImplementedException();
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
    Location: {GameArea.Location}
    Size: {GameArea.Size}");
            Debug.WriteLine("");
#pragma warning restore CS0162 // Unreachable code detected
        }
    }
}
