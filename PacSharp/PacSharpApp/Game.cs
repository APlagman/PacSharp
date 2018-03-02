using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PacSharpApp.Graphics;
using PacSharpApp.Graphics.Animation;
using PacSharpApp.Objects;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    abstract class Game
    {
        private const int UpMultiplier = -1;
        private const int DownMultiplier = 1;
        
        private TimeSpan accumulatedTime;
        private DateTime previousTime;

        private GameState state;
        private bool resetScheduled = false;
        private int score = 0;

        private protected Game(IGameUI owner, Control gameArea)
        {
            GraphicsHandler = new GraphicsHandler(owner, gameArea);
            InputHandler = new InputHandler();
        }

        private protected TileCollection Tiles { get; } = new TileCollection(28, 36);
        protected internal InputHandler InputHandler { get; private set; }
        private protected GraphicsHandler GraphicsHandler { get; private set; }
        private protected IDictionary<string, GameObject> GameObjects { get; private set; } = new Dictionary<string, GameObject>();
        private protected Animation Animation { get; set; }
        private protected virtual int TargetFPS { get; } = 60;
        private protected virtual TimeSpan MaxElapsedTime => TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 10);
        private protected virtual bool UseFixedTimeStepForUpdates { get; } = false;
        private protected virtual bool UseFixedTimeStepForAnimations { get; } = false;
        private TimeSpan TargetElapsedTime => TimeSpan.FromTicks(TimeSpan.TicksPerSecond / TargetFPS);
        private protected bool TilesUpdated => Tiles.HasBeenUpdated;
        protected internal GameState State
        {
            get => state;
            private protected set
            {
                state = value;
                OnGameStateChanged();
            }
        }

        protected internal bool Paused { get; private protected set; } = true;
        protected internal int Score
        {
            get => score;
            set
            {
                score = value;
                UpdateScore();
            }
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

        private protected void InitGameObject(string name, Sprite source)
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
            if (resetScheduled)
            {
                Reset();
                return;
            }

            bool updated = false;
            if (!UseFixedTimeStepForAnimations)
                UpdateAnimation(elapsedTime);
            if (!UseFixedTimeStepForUpdates)
            {
                Update(elapsedTime);
                updated = true;
            }
            if (UseFixedTimeStepForUpdates || UseFixedTimeStepForAnimations)
            {
                if (elapsedTime > MaxElapsedTime)
                    elapsedTime = MaxElapsedTime;
                accumulatedTime += elapsedTime;
                
                while (accumulatedTime >= TargetElapsedTime)
                {
                    if (UseFixedTimeStepForUpdates)
                        Update(TargetElapsedTime);
                    if (UseFixedTimeStepForAnimations)
                        UpdateAnimation(TargetElapsedTime);
                    accumulatedTime -= TargetElapsedTime;
                    updated = true;
                }
            }
            updated |= TilesUpdated;
            if (updated)
                GraphicsHandler.CommitTiles(Tiles);
            GraphicsHandler.UpdateAnimatedSprites(elapsedTime);
            GraphicsHandler.Draw(State);
        }

        private void UpdateAnimation(TimeSpan elapsedTime)
        {
            Animation?.Update(elapsedTime, Tiles, GameObjects, GraphicsHandler);
        }

        private void Update(TimeSpan elapsedTime)
        {
            InputHandler.Update();
            HandleInput();
            if (PreventUpdate)
                return;
            UpdateImpl(elapsedTime);
            LogPostUpdate();
        }

        private protected abstract bool PreventUpdate { get; }

        private protected abstract void HandleInput();
        private protected abstract void UpdateImpl(TimeSpan elapsedTime);
        private protected abstract void LogPostUpdate();
        #endregion

        #region Game State
        internal void NewGame()
        {
            ResetImpl();
            Paused = false;
        }

        internal void TogglePause()
        {
            Paused = !Paused;
        }

        internal void Quit()
        {
            GraphicsHandler.Close();
        }

        private protected virtual void OnGameStateChanged()
        {
            Tiles.Clear();
            GameObjects.Clear();
            GraphicsHandler.Clear();
            Animation = null;
        }

        private protected virtual void UpdateScore() { }

        internal void ScheduleReset() => resetScheduled = true;

        private void Reset()
        {
            resetScheduled = false;
            ResetImpl();
            UpdateAnimation(new TimeSpan());
            GraphicsHandler.CommitTiles(Tiles);
            GraphicsHandler.Draw(State);
            previousTime = DateTime.Now;
        }

        private protected abstract void ResetImpl();
        #endregion

        internal static Vector2 Vector2FromTilePosition(double x, double y)
        {
            return new Vector2(x * GraphicsConstants.TileWidth, y * GraphicsConstants.TileWidth);
        }
    }
}
