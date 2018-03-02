using PacSharpApp.Objects;
using PacSharpApp.Properties;
using PacSharpApp.Utils;
using System;
using System.Collections.Generic;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics.Animation
{
    internal class MainMenuAnimation : Animation
    {
        public MainMenuAnimation(GraphicsHandler graphicsHandler, Action onCompletion)
            : base(graphicsHandler, 1500, onCompletion)
        { }

        private protected override bool Repeat => false;
        private protected override int FrameCount => 14;

        private protected override void NextFrame(Tile[,] tiles, IDictionary<string, GameObject> gameObjects)
        {
            switch (CurrentFrame)
            {
                case 0:
                    graphicsHandler.PauseAnimations = true;
                    gameObjects.Clear();
                    Game.ClearTiles(tiles);
                    tiles[0, 3] = new Tile(GraphicsID.Tile1, PaletteID.Text);
                    tiles[0, 4] = new Tile(GraphicsID.TileU, PaletteID.Text);
                    tiles[0, 5] = new Tile(GraphicsID.TileP, PaletteID.Text);

                    tiles[0, 9] = new Tile(GraphicsID.TileH, PaletteID.Text);
                    tiles[0, 10] = new Tile(GraphicsID.TileI, PaletteID.Text);
                    tiles[0, 11] = new Tile(GraphicsID.TileG, PaletteID.Text);
                    tiles[0, 12] = new Tile(GraphicsID.TileH, PaletteID.Text);
                    tiles[0, 14] = new Tile(GraphicsID.TileS, PaletteID.Text);
                    tiles[0, 15] = new Tile(GraphicsID.TileC, PaletteID.Text);
                    tiles[0, 16] = new Tile(GraphicsID.TileO, PaletteID.Text);
                    tiles[0, 17] = new Tile(GraphicsID.TileR, PaletteID.Text);
                    tiles[0, 18] = new Tile(GraphicsID.TileE, PaletteID.Text);

                    tiles[0, 21] = new Tile(GraphicsID.Tile2, PaletteID.Text);
                    tiles[0, 22] = new Tile(GraphicsID.TileU, PaletteID.Text);
                    tiles[0, 23] = new Tile(GraphicsID.TileP, PaletteID.Text);

                    tiles[1, 5] = new Tile(GraphicsID.Tile0, PaletteID.Text);
                    tiles[1, 6] = new Tile(GraphicsID.Tile0, PaletteID.Text);

                    tiles[4, 7] = new Tile(GraphicsID.TileC, PaletteID.Text);
                    tiles[4, 8] = new Tile(GraphicsID.TileH, PaletteID.Text);
                    tiles[4, 9] = new Tile(GraphicsID.TileA, PaletteID.Text);
                    tiles[4, 10] = new Tile(GraphicsID.TileR, PaletteID.Text);
                    tiles[4, 11] = new Tile(GraphicsID.TileA, PaletteID.Text);
                    tiles[4, 12] = new Tile(GraphicsID.TileC, PaletteID.Text);
                    tiles[4, 13] = new Tile(GraphicsID.TileT, PaletteID.Text);
                    tiles[4, 14] = new Tile(GraphicsID.TileE, PaletteID.Text);
                    tiles[4, 15] = new Tile(GraphicsID.TileR, PaletteID.Text);
                    tiles[4, 17] = new Tile(GraphicsID.TileForwardSlash, PaletteID.Text);
                    tiles[4, 19] = new Tile(GraphicsID.TileN, PaletteID.Text);
                    tiles[4, 20] = new Tile(GraphicsID.TileI, PaletteID.Text);
                    tiles[4, 21] = new Tile(GraphicsID.TileC, PaletteID.Text);
                    tiles[4, 22] = new Tile(GraphicsID.TileK, PaletteID.Text);
                    tiles[4, 23] = new Tile(GraphicsID.TileN, PaletteID.Text);
                    tiles[4, 24] = new Tile(GraphicsID.TileA, PaletteID.Text);
                    tiles[4, 25] = new Tile(GraphicsID.TileM, PaletteID.Text);
                    tiles[4, 26] = new Tile(GraphicsID.TileE, PaletteID.Text);

                    tiles[35, 2] = new Tile(GraphicsID.TileC, PaletteID.Text);
                    tiles[35, 3] = new Tile(GraphicsID.TileR, PaletteID.Text);
                    tiles[35, 4] = new Tile(GraphicsID.TileE, PaletteID.Text);
                    tiles[35, 5] = new Tile(GraphicsID.TileD, PaletteID.Text);
                    tiles[35, 6] = new Tile(GraphicsID.TileI, PaletteID.Text);
                    tiles[35, 7] = new Tile(GraphicsID.TileT, PaletteID.Text);
                    tiles[35, 10] = new Tile(GraphicsID.Tile0, PaletteID.Text);
                    break;
                case 1:
                    UntilNextFrame = 1000;
                    gameObjects["staticBlinky"] = new GameObject(GraphicsHandler.SpriteSize);
                    gameObjects["staticBlinky"].Position = Game.Vector2FromTilePosition(5, 6.5);
                    graphicsHandler.UpdateStaticSprite(gameObjects["staticBlinky"], GraphicsID.SpriteGhostRight0, PaletteID.Blinky);
                    break;
                case 2:
                    UntilNextFrame = 500;
                    tiles[6, 7] = new Tile(GraphicsID.TileHyphen, PaletteID.Blinky);
                    tiles[6, 8] = new Tile(GraphicsID.TileS, PaletteID.Blinky);
                    tiles[6, 9] = new Tile(GraphicsID.TileH, PaletteID.Blinky);
                    tiles[6, 10] = new Tile(GraphicsID.TileA, PaletteID.Blinky);
                    tiles[6, 11] = new Tile(GraphicsID.TileD, PaletteID.Blinky);
                    tiles[6, 12] = new Tile(GraphicsID.TileO, PaletteID.Blinky);
                    tiles[6, 13] = new Tile(GraphicsID.TileW, PaletteID.Blinky);
                    break;
                case 3:
                    UntilNextFrame = 500;
                    tiles[6, 18] = new Tile(GraphicsID.TileQuotationMark, PaletteID.Blinky);
                    tiles[6, 19] = new Tile(GraphicsID.TileB, PaletteID.Blinky);
                    tiles[6, 20] = new Tile(GraphicsID.TileL, PaletteID.Blinky);
                    tiles[6, 21] = new Tile(GraphicsID.TileI, PaletteID.Blinky);
                    tiles[6, 22] = new Tile(GraphicsID.TileN, PaletteID.Blinky);
                    tiles[6, 23] = new Tile(GraphicsID.TileK, PaletteID.Blinky);
                    tiles[6, 24] = new Tile(GraphicsID.TileY, PaletteID.Blinky);
                    tiles[6, 25] = new Tile(GraphicsID.TileQuotationMark, PaletteID.Blinky);
                    break;
                case 4:
                    UntilNextFrame = 1000;
                    gameObjects["staticPinky"] = new GameObject(GraphicsHandler.SpriteSize);
                    gameObjects["staticPinky"].Position = Game.Vector2FromTilePosition(5, 9.5);
                    graphicsHandler.UpdateStaticSprite(gameObjects["staticPinky"], GraphicsID.SpriteGhostRight0, PaletteID.Pinky);
                    break;
                case 5:
                    UntilNextFrame = 500;
                    tiles[9, 7] = new Tile(GraphicsID.TileHyphen, PaletteID.Pinky);
                    tiles[9, 8] = new Tile(GraphicsID.TileS, PaletteID.Pinky);
                    tiles[9, 9] = new Tile(GraphicsID.TileP, PaletteID.Pinky);
                    tiles[9, 10] = new Tile(GraphicsID.TileE, PaletteID.Pinky);
                    tiles[9, 11] = new Tile(GraphicsID.TileE, PaletteID.Pinky);
                    tiles[9, 12] = new Tile(GraphicsID.TileD, PaletteID.Pinky);
                    tiles[9, 13] = new Tile(GraphicsID.TileY, PaletteID.Pinky);
                    break;
                case 6:
                    UntilNextFrame = 500;
                    tiles[9, 18] = new Tile(GraphicsID.TileQuotationMark, PaletteID.Pinky);
                    tiles[9, 19] = new Tile(GraphicsID.TileP, PaletteID.Pinky);
                    tiles[9, 20] = new Tile(GraphicsID.TileI, PaletteID.Pinky);
                    tiles[9, 21] = new Tile(GraphicsID.TileN, PaletteID.Pinky);
                    tiles[9, 22] = new Tile(GraphicsID.TileK, PaletteID.Pinky);
                    tiles[9, 23] = new Tile(GraphicsID.TileY, PaletteID.Pinky);
                    tiles[9, 24] = new Tile(GraphicsID.TileQuotationMark, PaletteID.Pinky);
                    break;
                case 7:
                    UntilNextFrame = 1000;
                    gameObjects["staticInky"] = new GameObject(GraphicsHandler.SpriteSize);
                    gameObjects["staticInky"].Position = Game.Vector2FromTilePosition(5, 12.5);
                    graphicsHandler.UpdateStaticSprite(gameObjects["staticInky"], GraphicsID.SpriteGhostRight0, PaletteID.Inky);
                    break;
                case 8:
                    UntilNextFrame = 500;
                    tiles[12, 7] = new Tile(GraphicsID.TileHyphen, PaletteID.Inky);
                    tiles[12, 8] = new Tile(GraphicsID.TileB, PaletteID.Inky);
                    tiles[12, 9] = new Tile(GraphicsID.TileA, PaletteID.Inky);
                    tiles[12, 10] = new Tile(GraphicsID.TileS, PaletteID.Inky);
                    tiles[12, 11] = new Tile(GraphicsID.TileH, PaletteID.Inky);
                    tiles[12, 12] = new Tile(GraphicsID.TileF, PaletteID.Inky);
                    tiles[12, 13] = new Tile(GraphicsID.TileU, PaletteID.Inky);
                    tiles[12, 14] = new Tile(GraphicsID.TileL, PaletteID.Inky);
                    break;
                case 9:
                    UntilNextFrame = 500;
                    tiles[12, 18] = new Tile(GraphicsID.TileQuotationMark, PaletteID.Inky);
                    tiles[12, 19] = new Tile(GraphicsID.TileI, PaletteID.Inky);
                    tiles[12, 20] = new Tile(GraphicsID.TileN, PaletteID.Inky);
                    tiles[12, 21] = new Tile(GraphicsID.TileK, PaletteID.Inky);
                    tiles[12, 22] = new Tile(GraphicsID.TileY, PaletteID.Inky);
                    tiles[12, 23] = new Tile(GraphicsID.TileQuotationMark, PaletteID.Inky);
                    break;
                case 10:
                    UntilNextFrame = 1000;
                    gameObjects["staticClyde"] = new GameObject(GraphicsHandler.SpriteSize);
                    gameObjects["staticClyde"].Position = Game.Vector2FromTilePosition(5, 15.5);
                    graphicsHandler.UpdateStaticSprite(gameObjects["staticClyde"], GraphicsID.SpriteGhostRight0, PaletteID.Clyde);
                    break;
                case 11:
                    UntilNextFrame = 500;
                    tiles[15, 7] = new Tile(GraphicsID.TileHyphen, PaletteID.Clyde);
                    tiles[15, 8] = new Tile(GraphicsID.TileP, PaletteID.Clyde);
                    tiles[15, 9] = new Tile(GraphicsID.TileO, PaletteID.Clyde);
                    tiles[15, 10] = new Tile(GraphicsID.TileK, PaletteID.Clyde);
                    tiles[15, 11] = new Tile(GraphicsID.TileE, PaletteID.Clyde);
                    tiles[15, 12] = new Tile(GraphicsID.TileY, PaletteID.Clyde);
                    break;
                case 12:
                    UntilNextFrame = 500;
                    tiles[15, 18] = new Tile(GraphicsID.TileQuotationMark, PaletteID.Clyde);
                    tiles[15, 19] = new Tile(GraphicsID.TileC, PaletteID.Clyde);
                    tiles[15, 20] = new Tile(GraphicsID.TileL, PaletteID.Clyde);
                    tiles[15, 21] = new Tile(GraphicsID.TileY, PaletteID.Clyde);
                    tiles[15, 22] = new Tile(GraphicsID.TileD, PaletteID.Clyde);
                    tiles[15, 23] = new Tile(GraphicsID.TileE, PaletteID.Clyde);
                    tiles[15, 24] = new Tile(GraphicsID.TileQuotationMark, PaletteID.Clyde);
                    break;
                case 13:
                    UntilNextFrame = 1000;
                    gameObjects["pelletDemo"] = new GameObject(GraphicsHandler.TileSize);
                    gameObjects["pelletDemo"].Position = Game.Vector2FromTilePosition(10.5, 23.5);
                    graphicsHandler.UpdateStaticSprite(gameObjects["pelletDemo"], GraphicsID.TilePelletSmall, PaletteID.Pellet, Resources.Tiles);
                    tiles[23, 12] = new Tile(GraphicsID.Tile1, PaletteID.Text);
                    tiles[23, 13] = new Tile(GraphicsID.Tile0, PaletteID.Text);
                    tiles[23, 15] = new Tile(GraphicsID.TilePts0, PaletteID.Text);
                    tiles[23, 16] = new Tile(GraphicsID.TilePts1, PaletteID.Text);
                    tiles[23, 17] = new Tile(GraphicsID.TilePts2, PaletteID.Text);

                    gameObjects["powerPelletDemo"] = new PowerPellet(graphicsHandler);
                    gameObjects["powerPelletDemo"].Position = Game.Vector2FromTilePosition(10.5, 25.5);
                    tiles[25, 12] = new Tile(GraphicsID.Tile5, PaletteID.Text);
                    tiles[25, 13] = new Tile(GraphicsID.Tile0, PaletteID.Text);
                    tiles[25, 15] = new Tile(GraphicsID.TilePts0, PaletteID.Text);
                    tiles[25, 16] = new Tile(GraphicsID.TilePts1, PaletteID.Text);
                    tiles[25, 17] = new Tile(GraphicsID.TilePts2, PaletteID.Text);
                    break;
            }
        }
    }
}