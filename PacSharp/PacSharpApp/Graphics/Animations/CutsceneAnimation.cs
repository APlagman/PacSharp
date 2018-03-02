using System;
using System.Collections.Generic;
using PacSharpApp.Objects;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics.Animation
{
    internal class CutsceneAnimation : Animation
    {
        public CutsceneAnimation(GraphicsHandler graphicsHandler)
            : base(graphicsHandler, 0)
        { }

        private protected override bool Repeat => false;
        private protected override int FrameCount => throw new NotImplementedException();

        private protected override void NextFrame(TileCollection tiles, IDictionary<string, GameObject> gameObjects)
        {
            throw new NotImplementedException();
        }
    }
}