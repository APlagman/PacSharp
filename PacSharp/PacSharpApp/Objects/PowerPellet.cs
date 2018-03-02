using PacSharpApp.Graphics;

namespace PacSharpApp.Objects
{
    class PowerPellet : GameObject
    {
        internal PowerPellet(GraphicsHandler handler)
            : base(GraphicsHandler.TileSize)
        {
            handler.RegisterAnimatedSprite(this, new PowerPelletSprite() { Palette = PaletteID.Pellet });
        }
    }
}
