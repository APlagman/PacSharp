using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    abstract class Game
    {
        protected private Tile[,] Tiles { get; } = new Tile[36, 28];
        protected internal InputHandler InputHandler { get; private set; }
        protected private GraphicsHandler GraphicsHandler { get; private set; }

        protected private IDictionary<string, GameObject> GameObjects { get; private set; } = new Dictionary<string, GameObject>();
        protected private bool TilesUpdated { get; set; }

        private const int UpMultiplier = -1;
        private const int DownMultiplier = 1;
        
        private TimeSpan accumulatedTime;
        private DateTime previousTime;

        protected internal GameState State { get; protected private set; }
        protected internal bool Paused { get; protected private set; } = true;
        protected internal int Score { get; set; } = 0;

        protected private Game(GameForm owner, Control gameArea)
        {
            GraphicsHandler = new GraphicsHandler(owner, gameArea);
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
            if (TilesUpdated)
                GraphicsHandler.CommitTiles(Tiles);
            GraphicsHandler.Draw(State);
        }

        private void Update(TimeSpan elapsedTime)
        {
            HandleInput();
            if (PreventUpdate)
                return;
            UpdateImpl(elapsedTime);
            InputHandler.Update();
            LogPostUpdate();
        }

        protected private abstract bool PreventUpdate { get; }

        protected private abstract void HandleInput();
        protected private abstract void UpdateImpl(TimeSpan elapsedTime);
        protected private abstract void LogPostUpdate();
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

        protected private abstract void UpdateHighScore();
        protected internal abstract void Reset();
        #endregion
    }
}
