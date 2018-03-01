using PacSharpApp.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    /// <summary>
    /// Handles drawing logic before delegating to the actual form
    /// </summary>
    class GraphicsHandler
    {
        internal static readonly Size SpriteSize = new Size(SpriteWidth, SpriteWidth);
        internal const int GridWidth = 28;
        internal const int GridHeight = 36;
        internal const int TileWidth = 8;
        internal const int SpriteWidth = 16;
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
        
        private static void SwapColors(Bitmap source, PaletteID palette)
        {
            DateTime temp = DateTime.Now;
            if ((int)palette == (int)PaletteID.Blinky)
                return;
            IDictionary<Color, Color> paletteMap = GetColorMap(palette);
            //for (int x = 0; x < source.Width; ++x)
            //{
            //    for (int y = 0; y < source.Height; ++y)
            //    {
            //        source.SetPixel(x, y, paletteMap[source.GetPixel(x, y)]);
            //    }
            //}
            BitmapData bmpData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite, source.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * source.Height;
            byte[] values = new byte[bytes];
            Marshal.Copy(ptr, values, 0, bytes);

            for (int i = 0; i < bytes; i += 4)
            {
                Color dest = paletteMap[Color.FromArgb(values[i + 2], values[i + 1], values[i])];
                values[i + 2] = dest.R;
                values[i + 1] = dest.G;
                values[i] = dest.B;
            }

            Marshal.Copy(values, 0, ptr, bytes);
            source.UnlockBits(bmpData);
        }

        private static IDictionary<Color, Color> GetColorMap(PaletteID id)
        {
            var results = new Dictionary<Color, Color>();
            var destMap = GetPaletteMap(id);
            for (int i = 0; i < ColorsPerPalette; ++i)
                results.Add(BasePaletteMap[i], destMap[i]);

            return results;
        }

        private static Rectangle GetGraphicLocation(GraphicsID id)
        {
            Point location = new Point();
            int width;
            switch (id)
            {
                case GraphicsID.TileEmpty:
                    location = new Point(15, 15);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile0Bottom:
                    location = new Point(0, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile1Bottom:
                    location = new Point(1, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile2Bottom:
                    location = new Point(2, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile3Bottom:
                    location = new Point(3, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile4Bottom:
                    location = new Point(4, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile5Bottom:
                    location = new Point(5, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile6Bottom:
                    location = new Point(6, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile7Bottom:
                    location = new Point(7, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile8Bottom:
                    location = new Point(8, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile9Bottom:
                    location = new Point(9, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.TileABottom:
                    location = new Point(10, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.TileBBottom:
                    location = new Point(11, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCBottom:
                    location = new Point(12, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.TileDBottom:
                    location = new Point(13, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.TileEBottom:
                    location = new Point(14, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.TileFBottom:
                    location = new Point(15, 0);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePelletSmall:
                    location = new Point(0, 1);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePelletMedium:
                    location = new Point(2, 1);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePelletLarge:
                    location = new Point(4, 1);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePeriod:
                    location = new Point(5, 2);
                    width = TileWidth;
                    break;
                case GraphicsID.TileQuotationMark:
                    location = new Point(6, 2);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile0:
                    location = new Point(0, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile1:
                    location = new Point(1, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile2:
                    location = new Point(2, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile3:
                    location = new Point(3, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile4:
                    location = new Point(4, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile5:
                    location = new Point(5, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile6:
                    location = new Point(6, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile7:
                    location = new Point(7, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile8:
                    location = new Point(8, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.Tile9:
                    location = new Point(9, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.TileForwardSlash:
                    location = new Point(10, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.TileHyphen:
                    location = new Point(11, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCornerTopRight:
                    location = new Point(12, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCornerBottomRight:
                    location = new Point(13, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCornerTopLeft:
                    location = new Point(14, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCornerBottomLeft:
                    location = new Point(15, 3);
                    width = TileWidth;
                    break;
                case GraphicsID.TileA:
                    location = new Point(1, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileB:
                    location = new Point(2, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileC:
                    location = new Point(3, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileD:
                    location = new Point(4, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileE:
                    location = new Point(5, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileF:
                    location = new Point(6, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileG:
                    location = new Point(7, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileH:
                    location = new Point(8, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileI:
                    location = new Point(9, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileJ:
                    location = new Point(10, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileK:
                    location = new Point(11, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileL:
                    location = new Point(12, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileM:
                    location = new Point(13, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileN:
                    location = new Point(14, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileO:
                    location = new Point(15, 4);
                    width = TileWidth;
                    break;
                case GraphicsID.TileP:
                    location = new Point(0, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileQ:
                    location = new Point(1, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileR:
                    location = new Point(2, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileS:
                    location = new Point(3, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileT:
                    location = new Point(4, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileU:
                    location = new Point(5, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileV:
                    location = new Point(6, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileW:
                    location = new Point(7, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileX:
                    location = new Point(8, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileY:
                    location = new Point(9, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileZ:
                    location = new Point(10, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileExclamationMark:
                    location = new Point(11, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCopyright:
                    location = new Point(12, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePts0:
                    location = new Point(13, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePts1:
                    location = new Point(14, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePts2:
                    location = new Point(15, 5);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints10:
                    location = new Point(1, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints30:
                    location = new Point(2, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints50:
                    location = new Point(3, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints70:
                    location = new Point(4, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints00End:
                    location = new Point(5, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints10LeftAligned:
                    location = new Point(6, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints20Left:
                    location = new Point(7, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints20Right:
                    location = new Point(8, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints30Left:
                    location = new Point(9, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints30Right:
                    location = new Point(10, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints50Left:
                    location = new Point(11, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints50Right:
                    location = new Point(12, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints00Left:
                    location = new Point(13, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TilePoints00Right:
                    location = new Point(14, 8);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCherryTopRight:
                    location = new Point(0, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCherryTopLeft:
                    location = new Point(1, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCherryBottomRight:
                    location = new Point(2, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileCherryBottomLeft:
                    location = new Point(3, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileStrawberryTopRight:
                    location = new Point(4, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileStrawberryTopLeft:
                    location = new Point(5, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileStrawberryBottomRight:
                    location = new Point(6, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileStrawberryBottomLeft:
                    location = new Point(7, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileOrangeTopRight:
                    location = new Point(8, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileOrangeTopLeft:
                    location = new Point(9, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileOrangeBottomRight:
                    location = new Point(10, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileOrangeBottomLeft:
                    location = new Point(11, 9);
                    width = TileWidth;
                    break;
                case GraphicsID.TileAppleTopRight:
                    location = new Point(0, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileAppleTopLeft:
                    location = new Point(1, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileAppleBottomRight:
                    location = new Point(2, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileAppleBottomLeft:
                    location = new Point(3, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMelonTopRight:
                    location = new Point(4, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMelonTopLeft:
                    location = new Point(5, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMelonBottomRight:
                    location = new Point(6, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMelonBottomLeft:
                    location = new Point(7, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGalaxianTopRight:
                    location = new Point(8, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGalaxianTopLeft:
                    location = new Point(9, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGalaxianBottomRight:
                    location = new Point(10, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGalaxianBottomLeft:
                    location = new Point(11, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileKeyTopRight:
                    location = new Point(12, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileKeyTopLeft:
                    location = new Point(13, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileKeyBottomRight:
                    location = new Point(14, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileKeyBottomLeft:
                    location = new Point(15, 10);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGhostTopLeft:
                    location = new Point(0, 11);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGhostTopRight:
                    location = new Point(1, 11);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGhostMiddleLeft:
                    location = new Point(2, 11);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGhostMiddleRight:
                    location = new Point(3, 11);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGhostBottomLeft:
                    location = new Point(4, 11);
                    width = TileWidth;
                    break;
                case GraphicsID.TileGhostBottomRight:
                    location = new Point(5, 11);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeElbowTopRight:
                    location = new Point(0, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeElbowTopLeft:
                    location = new Point(1, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeRight:
                    location = new Point(2, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeLeft:
                    location = new Point(3, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeElbowBottomRight:
                    location = new Point(4, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeElbowBottomLeft:
                    location = new Point(5, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeElbowSquareBottomRight:
                    location = new Point(6, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeElbowSquareBottomLeft:
                    location = new Point(7, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeElbowSquareTopRight:
                    location = new Point(8, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeElbowSquareTopLeft:
                    location = new Point(9, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeTop:
                    location = new Point(10, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeBottom:
                    location = new Point(12, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeSolidBottom:
                    location = new Point(14, 13);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCurvedBottomRight:
                    location = new Point(0, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCurvedBottomLeft:
                    location = new Point(1, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCurvedTopRight:
                    location = new Point(2, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCurvedTopLeft:
                    location = new Point(3, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeSolidTop:
                    location = new Point(4, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCornerTopRight:
                    location = new Point(6, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCornerTopLeft:
                    location = new Point(7, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeSolidLeft:
                    location = new Point(8, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeSolidRight:
                    location = new Point(9, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCornerBottomRight:
                    location = new Point(10, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCornerBottomLeft:
                    location = new Point(11, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCornerSquareTopRight:
                    location = new Point(12, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCornerSquareTopLeft:
                    location = new Point(13, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCornerSquareBottomRight:
                    location = new Point(14, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeCornerSquareBottomLeft:
                    location = new Point(15, 14);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeBottomLeft:
                    location = new Point(2, 15);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeBottomRight:
                    location = new Point(3, 15);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeSolidElbowSquareTopLeft:
                    location = new Point(4, 15);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeSolidElbowSquareTopRight:
                    location = new Point(5, 15);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeSolidElbowSquareBottomLeft:
                    location = new Point(10, 15);
                    width = TileWidth;
                    break;
                case GraphicsID.TileMazeSolidElbowSquareBottomRight:
                    location = new Point(11, 15);
                    width = TileWidth;
                    break;
                case GraphicsID.SpriteCherry:
                    location = new Point(0, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteStrawberry:
                    location = new Point(1, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteOrange:
                    location = new Point(2, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteApple:
                    location = new Point(3, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteMelon:
                    location = new Point(5, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGalaxian:
                    location = new Point(6, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteKey:
                    location = new Point(7, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostSpeckedLeft0:
                    location = new Point(0, 1);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostSpeckedLeft1:
                    location = new Point(1, 1);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanTopLeftOpen:
                    location = new Point(0, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanTopRightOpen:
                    location = new Point(1, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanBottomLeftOpen:
                    location = new Point(2, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanBottomRightOpen:
                    location = new Point(3, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanTopLeftMiddle:
                    location = new Point(4, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanTopRightMiddle:
                    location = new Point(5, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanBottomLeftMiddle:
                    location = new Point(6, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanBottomRightMiddle:
                    location = new Point(7, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanTopLeftClosed:
                    location = new Point(0, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanTopRightClosed:
                    location = new Point(1, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanBottomLeftClosed:
                    location = new Point(2, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteLargePacmanBottomRightClosed:
                    location = new Point(3, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostAfraid0:
                    location = new Point(4, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostAfraid1:
                    location = new Point(5, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostRight0:
                    location = new Point(0, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostRight1:
                    location = new Point(1, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostDown0:
                    location = new Point(2, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostDown1:
                    location = new Point(3, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostLeft0:
                    location = new Point(4, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostLeft1:
                    location = new Point(5, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostUp:
                    location = new Point(6, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGhostUp2:
                    location = new Point(7, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePoints200:
                    location = new Point(0, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePoints400:
                    location = new Point(1, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePoints800:
                    location = new Point(2, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePoints1600:
                    location = new Point(3, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanOpenRight:
                    location = new Point(4, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanOpenDown:
                    location = new Point(5, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanMiddleRight:
                    location = new Point(6, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanMiddleDown:
                    location = new Point(7, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanSolid:
                    location = new Point(0, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpriteGlitch:
                    location = new Point(1, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying0:
                    location = new Point(4, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying1:
                    location = new Point(5, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying2:
                    location = new Point(6, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying3:
                    location = new Point(7, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying4:
                    location = new Point(0, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying5:
                    location = new Point(1, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying6:
                    location = new Point(2, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying7:
                    location = new Point(3, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying8:
                    location = new Point(4, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying9:
                    location = new Point(5, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying10:
                    location = new Point(6, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsID.SpritePacmanDying11:
                    location = new Point(7, 7);
                    width = SpriteWidth;
                    break;
                default:
                    throw new Exception("Unhandled GraphicsId");
            }
            return new Rectangle(new Point(location.X * width, location.Y * width), new Size(width, width));
        }

        internal GameArea GameArea { get; }

        private GameUI ui;
        private IDictionary<GameObject, Image> gameObjectMap = new Dictionary<GameObject, Image>();
        private Image tileImage;
        private Image screenImage;

        internal GraphicsHandler(GameUI ui, Control gameArea)
        {
            this.ui = ui;
            GameArea = new GameArea(gameArea, OnPaint);
            screenImage = new Bitmap(GridWidth * TileWidth, GridHeight * TileWidth);
            tileImage = new Bitmap(GridWidth * TileWidth, GridHeight * TileWidth);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (sender is Control control)
                e.Graphics.DrawImage(screenImage, new Rectangle(control.Location, control.Size));
        }

        internal void CommitTiles(Tile[,] tiles)
        {
            using (var tileGraphics = Graphics.FromImage(tileImage))
            {
                tileGraphics.Clear(Color.Black);
                for (int row = 0; row < tiles.GetLength(0); ++row)
                    for (int col = 0; col < tiles.GetLength(1); ++col)
                    {
                        if (!tiles[row, col].Updated)
                            continue;
                        Bitmap source = Resources.Tiles.Clone(GetGraphicLocation(tiles[row, col].GraphicsId), Resources.Tiles.PixelFormat);
                        if (tiles[row, col].Palette != PaletteID.Empty)
                            SwapColors(source, tiles[row, col].Palette);
                        tileGraphics.DrawImage(source, new Point(col * TileWidth, row * TileWidth));
                    }
            }
        }

        internal void UpdateGraphic(GameObject obj, GraphicsID id)
        {
            UpdateGraphic(obj, id, Resources.Sprites);
        }

        internal void UpdateGraphic(GameObject obj, GraphicsID id, Bitmap source)
        {
            gameObjectMap[obj] = source.Clone(GetGraphicLocation(id), source.PixelFormat);
        }

        internal void Draw(GameState state)
        {
            using (var screenGraphics = Graphics.FromImage(screenImage))
            {
                screenGraphics.Clear(Color.Black);
                screenGraphics.DrawImage(tileImage, Point.Empty);
                foreach (var pair in gameObjectMap)
                {
                    GameObject obj = pair.Key;
                    Point location = obj.ScreenPosition(GameArea);
                    screenGraphics.DrawImage(pair.Value, location);
                }
            }
            GameArea.CommitDraw();
        }

        internal void Register(GameObject obj, Image graphics)
        {
            gameObjectMap.Add(obj, graphics);
        }
        
        internal void Close()
        {
            ui.Close();
        }

        internal void OnNewGame()
        {
            ui.OnNewGame();
        }
    }
}
