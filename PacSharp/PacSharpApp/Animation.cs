using PacSharpApp.Properties;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    abstract class Animation
    {
        private protected int CurrentFrame { get; set; } = 0;
        private protected long[] UntilNextFrame { get; }
        private int FrameCount => UntilNextFrame.Length;
        private protected abstract bool Repeat { get; }
        internal bool Finished { get; private set; } = false;

        private TimeSpan elapsedTimeThisFrame;

        private protected Animation(long[] untilNextFrame)
        {
            UntilNextFrame = untilNextFrame;
            elapsedTimeThisFrame = new TimeSpan();
        }

        internal bool Update(TimeSpan elapsedTime, Tile[,] tiles, IDictionary<string, GameObject> gameObjects, GraphicsHandler graphicsHandler)
        {
            elapsedTimeThisFrame += elapsedTime;
            if (Finished && !Repeat)
                return false;
            if (elapsedTimeThisFrame.TotalMilliseconds > UntilNextFrame[CurrentFrame])
            {
                elapsedTimeThisFrame -= TimeSpan.FromMilliseconds(UntilNextFrame[CurrentFrame]);
                CurrentFrame = (CurrentFrame + 1) % FrameCount;
                if (CurrentFrame == 0)
                {
                    Finished = true;
                    if (!Repeat)
                        return false;
                }
                UpdateTiles(tiles);
                return true;
            }
            else if (CurrentFrame == 0 && !Finished)
            {
                UpdateTiles(tiles);
                return true;
            }
            return false;
        }

        private protected void EmptyTiles(Tile[,] tiles)
        {
            for (int row = 0; row < tiles.GetLength(0); ++row)
                for (int col = 0; col < tiles.GetLength(1); ++col)
                    tiles[row, col] = new Tile(GraphicsIDs.TileEmpty, PaletteID.Empty);
        }

        private protected abstract void UpdateTiles(Tile[,] tiles);
    }
}