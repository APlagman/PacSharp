using System;
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

        protected private Animation(long[] untilNextFrame)
        {
            UntilNextFrame = untilNextFrame;
        }

        internal bool Update(TimeSpan elapsedTime, GraphicsId[,] tiles)
        {
            if (elapsedTime.TotalMilliseconds > UntilNextFrame[CurrentFrame])
            {
                CurrentFrame = (CurrentFrame + 1) % FrameCount;
                UpdateImpl(tiles);
                return true;
            }
            return false;
        }

        protected private void EmptyTiles(GraphicsId[,] tiles)
        {
            for (int row = 0; row < tiles.GetLength(0); ++row)
                for (int col = 0; col < tiles.GetLength(1); ++col)
                    tiles[row, col] = GraphicsId.TileEmpty;
        }

        protected private abstract void UpdateImpl(GraphicsId[,] tiles);
    }
}