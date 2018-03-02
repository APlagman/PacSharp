using PacSharpApp.Graphics;

namespace PacSharpApp.Objects
{
    class PowerPelletObject : GameObject
    {
        internal PowerPelletObject(GraphicsHandler handler)
            : base(GraphicsConstants.TileSize)
        {
            handler.UpdateAnimatedSprite(this, new PowerPelletSprite() { Palette = PaletteID.Pellet });
        }
    }
}
