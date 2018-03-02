using System;
using System.Drawing;

namespace PacSharpApp.Graphics
{
    abstract class AnimatedSprite : Sprite
    {
        private TimeSpan elapsedTimeThisFrame;
        private protected readonly Bitmap[] sourceImages;
        private Bitmap[] images;
        private uint currentImage;

        internal AnimatedSprite(Bitmap source, Bitmap[] sourceImages)
            : base(source)
        {
            this.sourceImages = sourceImages;
            images = new Bitmap[sourceImages.Length];
            sourceImages.CopyTo(images, 0);
        }

        internal override Image Image => images[currentImage];
        private protected abstract TimeSpan UntilUpdate { get; }

        private protected override void UpdatePalette()
        {
            for (uint i = 0; i < images.Length; ++i)
            {
                images[i] = sourceImages[i];
                GraphicsHandler.SwapColors(images[i], Palette);
            }
        }

        protected internal sealed override void Update(TimeSpan elapsedTime)
        {
            elapsedTimeThisFrame += elapsedTime;
            if (elapsedTimeThisFrame > UntilUpdate)
            {
                elapsedTimeThisFrame = new TimeSpan();
                ++currentImage;
                currentImage %= (uint)images.Length;
            }
        }
    }
}
