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
        private const int MillisPerAnimationFrame = 100;
        private static readonly Bitmap sourceSheet = Resources.Sprites;
        private static readonly IDictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]> sourceImages = new Dictionary<string, (Bitmap, TimeSpan)[]>()
        {
            {
                GhostSpriteAnimationSet.NormalRight.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostRight0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostRight1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            },
            {
                GhostSpriteAnimationSet.NormalLeft.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostLeft0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostLeft1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            },
            {
                GhostSpriteAnimationSet.NormalUp.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostUp0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostUp1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            },
            {
                GhostSpriteAnimationSet.NormalDown.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostDown0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostDown1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            },
            {
                GhostSpriteAnimationSet.Afraid.ToString(),
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostAfraid0, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicSourceRectangle(GraphicsID.SpriteGhostAfraid1, GraphicsConstants.SpriteWidth, sourceSheet.Width / GraphicsConstants.SpriteWidth), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(MillisPerAnimationFrame))
                }
            }
        };

        internal GhostSprite()
            : base(sourceImages, GhostSpriteAnimationSet.NormalRight.ToString())
        {
        }

        internal override int ZIndex => 1;

        public void UpdateAnimationSet(string setID)
        {
            CurrentAnimationSet = setID;
        }

        private protected override void Turn(Direction value)
        {
            if (CurrentAnimationSet == GhostSpriteAnimationSet.Afraid.ToString())
                return;
            switch (value)
            {
                case Direction.Up:
                    UpdateAnimationSet(GhostSpriteAnimationSet.NormalUp.ToString());
                    break;
                case Direction.Down:
                    UpdateAnimationSet(GhostSpriteAnimationSet.NormalDown.ToString());
                    break;
                case Direction.Left:
                    UpdateAnimationSet(GhostSpriteAnimationSet.NormalLeft.ToString());
                    break;
                case Direction.Right:
                    UpdateAnimationSet(GhostSpriteAnimationSet.NormalRight.ToString());
                    break;
                default:
                    throw new Exception("Unhandled direction.");
            }
        }
    }

    enum GhostSpriteAnimationSet
    {
        NormalUp,
        NormalDown,
        NormalLeft,
        NormalRight,
        Afraid
    }
}
