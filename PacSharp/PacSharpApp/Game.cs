using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PacSharpApp
{
    abstract class Game
    {
        private protected GameArea GameArea { get; private set; }
        internal protected InputHandler InputHandler { get; private set; }
        private protected GraphicsHandler GraphicsHandler { get; private set; }

        private protected IDictionary<string, GameObject> GameObjects { get; private set; } = new Dictionary<string, GameObject>();

        private const int UpMultiplier = -1;
        private const int DownMultiplier = 1;
        
        private TimeSpan accumulatedTime;
        private DateTime previousTime;

        internal protected GameState State { get; private protected set; }
        internal protected bool Paused { get; private protected set; } = true;
        internal protected int Score { get; set; } = 0;

        private protected Game(GameForm owner, Control gameArea)
        {
            GameArea = new GameArea(gameArea);
            GraphicsHandler = new GraphicsHandler(owner, GameArea);
            InputHandler = new InputHandler();
        }

        #region Initialization
        internal void Init()
        {
#if DEBUG
            Properties.Settings.Default.Reset();
#endif
            Application.Idle += TickWhileIdle;
        }

        internal void InitGameObject(PictureBox control)
        {
            GameObjects[control.Name] = new GameObject(control.Size);
            GraphicsHandler.Register(GameObjects[control.Name], control);
        }

        void TickWhileIdle(object sender, EventArgs e)
        {
            while (!NativeMethods.PeekMessage(out NativeMethods.Message message, IntPtr.Zero, 0, 0, 0))
            {
                GameTick();
            }
        }
        #endregion

        #region Update Logic
        private void GameTick()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan elapsedTime = currentTime - previousTime;
            previousTime = currentTime;
            Update(elapsedTime);
            GraphicsHandler.Draw(State);
        }

        private void Update(TimeSpan elapsedTime)
        {
            HandleInput();
            if (PreventUpdate)
                return;
            UpdateGameObjects(elapsedTime);
            InputHandler.Update();
            LogPostUpdate();
        }

        private protected abstract bool PreventUpdate { get; }
        private protected abstract void HandleInput();
        private protected abstract void UpdateGameObjects(TimeSpan elapsedTime);
        private protected abstract void LogPostUpdate();
        #endregion

        #region Game State / Initialization
        internal void NewGame()
        {
            Reset();
            Paused = false;
        }

        private void Start()
        {
            GraphicsHandler.OnNewGame();
            accumulatedTime = TimeSpan.Zero;
            previousTime = DateTime.Now;
        }

        internal void TogglePause()
        {
            Paused = !Paused;
        }

        internal void Quit()
        {
            UpdateHighScore();
            GraphicsHandler.Close();
        }

        private protected abstract void UpdateHighScore();
        private protected abstract void Reset();
        #endregion
    }
}
