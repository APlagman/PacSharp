using System;
using System.Collections.Generic;
using System.Drawing;
using PacSharpApp.Properties;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    class PacmanSprite : AnimatedSprite
    {
        private static readonly Bitmap sourceSheet = Resources.Sprites;
        private static readonly IDictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]> sourceImages = new Dictionary<string, (Bitmap, TimeSpan)[]>()
        {
            {
                "moving",
                new (Bitmap, TimeSpan)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.SpritePacmanSolid), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(50)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.SpritePacmanMiddleRight), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(50)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.SpritePacmanOpenRight), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(50)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.SpritePacmanMiddleRight), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(50))
                }
            }
        };

        internal PacmanSprite()
            : base(sourceImages, "moving")
        {
            Palette = PaletteID.Pacman;
        }

        internal override int ZIndex => 9;
    }
}
