using System.Drawing;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    static class GraphicsConstants
    {
        internal const int GridWidth = 28;
        internal const int GridHeight = 36;
        internal const int TileWidth = 8;
        internal const int SpriteWidth = 16;
        internal static readonly Size SpriteSize = new Size(SpriteWidth, SpriteWidth);
        internal static readonly Size TileSize = new Size(TileWidth, TileWidth);
        internal static readonly Size GridAreaSize = new Size(GridWidth * TileWidth, GridHeight * TileWidth);
    }
}
