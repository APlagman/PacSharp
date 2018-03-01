/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    class Tile
    {
        private bool updated = true;

        internal GraphicsID GraphicsId { get; }
        internal PaletteID Palette { get; }
        internal bool Updated
        {
            get
            {
                if (updated)
                {
                    updated = false;
                    return true;
                }
                return updated;
            }
        }

        public Tile(GraphicsID graphicsId, PaletteID palette)
        {
            GraphicsId = graphicsId;
            Palette = palette;
        }
    }
}