using System.Drawing;

namespace PacSharpApp.Graphics
{
    class StaticSprite : Sprite
    {
        private readonly Bitmap sourceImage;
        private Bitmap currentImage;

        internal StaticSprite(Bitmap source, GraphicsID graphicsId)
        {
            sourceImage = source.Clone(GraphicsHandler.GetGraphicLocation(graphicsId), source.PixelFormat);
            currentImage = sourceImage;
        }

        internal override Image Image => currentImage;

        internal override void RotateFlip(RotateFlipType rfType)
        {
            sourceImage.RotateFlip(rfType);
            currentImage.RotateFlip(rfType);
        }

        private protected override void UpdatePalette()
        {
            currentImage = sourceImage;
            GraphicsHandler.SwapColors(currentImage, Palette);
        }
    }
}
