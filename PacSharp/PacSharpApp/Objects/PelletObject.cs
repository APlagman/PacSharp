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
            handler.UpdateStaticSprite(this, GraphicsID.TilePelletSmall, PaletteID.Pellet, Resources.Tiles);
        }
    }
}
