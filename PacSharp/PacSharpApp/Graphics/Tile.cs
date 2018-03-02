/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    class Tile
    {
        public Tile(GraphicsID graphicsId, PaletteID palette)
        {
            GraphicsId = graphicsId;
            Palette = palette;
        }

        internal GraphicsID GraphicsId { get; }
        internal PaletteID Palette { get; }
        internal bool Updated { get; set; } = true;
    }
}