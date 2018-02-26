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

        protected private override void UpdateImpl(GraphicsId[,] tiles)
        {
            throw new NotImplementedException();
        }
    }
}