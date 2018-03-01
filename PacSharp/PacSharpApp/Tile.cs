/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    class Tile
    {
        internal GraphicsID GraphicsId { get; }
        internal PaletteID Palette { get; }
        internal bool Updated { get; set; } = true;

        public Tile(GraphicsID graphicsId, PaletteID palette)
        {
            GraphicsId = graphicsId;
            Palette = palette;
        }
    }
}