using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    abstract class AnimatedSprite : Sprite
    {
        private IDictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]> sources;
        private Bitmap[] images;
        private uint currentImageIndex;
        private string currentAnimationSetID;
        private TimeSpan elapsedTimeThisFrame;

        internal AnimatedSprite(IDictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]> sources, string currentAnimationSet)
        {
            this.sources = sources;
            CurrentAnimationSetID = currentAnimationSet;
            UpdateImagesInUse(true);
        }

        private protected string CurrentAnimationSetID { get => currentAnimationSetID; set { currentAnimationSetID = value; UpdateImagesInUse(true); } }
        internal sealed override Image Image => images[currentImageIndex];
        internal bool AnimationFinished { get; private set; }
        internal bool RepeatAnimation { get; set; } = true;

        private void UpdateImagesInUse(bool resetIndex)
        {
            AnimationFinished = false;
            if (resetIndex)
                currentImageIndex = 0;
            images = new Bitmap[sources[currentAnimationSetID].Length];
            for (int i = 0; i < images.Length; ++i)
            {
                images[i] = sources[currentAnimationSetID][i].bitmap.Clone() as Bitmap;
                GraphicsUtils.SwapColors(images[i], Palette);
            }
            
        }

        private protected sealed override void UpdatePalette()
        {
            UpdateImagesInUse(false);
        }

        protected internal sealed override void Update(TimeSpan elapsedTime)
        {
            if (AnimationFinished && !RepeatAnimation)
                return;
            elapsedTimeThisFrame += elapsedTime;
            if (elapsedTimeThisFrame > sources[CurrentAnimationSetID][currentImageIndex].untilUpdate)
            {
                elapsedTimeThisFrame = new TimeSpan();
                ++currentImageIndex;
                if (currentImageIndex == images.Length)
                {
                    AnimationFinished = true;
                    if (!RepeatAnimation)
                        --currentImageIndex;
                }
                currentImageIndex %= (uint)images.Length;
            }
        }

        internal override void RotateFlip(RotateFlipType rfType)
        {
            foreach (var (bitmap, untilUpdate) in sources[CurrentAnimationSetID])
                bitmap.RotateFlip(rfType);
            foreach (var image in images)
                image.RotateFlip(rfType);
        }
    }
}
