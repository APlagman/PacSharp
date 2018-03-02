using PacSharpApp.Graphics;

namespace PacSharpApp.Objects
{
    class PowerPellet : GameObject
    {
        internal PowerPellet(GraphicsHandler handler)
            : base(GraphicsConstants.TileSize)
        {
            handler.UpdateAnimatedSprite(this, new PowerPelletSprite() { Palette = PaletteID.Pellet });
        }
    }
}
