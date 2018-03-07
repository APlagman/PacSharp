using System.Drawing;
using PacSharpApp.Graphics;
using PacSharpApp.Properties;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    class PelletObject : GameObject
    {
        internal const int Worth = 10;

        internal PelletObject(GraphicsHandler handler)
            : base(GraphicsConstants.TileSize)
        {
            handler.SetStaticSprite(this, GraphicsID.TilePelletSmall, PaletteID.Pellet, Resources.Tiles, GraphicsConstants.TileWidth);
        }

        public RectangleF EdibleBounds => new RectangleF(new PointF((float)Position.X - 1, (float)Position.Y - 1), new Size(2, 2));
    }
}
