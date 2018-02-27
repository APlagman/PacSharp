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


        private static Rectangle GetGraphicLocation(GraphicsIDs id)
        {
            Point location = new Point();
            int width;
            switch (id)
            {
                case GraphicsIDs.TileEmpty:
                    location = new Point(15, 15);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile0Bottom:
                    location = new Point(0, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile1Bottom:
                    location = new Point(1, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile2Bottom:
                    location = new Point(2, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile3Bottom:
                    location = new Point(3, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile4Bottom:
                    location = new Point(4, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile5Bottom:
                    location = new Point(5, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile6Bottom:
                    location = new Point(6, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile7Bottom:
                    location = new Point(7, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile8Bottom:
                    location = new Point(8, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile9Bottom:
                    location = new Point(9, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileABottom:
                    location = new Point(10, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileBBottom:
                    location = new Point(11, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCBottom:
                    location = new Point(12, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileDBottom:
                    location = new Point(13, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileEBottom:
                    location = new Point(14, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileFBottom:
                    location = new Point(15, 0);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePelletSmall:
                    location = new Point(0, 1);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePelletMedium:
                    location = new Point(2, 1);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePelletLarge:
                    location = new Point(4, 1);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePeriod:
                    location = new Point(5, 2);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileQuotationMark:
                    location = new Point(6, 2);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile0:
                    location = new Point(0, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile1:
                    location = new Point(1, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile2:
                    location = new Point(2, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile3:
                    location = new Point(3, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile4:
                    location = new Point(4, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile5:
                    location = new Point(5, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile6:
                    location = new Point(6, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile7:
                    location = new Point(7, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile8:
                    location = new Point(8, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.Tile9:
                    location = new Point(9, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileForwardSlash:
                    location = new Point(10, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileHyphen:
                    location = new Point(11, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCornerTopRight:
                    location = new Point(12, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCornerBottomRight:
                    location = new Point(13, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCornerTopLeft:
                    location = new Point(14, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCornerBottomLeft:
                    location = new Point(15, 3);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileA:
                    location = new Point(1, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileB:
                    location = new Point(2, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileC:
                    location = new Point(3, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileD:
                    location = new Point(4, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileE:
                    location = new Point(5, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileF:
                    location = new Point(6, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileG:
                    location = new Point(7, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileH:
                    location = new Point(8, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileI:
                    location = new Point(9, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileJ:
                    location = new Point(10, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileK:
                    location = new Point(11, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileL:
                    location = new Point(12, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileM:
                    location = new Point(13, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileN:
                    location = new Point(14, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileO:
                    location = new Point(15, 4);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileP:
                    location = new Point(0, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileQ:
                    location = new Point(1, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileR:
                    location = new Point(2, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileS:
                    location = new Point(3, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileT:
                    location = new Point(4, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileU:
                    location = new Point(5, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileV:
                    location = new Point(6, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileW:
                    location = new Point(7, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileX:
                    location = new Point(8, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileY:
                    location = new Point(9, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileZ:
                    location = new Point(10, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileExclamationMark:
                    location = new Point(11, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCopyright:
                    location = new Point(12, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePts0:
                    location = new Point(13, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePts1:
                    location = new Point(14, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePts2:
                    location = new Point(15, 5);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints10:
                    location = new Point(1, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints30:
                    location = new Point(2, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints50:
                    location = new Point(3, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints70:
                    location = new Point(4, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints00End:
                    location = new Point(5, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints10LeftAligned:
                    location = new Point(6, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints20Left:
                    location = new Point(7, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints20Right:
                    location = new Point(8, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints30Left:
                    location = new Point(9, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints30Right:
                    location = new Point(10, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints50Left:
                    location = new Point(11, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints50Right:
                    location = new Point(12, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints00Left:
                    location = new Point(13, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TilePoints00Right:
                    location = new Point(14, 8);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCherryTopRight:
                    location = new Point(0, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCherryTopLeft:
                    location = new Point(1, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCherryBottomRight:
                    location = new Point(2, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileCherryBottomLeft:
                    location = new Point(3, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileStrawberryTopRight:
                    location = new Point(4, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileStrawberryTopLeft:
                    location = new Point(5, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileStrawberryBottomRight:
                    location = new Point(6, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileStrawberryBottomLeft:
                    location = new Point(7, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileOrangeTopRight:
                    location = new Point(8, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileOrangeTopLeft:
                    location = new Point(9, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileOrangeBottomRight:
                    location = new Point(10, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileOrangeBottomLeft:
                    location = new Point(11, 9);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileAppleTopRight:
                    location = new Point(0, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileAppleTopLeft:
                    location = new Point(1, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileAppleBottomRight:
                    location = new Point(2, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileAppleBottomLeft:
                    location = new Point(3, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMelonTopRight:
                    location = new Point(4, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMelonTopLeft:
                    location = new Point(5, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMelonBottomRight:
                    location = new Point(6, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMelonBottomLeft:
                    location = new Point(7, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGalaxianTopRight:
                    location = new Point(8, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGalaxianTopLeft:
                    location = new Point(9, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGalaxianBottomRight:
                    location = new Point(10, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGalaxianBottomLeft:
                    location = new Point(11, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileKeyTopRight:
                    location = new Point(12, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileKeyTopLeft:
                    location = new Point(13, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileKeyBottomRight:
                    location = new Point(14, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileKeyBottomLeft:
                    location = new Point(15, 10);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGhostTopLeft:
                    location = new Point(0, 11);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGhostTopRight:
                    location = new Point(1, 11);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGhostMiddleLeft:
                    location = new Point(2, 11);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGhostMiddleRight:
                    location = new Point(3, 11);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGhostBottomLeft:
                    location = new Point(4, 11);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileGhostBottomRight:
                    location = new Point(5, 11);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeElbowTopRight:
                    location = new Point(0, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeElbowTopLeft:
                    location = new Point(1, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeRight:
                    location = new Point(2, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeLeft:
                    location = new Point(3, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeElbowBottomRight:
                    location = new Point(4, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeElbowBottomLeft:
                    location = new Point(5, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeElbowSquareBottomRight:
                    location = new Point(6, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeElbowSquareBottomLeft:
                    location = new Point(7, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeElbowSquareTopRight:
                    location = new Point(8, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeElbowSquareTopLeft:
                    location = new Point(9, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeTop:
                    location = new Point(10, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeBottom:
                    location = new Point(12, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeSolidBottom:
                    location = new Point(14, 13);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCurvedBottomRight:
                    location = new Point(0, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCurvedBottomLeft:
                    location = new Point(1, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCurvedTopRight:
                    location = new Point(2, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCurvedTopLeft:
                    location = new Point(3, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeSolidTop:
                    location = new Point(4, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCornerTopRight:
                    location = new Point(6, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCornerTopLeft:
                    location = new Point(7, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeSolidLeft:
                    location = new Point(8, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeSolidRight:
                    location = new Point(9, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCornerBottomRight:
                    location = new Point(10, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCornerBottomLeft:
                    location = new Point(11, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCornerSquareTopRight:
                    location = new Point(12, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCornerSquareTopLeft:
                    location = new Point(13, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCornerSquareBottomRight:
                    location = new Point(14, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeCornerSquareBottomLeft:
                    location = new Point(15, 14);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeBottomLeft:
                    location = new Point(2, 15);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeBottomRight:
                    location = new Point(3, 15);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeSolidElbowSquareTopLeft:
                    location = new Point(4, 15);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeSolidElbowSquareTopRight:
                    location = new Point(5, 15);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeSolidElbowSquareBottomLeft:
                    location = new Point(10, 15);
                    width = TileWidth;
                    break;
                case GraphicsIDs.TileMazeSolidElbowSquareBottomRight:
                    location = new Point(11, 15);
                    width = TileWidth;
                    break;
                case GraphicsIDs.SpriteCherry:
                    location = new Point(0, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteStrawberry:
                    location = new Point(1, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteOrange:
                    location = new Point(2, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteApple:
                    location = new Point(3, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteMelon:
                    location = new Point(5, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGalaxian:
                    location = new Point(6, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteKey:
                    location = new Point(7, 0);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostSpeckedLeft0:
                    location = new Point(0, 1);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostSpeckedLeft1:
                    location = new Point(1, 1);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanTopLeftOpen:
                    location = new Point(0, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanTopRightOpen:
                    location = new Point(1, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanBottomLeftOpen:
                    location = new Point(2, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanBottomRightOpen:
                    location = new Point(3, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanTopLeftMiddle:
                    location = new Point(4, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanTopRightMiddle:
                    location = new Point(5, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanBottomLeftMiddle:
                    location = new Point(6, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanBottomRightMiddle:
                    location = new Point(7, 2);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanTopLeftClosed:
                    location = new Point(0, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanTopRightClosed:
                    location = new Point(1, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanBottomLeftClosed:
                    location = new Point(2, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteLargePacmanBottomRightClosed:
                    location = new Point(3, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostAfraid0:
                    location = new Point(4, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostAfraid1:
                    location = new Point(5, 3);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostRight0:
                    location = new Point(0, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostRight1:
                    location = new Point(1, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostDown0:
                    location = new Point(2, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostDown1:
                    location = new Point(3, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostLeft0:
                    location = new Point(4, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostLeft1:
                    location = new Point(5, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostUp:
                    location = new Point(6, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGhostUp2:
                    location = new Point(7, 4);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePoints200:
                    location = new Point(0, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePoints400:
                    location = new Point(1, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePoints800:
                    location = new Point(2, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePoints1600:
                    location = new Point(3, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanOpenRight:
                    location = new Point(4, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanOpenDown:
                    location = new Point(5, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanMiddleRight:
                    location = new Point(6, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanMiddleDown:
                    location = new Point(7, 5);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanSolid:
                    location = new Point(0, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpriteGlitch:
                    location = new Point(1, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying0:
                    location = new Point(4, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying1:
                    location = new Point(5, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying2:
                    location = new Point(6, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying3:
                    location = new Point(7, 6);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying4:
                    location = new Point(0, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying5:
                    location = new Point(1, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying6:
                    location = new Point(2, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying7:
                    location = new Point(3, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying8:
                    location = new Point(4, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying9:
                    location = new Point(5, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying10:
                    location = new Point(6, 7);
                    width = SpriteWidth;
                    break;
                case GraphicsIDs.SpritePacmanDying11:
                    location = new Point(7, 7);
                    width = SpriteWidth;
                    break;
                default:
                    throw new Exception("Unhandled GraphicsId");
            }
            return new Rectangle(new Point(location.X * width, location.Y * width), new Size(width, width));
        }

        internal GameArea GameArea { get; }

        private GameForm ui;
        private IDictionary<GameObject, Image> gameObjectMap = new Dictionary<GameObject, Image>();
        private Image tileImage;
        private Image screenImage;

        internal GraphicsHandler(GameForm ui, Control gameArea)
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
                        Bitmap source = Resources.Tiles.Clone(GetGraphicLocation(tiles[row, col].GraphicsId), Resources.Tiles.PixelFormat);
                        if (tiles[row, col].Palette != PaletteID.Empty)
                            SwapColors(source, tiles[row, col].Palette);
                        tileGraphics.DrawImage(source, new Point(col * TileWidth, row * TileWidth));
                    }
            }
        }

        internal void UpdateGraphic(GameObject obj, GraphicsIDs id, Bitmap source)
        {
            gameObjectMap[obj] = source.Clone(GetGraphicLocation(id), source.PixelFormat);
        }

        internal void Draw(GameState state)
        {
            using (var screenGraphics = Graphics.FromImage(screenImage))
            {
                screenGraphics.Clear(Color.Black);
                screenGraphics.DrawImage(tileImage, Point.Empty);
                //foreach (var pair in gameObjectMap)
                //{
                //    GameObject obj = pair.Key;
                //    Point location = obj.ScreenPosition(GameArea);
                //    screenGraphics.DrawImage(pair.Value, location);
                //}
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
