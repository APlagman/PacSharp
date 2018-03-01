using System;
using System.Collections.Generic;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    abstract class Animation
    {
        private protected GraphicsHandler graphicsHandler;
        private protected int CurrentFrame { get; set; } = 0;
        private protected long UntilNextFrame { get; set; }
        private protected abstract int FrameCount { get; }
        private protected abstract bool Repeat { get; }
        internal bool Finished { get; private set; } = false;

        private TimeSpan elapsedTimeThisFrame;

        private protected Animation(GraphicsHandler graphicsHandler, long untilNextFrame)
        {
            this.graphicsHandler = graphicsHandler;
            UntilNextFrame = untilNextFrame;
            elapsedTimeThisFrame = new TimeSpan();
        }

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
                        return false;
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