using System.Drawing.Imaging;

namespace PacSharpApp
{
    class Tile
    {
        internal GraphicsID GraphicsId { get; }
        internal PaletteID Palette { get; }

        public Tile(GraphicsID graphicsId, PaletteID palette)
        {
            GraphicsId = graphicsId;
            Palette = palette;
        }
    }
}