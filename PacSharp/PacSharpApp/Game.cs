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
        internal static void EmptyTiles(Tile[,] tiles)
        {
            for (int row = 0; row < tiles.GetLength(0); ++row)
                for (int col = 0; col < tiles.GetLength(1); ++col)
                    tiles[row, col] = new Tile(GraphicsID.TileEmpty, PaletteID.Empty);
        }

        private protected Tile[,] Tiles { get; } = new Tile[36, 28];
        protected internal InputHandler InputHandler { get; private set; }
        private protected GraphicsHandler GraphicsHandler { get; private set; }
        private protected IDictionary<string, GameObject> GameObjects { get; private set; } = new Dictionary<string, GameObject>();

        private const int UpMultiplier = -1;
        private const int DownMultiplier = 1;
        
        private TimeSpan accumulatedTime;
        private DateTime previousTime;
        private protected virtual int TargetFPS { get; } = 60;
        private protected virtual TimeSpan MaxElapsedTime => TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 10);
        private protected virtual bool UseFixedTimeStep { get; } = false;
        private TimeSpan TargetElapsedTime => TimeSpan.FromTicks(TimeSpan.TicksPerSecond / TargetFPS);

        private protected bool TilesUpdated
        {
            get
            {
                for (int r = 0; r < Tiles.GetLength(0); ++r)
                    for (int c = 0; c < Tiles.GetLength(1); ++c)
                        if (Tiles[r, c].Updated)
                            return true;
                return false;
            }
        }

        protected internal GameState State { get; private protected set; }
        protected internal bool Paused { get; private protected set; } = true;
        protected internal int Score { get; set; } = 0;

        private protected Game(GameUI owner, Control gameArea)
        {
            GraphicsHandler = new GraphicsHandler(owner, gameArea);
            InputHandler = new InputHandler();
        }

        #region Initialization
        internal void Init()
        {
            previousTime = DateTime.Now;
#if DEBUG
            Properties.Settings.Default.Reset();
#endif
            Application.Idle += TickWhileIdle;
        }

        private protected void InitGameObject(string name, Image source)
        {
            GameObjects.Add(name, new GameObject(source.Size));
            GraphicsHandler.Register(GameObjects[name], source);
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

            bool updated = false;
            if (UseFixedTimeStep)
            {
                if (elapsedTime > MaxElapsedTime)
                    elapsedTime = MaxElapsedTime;
                accumulatedTime += elapsedTime;
                
                while (accumulatedTime >= TargetElapsedTime)
                {
                    Update(elapsedTime);
                    accumulatedTime -= TargetElapsedTime;
                    updated = true;
                }
            }
            else
            {
                Update(elapsedTime);
                updated = true;
            }

            updated &= TilesUpdated;
            if (updated)
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

        private protected abstract bool PreventUpdate { get; }

        private protected abstract void HandleInput();
        private protected abstract void UpdateImpl(TimeSpan elapsedTime);
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
        protected internal abstract void Reset();
        #endregion
    }
}
