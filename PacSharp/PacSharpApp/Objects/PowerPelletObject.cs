using System.Drawing;
using PacSharpApp.Graphics;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    class PowerPelletObject : GameObject
    {
        internal const int Worth = 50;

        internal PowerPelletObject(GraphicsHandler handler)
            : base(GraphicsConstants.TileSize)
        {
            handler.UpdateAnimatedSprite(this, new PowerPelletSprite() { Palette = PaletteID.Pellet });
        }

        public RectangleF EdibleBounds => new Rectangle(new Point((int)Position.X - 3, (int)Position.Y - 3), new Size(6, 6));
    }
}
