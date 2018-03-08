using System;
using System.Collections.Generic;
using System.Drawing;
using PacSharpApp.Properties;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    class PacmanSprite : AnimatedSprite, IMultiAnimationSprite
    {
        private const int MillisPerEatingAnimationFrame = 50;
        private const int MillisPerDyingAnimationFrame = 100;

        private static readonly Bitmap sourceSheet = Resources.Sprites;
        private static readonly IDictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]> sourceImages = new Dictionary<string, (Bitmap, TimeSpan)[]>()
        {
            {
                AnimationID.MovingRight.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanSolid, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanMiddleRight, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanOpenRight, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanMiddleRight, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame))
                }
            },
            {
                AnimationID.MovingLeft.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanSolid, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat).AfterRotateFlip(RotateFlipType.RotateNoneFlipX), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanMiddleRight, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat).AfterRotateFlip(RotateFlipType.RotateNoneFlipX), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanOpenRight, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat).AfterRotateFlip(RotateFlipType.RotateNoneFlipX), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanMiddleRight, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat).AfterRotateFlip(RotateFlipType.RotateNoneFlipX), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame))
                }
            },
            {
                AnimationID.MovingUp.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanSolid, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat).AfterRotateFlip(RotateFlipType.RotateNoneFlipY), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanMiddleDown, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat).AfterRotateFlip(RotateFlipType.RotateNoneFlipY), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanOpenDown, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat).AfterRotateFlip(RotateFlipType.RotateNoneFlipY), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanMiddleDown, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat).AfterRotateFlip(RotateFlipType.RotateNoneFlipY), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame))
                }
            },
            {
                AnimationID.MovingDown.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanSolid, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanMiddleDown, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanOpenDown, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanMiddleDown, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerEatingAnimationFrame))
                }
            },
            {
                AnimationID.Dying.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame * 3)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying2, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying3, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying4, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying5, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying6, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying7, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying8, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying9, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying10, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpritePacmanDying11, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerDyingAnimationFrame))
                }
            }
        };

        internal void UpdateAnimationSet(object p)
        {
            throw new NotImplementedException();
        }

        internal PacmanSprite()
            : base(sourceImages, AnimationID.MovingRight.ToString())
        {
            Palette = PaletteID.Pacman;
        }

        internal override int ZIndex => 9;

        public void UpdateAnimationSet(string setID)
        {
            CurrentAnimationSetID = setID;
        }

        private protected override void Turn(Direction dir)
        {
            CurrentAnimationSetID = dir.ToPacmanSpriteAnimationID().ToString();
        }

        internal enum AnimationID
        {
            MovingUp,
            MovingDown,
            MovingLeft,
            MovingRight,
            Dying
        }
    }
}
