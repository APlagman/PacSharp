using System;
using System.Collections.Generic;
using PacSharpApp.Objects;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics.Animation
{
    abstract class Animation
    {
        private protected readonly GraphicsHandler graphicsHandler;
        private readonly Action onCompletion;
        private TimeSpan elapsedTimeThisFrame;

        private protected Animation(GraphicsHandler graphicsHandler, long untilNextFrame, Action onCompletion = null)
        {
            this.graphicsHandler = graphicsHandler;
            this.onCompletion = onCompletion;
            UntilNextFrame = untilNextFrame;
            elapsedTimeThisFrame = new TimeSpan();
        }
        private protected int CurrentFrame { get; set; } = 0;
        private protected long UntilNextFrame { get; set; }
        private protected abstract int FrameCount { get; }
        private protected abstract bool Repeat { get; }
        internal bool Finished { get; private set; } = false;

        internal bool Update(TimeSpan elapsedTime, Tile[,] tiles, IDictionary<string, GameObject> gameObjects, GraphicsHandler graphicsHandler)
        {
            elapsedTimeThisFrame += elapsedTime;
            if (Finished && !Repeat)
                return false;
            if (elapsedTimeThisFrame.TotalMilliseconds > UntilNextFrame)
            {
                elapsedTimeThisFrame = new TimeSpan();
                CurrentFrame = (CurrentFrame + 1) % FrameCount;
                if (CurrentFrame == 0)
                {
                    Finished = true;
                    if (!Repeat)
                    {
                        onCompletion();
                        return false;
                    }
                }
                NextFrame(tiles, gameObjects);
                return true;
            }
            else if (CurrentFrame == 0 && !Finished)
            {
                NextFrame(tiles, gameObjects);
                return true;
            }
            return false;
        }

        private protected abstract void NextFrame(Tile[,] tiles, IDictionary<string, GameObject> gameObjects);
    }
}