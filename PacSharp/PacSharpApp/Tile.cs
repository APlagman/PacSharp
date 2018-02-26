using System.Drawing.Imaging;

namespace PacSharpApp
{
    class Tile
    {
        internal GraphicsIDs GraphicsId { get; }
        internal PaletteID Palette { get; }

        public Tile(GraphicsIDs graphicsId, PaletteID palette)
        {
            GraphicsId = graphicsId;
            Palette = palette;
        }
    }
}