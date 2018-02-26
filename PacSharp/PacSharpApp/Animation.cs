using PacSharpApp.Properties;
using System;
using System.Drawing.Imaging;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    abstract class Animation
    {
        protected private int CurrentFrame { get; set; } = 0;
        protected private long[] UntilNextFrame { get; }
        private int FrameCount => UntilNextFrame.Length;
        protected private abstract bool Repeat { get; }
        internal bool Finished { get; private set; } = false;

        protected private Animation(long[] untilNextFrame)
        {
            UntilNextFrame = untilNextFrame;
        }

        internal bool Update(TimeSpan elapsedTime, Tile[,] tiles)
        {
            if (Finished)
                return false;
            if (elapsedTime.TotalMilliseconds > UntilNextFrame[CurrentFrame])
            {
                CurrentFrame = (CurrentFrame + 1) % FrameCount;
                if (CurrentFrame == 0 && !Repeat)
                {
                    Finished = true;
                    return false;
                }
                UpdateTiles(tiles);
                return true;
            }
            return false;
        }

        protected private void EmptyTiles(Tile[,] tiles)
        {
            for (int row = 0; row < tiles.GetLength(0); ++row)
                for (int col = 0; col < tiles.GetLength(1); ++col)
                    tiles[row, col] = new Tile(GraphicsIDs.TileEmpty, PaletteID.Empty);
        }

        protected private abstract void UpdateTiles(Tile[,] tiles);
    }
}