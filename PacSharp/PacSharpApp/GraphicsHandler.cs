using System;
using System.Collections.Generic;
using System.Drawing;
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
        private const int TileSourceWidth = 8;
        private const int SpriteSourceWidth = 16;

        private static Rectangle GetGraphicLocation(GraphicsId id)
        {
            Point location = new Point();
            int width;
            switch (id)
            {
                case GraphicsId.Tile0Bottom:
                    location = new Point(0, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile1Bottom:
                    location = new Point(1, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile2Bottom:
                    location = new Point(2, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile3Bottom:
                    location = new Point(3, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile4Bottom:
                    location = new Point(4, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile5Bottom:
                    location = new Point(5, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile6Bottom:
                    location = new Point(6, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile7Bottom:
                    location = new Point(7, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile8Bottom:
                    location = new Point(8, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile9Bottom:
                    location = new Point(9, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileABottom:
                    location = new Point(10, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileBBottom:
                    location = new Point(11, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCBottom:
                    location = new Point(12, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileDBottom:
                    location = new Point(13, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileEBottom:
                    location = new Point(14, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileFBottom:
                    location = new Point(15, 0);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePelletSmall:
                    location = new Point(0, 1);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePelletMedium:
                    location = new Point(2, 1);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePelletLarge:
                    location = new Point(4, 1);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePeriod:
                    location = new Point(5, 2);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileQuotationMark:
                    location = new Point(6, 2);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile0:
                    location = new Point(0, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile1:
                    location = new Point(1, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile2:
                    location = new Point(2, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile3:
                    location = new Point(3, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile4:
                    location = new Point(4, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile5:
                    location = new Point(5, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile6:
                    location = new Point(6, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile7:
                    location = new Point(7, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile8:
                    location = new Point(8, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile9:
                    location = new Point(9, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileForwardSlash:
                    location = new Point(10, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileHyphen:
                    location = new Point(11, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCornerTopRight:
                    location = new Point(12, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCornerBottomRight:
                    location = new Point(13, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCornerTopLeft:
                    location = new Point(14, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCornerBottomLeft:
                    location = new Point(15, 3);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileA:
                    location = new Point(1, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileB:
                    location = new Point(2, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileC:
                    location = new Point(3, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileD:
                    location = new Point(4, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileE:
                    location = new Point(5, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileF:
                    location = new Point(6, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileG:
                    location = new Point(7, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileH:
                    location = new Point(8, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileI:
                    location = new Point(9, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileJ:
                    location = new Point(10, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileK:
                    location = new Point(11, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileL:
                    location = new Point(12, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileM:
                    location = new Point(13, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileN:
                    location = new Point(14, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileO:
                    location = new Point(15, 4);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileP:
                    location = new Point(0, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileQ:
                    location = new Point(1, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileR:
                    location = new Point(2, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileS:
                    location = new Point(3, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileT:
                    location = new Point(4, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileU:
                    location = new Point(5, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileV:
                    location = new Point(6, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileW:
                    location = new Point(7, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileX:
                    location = new Point(8, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileY:
                    location = new Point(9, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileZ:
                    location = new Point(10, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileExclamationMark:
                    location = new Point(11, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCopyright:
                    location = new Point(12, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePts0:
                    location = new Point(13, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePts1:
                    location = new Point(14, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePts2:
                    location = new Point(15, 5);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints10:
                    location = new Point(1, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints30:
                    location = new Point(2, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints50:
                    location = new Point(3, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints70:
                    location = new Point(4, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints00End:
                    location = new Point(5, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints10LeftAligned:
                    location = new Point(6, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints20Left:
                    location = new Point(7, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints20Right:
                    location = new Point(8, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints30Left:
                    location = new Point(9, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints30Right:
                    location = new Point(10, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints50Left:
                    location = new Point(11, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints50Right:
                    location = new Point(12, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints00Left:
                    location = new Point(13, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints00Right:
                    location = new Point(14, 8);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCherryTopRight:
                    location = new Point(0, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCherryTopLeft:
                    location = new Point(1, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCherryBottomRight:
                    location = new Point(2, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCherryBottomLeft:
                    location = new Point(3, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileStrawberryTopRight:
                    location = new Point(4, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileStrawberryTopLeft:
                    location = new Point(5, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileStrawberryBottomRight:
                    location = new Point(6, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileStrawberryBottomLeft:
                    location = new Point(7, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileOrangeTopRight:
                    location = new Point(8, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileOrangeTopLeft:
                    location = new Point(9, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileOrangeBottomRight:
                    location = new Point(10, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileOrangeBottomLeft:
                    location = new Point(11, 9);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileAppleTopRight:
                    location = new Point(0, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileAppleTopLeft:
                    location = new Point(1, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileAppleBottomRight:
                    location = new Point(2, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileAppleBottomLeft:
                    location = new Point(3, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMelonTopRight:
                    location = new Point(4, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMelonTopLeft:
                    location = new Point(5, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMelonBottomRight:
                    location = new Point(6, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMelonBottomLeft:
                    location = new Point(7, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGalaxianTopRight:
                    location = new Point(8, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGalaxianTopLeft:
                    location = new Point(9, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGalaxianBottomRight:
                    location = new Point(10, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGalaxianBottomLeft:
                    location = new Point(11, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileKeyTopRight:
                    location = new Point(12, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileKeyTopLeft:
                    location = new Point(13, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileKeyBottomRight:
                    location = new Point(14, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileKeyBottomLeft:
                    location = new Point(15, 10);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostTopLeft:
                    location = new Point(0, 11);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostTopRight:
                    location = new Point(1, 11);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostMiddleLeft:
                    location = new Point(2, 11);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostMiddleRight:
                    location = new Point(3, 11);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostBottomLeft:
                    location = new Point(4, 11);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostBottomRight:
                    location = new Point(5, 11);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowTopRight:
                    location = new Point(0, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowTopLeft:
                    location = new Point(1, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeRight:
                    location = new Point(2, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeLeft:
                    location = new Point(3, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowBottomRight:
                    location = new Point(4, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowBottomLeft:
                    location = new Point(5, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowSquareBottomRight:
                    location = new Point(6, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowSquareBottomLeft:
                    location = new Point(7, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowSquareTopRight:
                    location = new Point(8, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowSquareTopLeft:
                    location = new Point(9, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeTop:
                    location = new Point(10, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeBottom:
                    location = new Point(12, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidBottom:
                    location = new Point(14, 13);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCurvedBottomRight:
                    location = new Point(0, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCurvedBottomLeft:
                    location = new Point(1, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCurvedTopRight:
                    location = new Point(2, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCurvedTopLeft:
                    location = new Point(3, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidTop:
                    location = new Point(4, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerTopRight:
                    location = new Point(6, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerTopLeft:
                    location = new Point(7, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidLeft:
                    location = new Point(8, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidRight:
                    location = new Point(9, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerBottomRight:
                    location = new Point(10, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerBottomLeft:
                    location = new Point(11, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerSquareTopRight:
                    location = new Point(12, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerSquareTopLeft:
                    location = new Point(13, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerSquareBottomRight:
                    location = new Point(14, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerSquareBottomLeft:
                    location = new Point(15, 14);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeBottomLeft:
                    location = new Point(2, 15);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeBottomRight:
                    location = new Point(3, 15);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidElbowSquareTopLeft:
                    location = new Point(4, 15);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidElbowSquareTopRight:
                    location = new Point(5, 15);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidElbowSquareBottomLeft:
                    location = new Point(10, 15);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidElbowSquareBottomRight:
                    location = new Point(11, 15);
                    width = TileSourceWidth;
                    break;
                case GraphicsId.SpriteCherry:
                    location = new Point(0, 0);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteStrawberry:
                    location = new Point(1, 0);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteOrange:
                    location = new Point(2, 0);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteApple:
                    location = new Point(3, 0);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteMelon:
                    location = new Point(5, 0);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGalaxian:
                    location = new Point(6, 0);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteKey:
                    location = new Point(7, 0);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostSpeckedLeft0:
                    location = new Point(0, 1);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostSpeckedLeft1:
                    location = new Point(1, 1);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopLeftOpen:
                    location = new Point(0, 2);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopRightOpen:
                    location = new Point(1, 2);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomLeftOpen:
                    location = new Point(2, 2);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomRightOpen:
                    location = new Point(3, 2);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopLeftMiddle:
                    location = new Point(4, 2);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopRightMiddle:
                    location = new Point(5, 2);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomLeftMiddle:
                    location = new Point(6, 2);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomRightMiddle:
                    location = new Point(7, 2);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopLeftClosed:
                    location = new Point(0, 3);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopRightClosed:
                    location = new Point(1, 3);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomLeftClosed:
                    location = new Point(2, 3);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomRightClosed:
                    location = new Point(3, 3);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostAfraid0:
                    location = new Point(4, 3);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostAfraid1:
                    location = new Point(5, 3);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostRight0:
                    location = new Point(0, 4);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostRight1:
                    location = new Point(1, 4);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostDown0:
                    location = new Point(2, 4);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostDown1:
                    location = new Point(3, 4);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostLeft0:
                    location = new Point(4, 4);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostLeft1:
                    location = new Point(5, 4);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostUp:
                    location = new Point(6, 4);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostUp2:
                    location = new Point(7, 4);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePoints200:
                    location = new Point(0, 5);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePoints400:
                    location = new Point(1, 5);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePoints800:
                    location = new Point(2, 5);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePoints1600:
                    location = new Point(3, 5);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanOpenRight:
                    location = new Point(4, 5);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanOpenDown:
                    location = new Point(5, 5);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanMiddleRight:
                    location = new Point(6, 5);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanMiddleDown:
                    location = new Point(7, 5);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanSolid:
                    location = new Point(0, 6);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGlitch:
                    location = new Point(1, 6);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying0:
                    location = new Point(4, 6);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying1:
                    location = new Point(5, 6);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying2:
                    location = new Point(6, 6);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying3:
                    location = new Point(7, 6);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying4:
                    location = new Point(0, 7);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying5:
                    location = new Point(1, 7);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying6:
                    location = new Point(2, 7);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying7:
                    location = new Point(3, 7);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying8:
                    location = new Point(4, 7);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying9:
                    location = new Point(5, 7);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying10:
                    location = new Point(6, 7);
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying11:
                    location = new Point(7, 7);
                    width = SpriteSourceWidth;
                    break;
                default:
                    throw new Exception("Unhandled GraphicsId");
            }
            return new Rectangle(new Point(location.X * width, location.Y * width), new Size(width, width));
        }

        private GameForm ui;
        private GameArea gameArea;
        private Dictionary<GameObject, PictureBox> gameObjectMap = new Dictionary<GameObject, PictureBox>();

        internal GraphicsHandler(GameForm ui, GameArea gameArea)
        {
            this.ui = ui;
            this.gameArea = gameArea;
        }

        internal void UpdateGraphic(GameObject obj, GraphicsId id, Bitmap source)
        {
            gameObjectMap[obj].Image = source.Clone(GetGraphicLocation(id), System.Drawing.Imaging.PixelFormat.DontCare);
        }

        internal void Draw(GameState state)
        {
            foreach (var goPair in gameObjectMap)
                UpdateGameObjectGraphics(goPair);
            UpdateUI(state);
        }

        private void UpdateUI(GameState state)
        {
            if (ui.InvokeRequired)
                ui.Invoke((MethodInvoker)delegate { UpdateUI(state); });
            else
            {
                ui.UpdateControls(state);
                ui.Invalidate();
            }
        }

        private void UpdateGameObjectGraphics(KeyValuePair<GameObject, PictureBox> goPair)
        {
            Control graphic = goPair.Value;
            if (graphic.InvokeRequired)
                graphic.Invoke((MethodInvoker)delegate { UpdateGameObjectGraphics(goPair); });
            else
            {
                GameObject obj = goPair.Key;
                graphic.UpdateLocation(obj, gameArea);
            }
        }

        internal void Register(GameObject obj, PictureBox graphics)
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
