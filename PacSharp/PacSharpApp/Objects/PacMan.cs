using PacSharpApp.Graphics;

namespace PacSharpApp.Objects
{
    class PacMan : GameObject
    {
        internal PacMan(GraphicsHandler handler)
            : base(GraphicsConstants.SpriteSize)
        {
            handler.UpdateAnimatedSprite(this, new PacManSprite());
        }
    }
}
