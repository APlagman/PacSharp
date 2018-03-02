using System;
using System.Drawing;

namespace PacSharpApp.Graphics
{
    class AnimatedSprite : Sprite
    {
        private readonly (Bitmap bitmap, TimeSpan untilUpdate)[] sources;
        private Bitmap[] images;
        private uint currentImage;
        private TimeSpan elapsedTimeThisFrame;

        internal AnimatedSprite((Bitmap bitmap, TimeSpan untilUpdate)[] sources)
        {
            this.sources = sources;
            images = new Bitmap[sources.Length];
            for (int i = 0; i < images.Length; ++i)
                images[i] = sources[i].bitmap.Clone() as Bitmap;
        }

        internal sealed override Image Image => images[currentImage];

        private protected sealed override void UpdatePalette()
        {
            for (uint i = 0; i < images.Length; ++i)
            {
                images[i] = sources[i].bitmap.Clone() as Bitmap;
                GraphicsHandler.SwapColors(images[i], Palette);
            }
        }

        protected internal sealed override void Update(TimeSpan elapsedTime)
        {
            elapsedTimeThisFrame += elapsedTime;
            if (elapsedTimeThisFrame > sources[currentImage].untilUpdate)
            {
                elapsedTimeThisFrame = new TimeSpan();
                ++currentImage;
                currentImage %= (uint)images.Length;
            }
        }

        internal override void RotateFlip(RotateFlipType rfType)
        {
            foreach (var (bitmap, untilUpdate) in sources)
                bitmap.RotateFlip(rfType);
            foreach (var image in images)
                image.RotateFlip(rfType);
        }
    }
}
