using System;

namespace PacSharpApp
{
    internal class CutsceneAnimation : Animation
    {
        private static readonly long[] FrameTimings = new long[] { };

        public CutsceneAnimation()
            : base(FrameTimings)
        {
        }

        private protected override bool Repeat => false;

        protected private override void UpdateTiles(Tile[,] tiles)
        {
            throw new NotImplementedException();
        }
    }
}