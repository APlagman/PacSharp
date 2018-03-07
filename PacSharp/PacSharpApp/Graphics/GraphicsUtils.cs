using PacSharpApp.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    static class GraphicsUtils
    {
        private const int ColorsPerPalette = 4;

        private const PaletteID BasePaletteID = PaletteID.Blinky;
        private static readonly Bitmap BasePalette = GetPalette(BasePaletteID);
        private static readonly IDictionary<int, Color> BasePaletteMap = GetPaletteMap(BasePaletteID);

        private static IDictionary<int, Color> GetPaletteMap(PaletteID id)
        {
            Bitmap palette = GetPalette(id);
            var map = new Dictionary<int, Color>();
            for (int i = 0; i < ColorsPerPalette; ++i)
                map.Add(i, palette.GetPixel(0, i));
            return map;
        }

        internal static Bitmap GetPalette(PaletteID id) => Resources.Palettes.Clone(new Rectangle(new Point((int)id, 0), new Size(1, 4)), Resources.Palettes.PixelFormat);

        internal static void SwapColors(Bitmap source, PaletteID palette)
        {
            if ((int)palette == (int)PaletteID.Blinky || palette == (int)PaletteID.Empty)
                return;
            IDictionary<Color, Color> paletteMap = GetColorMap(palette);
            BitmapData bmpData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite, source.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * source.Height;
            byte[] values = new byte[bytes];
            Marshal.Copy(ptr, values, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                Color src = Color.FromArgb(values[i + 3], values[i + 2], values[i + 1], values[i]);
                Color dest = paletteMap[src];
                values[i + 3] = dest.A;
                values[i + 2] = dest.R;
                values[i + 1] = dest.G;
                values[i] = dest.B;
            }

            Marshal.Copy(values, 0, ptr, bytes);
            source.UnlockBits(bmpData);
        }

        internal static IDictionary<Color, Color> GetColorMap(PaletteID id)
        {
            var results = new Dictionary<Color, Color>();
            var destMap = GetPaletteMap(id);
            for (int i = 0; i < ColorsPerPalette; ++i)
                results.Add(BasePaletteMap[i], destMap[i]);
            results.Add(Color.FromArgb(0, 255, 255, 255), Color.FromArgb(0, 255, 255, 255));

            return results;
        }

        internal static Rectangle GetGraphicSourceRectangle(GraphicsID id, int width, int sourceTilesPerRow)
        {
            return new Rectangle(new Point((int)id % sourceTilesPerRow * width, (int)id / sourceTilesPerRow * width), new Size(width, width));
        }

        internal static GraphicsID TileIDFromLocation(int row, int column)
        {
            return (GraphicsID)(row * 16 + column);
        }
    }
}