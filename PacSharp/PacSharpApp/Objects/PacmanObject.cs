﻿using PacSharpApp.Graphics;

namespace PacSharpApp.Objects
{
    class PacmanObject : GameObject
    {
        internal PacmanObject(GraphicsHandler handler)
            : base(GraphicsConstants.SpriteSize)
        {
            handler.UpdateAnimatedSprite(this, new PacmanSprite());
        }
    }
}