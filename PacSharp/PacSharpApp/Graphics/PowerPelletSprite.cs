using System;
using System.Collections.Generic;
using System.Drawing;
using PacSharpApp.Properties;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    class PowerPelletSprite : AnimatedSprite
    {
        private static readonly Bitmap sourceSheet = Resources.Tiles;
        private static readonly IDictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]> sourceImages = new Dictionary<string, (Bitmap bitmap, TimeSpan untilUpdate)[]>()
        {
            {
                "flashing",
                new (Bitmap bitmap, TimeSpan untilUpdate)[]
                {
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.TilePelletLarge), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(150)),
                    (sourceSheet.Clone(GraphicsUtils.GetGraphicLocation(GraphicsID.TileEmpty), sourceSheet.PixelFormat), TimeSpan.FromMilliseconds(150))
                }
            }
        };

        internal PowerPelletSprite()
            : base(sourceImages, "flashing")
        { }
    }
}
