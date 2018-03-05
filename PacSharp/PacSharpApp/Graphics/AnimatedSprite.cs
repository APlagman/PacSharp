using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    class AnimatedSprite : Sprite
    {
        private readonly IDictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]> sources;
        private Bitmap[] images;
        private uint currentImage;
        private TimeSpan elapsedTimeThisFrame;

        internal AnimatedSprite(IDictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]> sources, string currentAnimationSet)
        {
            this.sources = sources;
            CurrentAnimationSet = currentAnimationSet;
            images = new Bitmap[sources[currentAnimationSet].Length];
            for (int i = 0; i < images.Length; ++i)
                images[i] = sources[currentAnimationSet][i].bitmap.Clone() as Bitmap;
        }

        internal string CurrentAnimationSet { private get; set; }
        internal sealed override Image Image => images[currentImage];

        private protected sealed override void UpdatePalette()
        {
            for (uint i = 0; i < images.Length; ++i)
            {
                images[i] = sources[CurrentAnimationSet][i].bitmap.Clone() as Bitmap;
                GraphicsUtils.SwapColors(images[i], Palette);
            }
        }

        protected internal sealed override void Update(TimeSpan elapsedTime)
        {
            elapsedTimeThisFrame += elapsedTime;
            if (elapsedTimeThisFrame > sources[CurrentAnimationSet][currentImage].untilUpdate)
            {
                elapsedTimeThisFrame = new TimeSpan();
                ++currentImage;
                currentImage %= (uint)images.Length;
            }
        }

        internal override void RotateFlip(RotateFlipType rfType)
        {
            foreach (var (bitmap, untilUpdate) in sources[CurrentAnimationSet])
                bitmap.RotateFlip(rfType);
            foreach (var image in images)
                image.RotateFlip(rfType);
        }
    }
}
