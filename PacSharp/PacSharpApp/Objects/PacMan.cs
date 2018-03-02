using PacSharpApp.Graphics;

namespace PacSharpApp.Objects
{
    class PacMan : GameObject
    {
        internal PacMan(GraphicsHandler handler)
            : base(GraphicsHandler.SpriteSize)
        {
            handler.RegisterAnimatedSprite(this, new PacManSprite());
        }
    }
}
