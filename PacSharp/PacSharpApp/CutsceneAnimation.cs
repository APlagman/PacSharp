using System;
using System.Collections.Generic;

namespace PacSharpApp
{
    internal class CutsceneAnimation : Animation
    {
        private static readonly long[] FrameTimings = new long[] { };

        public CutsceneAnimation(GraphicsHandler graphicsHandler)
            : base(graphicsHandler, 0)
        {
        }

        private protected override bool Repeat => false;
        private protected override int FrameCount => throw new NotImplementedException();

        private protected override void NextFrame(Tile[,] tiles, IDictionary<string, GameObject> gameObjects)
        {
            throw new NotImplementedException();
        }
    }
}