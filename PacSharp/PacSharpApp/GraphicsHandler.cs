using PacSharpApp.Properties;
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
        private const int TileSourceWidth = 42;
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
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile2Bottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile3Bottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile4Bottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile5Bottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile6Bottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile7Bottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile8Bottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile9Bottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileABottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileBBottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCBottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileDBottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileEBottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileFBottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePelletSmall:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePelletMedium:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePelletLarge:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePeriod:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileQuotationMark:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile0:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile1:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile2:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile3:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile4:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile5:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile6:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile7:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile8:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.Tile9:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileForwardSlash:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileHyphen:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCornerTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCornerBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCornerTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCornerBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileA:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileB:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileC:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileD:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileE:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileF:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileG:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileH:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileI:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileJ:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileK:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileL:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileM:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileN:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileO:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileP:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileQ:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileR:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileS:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileT:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileU:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileV:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileW:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileX:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileY:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileZ:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileExclamationMark:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCopyright:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePts1:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePts2:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePts3:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints10:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints30:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints50:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints70:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints00End:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints10LeftAligned:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints20Left:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints20Right:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints30Left:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints30Right:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints50Left:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints50Right:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints00Left:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TilePoints00Right:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCherryTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCherryTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCherryBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileCherryBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileStrawberryTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileStrawberryTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileStrawberryBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileStrawberryBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileOrangeTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileOrangeTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileOrangeBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileOrangeBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileAppleTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileAppleTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileAppleBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileAppleBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMelonTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMelonTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMelonBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMelonBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGalaxianTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGalaxianTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGalaxianBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGalaxianBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileKeyTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileKeyTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileKeyBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileKeyBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostTopLeft:
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostMiddleLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostMiddleRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileGhostBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowSquareBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowSquareBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowSquareTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeElbowSquareTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeTop:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeBottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidBottom:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCurvedBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCurvedBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCurvedTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCurvedTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidTop:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerSquareTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerSquareTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerSquareBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeCornerSquareBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidElbowSquareTopLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidElbowSquareTopRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidElbowSquareBottomLeft:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.TileMazeSolidElbowSquareBottomRight:
                    location = new Point();
                    width = TileSourceWidth;
                    break;
                case GraphicsId.SpriteCherry:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteStrawberry:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteOrange:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteApple:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteMelon:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGalaxian:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteKey:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostSpeckedLeft0:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostSpeckedLeft1:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopLeftOpen:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopRightOpen:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomLeftOpen:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomRightOpen:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopLeftMiddle:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopRightMiddle:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomLeftMiddle:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomRightMiddle:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopLeftClosed:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanTopRightClosed:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomLeftClosed:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteLargePacmanBottomRightClosed:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostAfraid0:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostAfraid1:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostRight0:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostRight1:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostDown0:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostDown1:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostLeft0:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostLeft1:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostUp:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGhostUp2:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePoints200:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePoints400:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePoints800:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePoints1600:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanOpenRight:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanOpenDown:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanMiddleRight:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanMiddleDown:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanSolid:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpriteGlitch:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying0:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying1:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying2:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying3:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying4:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying5:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying6:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying7:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying8:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying9:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying10:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                case GraphicsId.SpritePacmanDying11:
                    location = new Point();
                    width = SpriteSourceWidth;
                    break;
                default:
                    throw new Exception("Unhandled GraphicsId");
            }
            return new Rectangle(location, new Size(width, width));
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
