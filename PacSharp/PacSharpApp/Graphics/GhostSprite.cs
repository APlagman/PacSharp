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
    class GhostSprite : AnimatedSprite, IMultiAnimationSprite
    {
        private const int MillisPerAnimationFrame = 200;

        private static readonly Bitmap sourceSheet = Resources.Sprites;
        private static readonly IDictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]> sourceImages = new Dictionary<string, (Bitmap, TimeSpan)[]>()
        {
            {
                AnimationID.NormalRight.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostRight0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostRight1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            },
            {
                AnimationID.NormalLeft.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostLeft0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostLeft1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            },
            {
                AnimationID.NormalUp.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostUp0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostUp1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            },
            {
                AnimationID.NormalDown.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostDown0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostDown1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            },
            {
                AnimationID.Afraid.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostAfraid0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostAfraid1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            }
        };

        internal GhostSprite()
            : base(sourceImages, AnimationID.NormalRight.ToString())
        {
        }

        internal override int ZIndex => 10;

        public void UpdateAnimationSet(string setID)
        {
            CurrentAnimationSetID = setID;
        }

        private protected override void Turn(Direction value)
        {
            if (CurrentAnimationSetID == AnimationID.Afraid.ToString())
                return;
            switch (value)
            {
                case Direction.Up:
                    UpdateAnimationSet(AnimationID.NormalUp.ToString());
                    break;
                case Direction.Down:
                    UpdateAnimationSet(AnimationID.NormalDown.ToString());
                    break;
                case Direction.Left:
                    UpdateAnimationSet(AnimationID.NormalLeft.ToString());
                    break;
                case Direction.Right:
                    UpdateAnimationSet(AnimationID.NormalRight.ToString());
                    break;
                default:
                    throw new Exception("Unhandled direction.");
            }
        }

        internal enum AnimationID
        {
            NormalUp,
            NormalDown,
            NormalLeft,
            NormalRight,
            Afraid
        }
    }
}
