using System.Drawing;

namespace PacSharpApp.Graphics
{
    class StaticSprite : Sprite
    {
        private Bitmap currentImage;

        internal StaticSprite(Bitmap source, GraphicsID graphicsId)
            : base(source.Clone(GraphicsHandler.GetGraphicLocation(graphicsId), source.PixelFormat))
        {
            currentImage = source;
        }

        internal override Image Image => currentImage;

        private protected override void UpdatePalette()
        {
            currentImage = source;
            GraphicsHandler.SwapColors(currentImage, palette);
        }
    }
}
