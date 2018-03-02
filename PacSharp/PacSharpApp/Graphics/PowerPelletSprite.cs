using System;
using System.Drawing;
using PacSharpApp.Properties;

namespace PacSharpApp.Graphics
{
    class PowerPelletSprite : AnimatedSprite
    {
        private static readonly Bitmap sourceSheet = Resources.Tiles;
        private static readonly (Bitmap, TimeSpan)[] sourceImages = new (Bitmap, TimeSpan)[]
        {
            (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.TilePelletLarge), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(150)),
            (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.TileEmpty), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(150))
        };

        internal PowerPelletSprite()
            : base(sourceImages)
        { }
    }
}
