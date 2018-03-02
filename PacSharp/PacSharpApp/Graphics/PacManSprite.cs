using System;
using System.Drawing;
using PacSharpApp.Properties;

namespace PacSharpApp.Graphics
{
    class PacManSprite : AnimatedSprite
    {
        private static readonly Bitmap sourceSheet = Resources.Sprites;
        private static readonly (Bitmap, TimeSpan)[] sourceImages = new(Bitmap, TimeSpan)[]
        {
            (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.SpritePacmanOpenRight), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(50)),
            (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.SpritePacmanMiddleRight), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(50)),
            (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.SpritePacmanSolid), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(50)),
            (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.SpritePacmanMiddleRight), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(50))
        };

        internal PacManSprite()
            : base(sourceImages)
        {
            Palette = PaletteID.Pacman;
        }
    }
}
