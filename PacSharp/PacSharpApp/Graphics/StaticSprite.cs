using System.Drawing;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    class StaticSprite : Sprite
    {
        private readonly Bitmap sourceImage;
        private Bitmap currentImage;

        internal StaticSprite(Bitmap source, GraphicsID graphicsId)
        {
            sourceImage = source.Clone(GraphicsUtils.GetGraphicLocation(graphicsId), source.PixelFormat);
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
            GraphicsUtils.SwapColors(currentImage, Palette);
        }
    }
}
