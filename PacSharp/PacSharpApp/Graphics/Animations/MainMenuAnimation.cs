using PacSharpApp.Objects;
using PacSharpApp.Properties;
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
            : base(graphicsHandler, 500, onCompletion)
        { }

        private protected override bool Repeat => false;
        private protected override int FrameCount => 14;

        private protected override void NextFrame(Tile[,] tiles, IDictionary<string, GameObject> gameObjects)
        {
            switch (CurrentFrame)
            {
                case 0:
                    graphicsHandler.PreventAnimatedSpriteUpdates = true;
                    tiles.DrawText(4, 7, "CHARACTER / NICKNAME");
                    break;
                case 1:
                    UntilNextFrame = 1000;
                    gameObjects["staticBlinky"] = new GameObject(GraphicsConstants.SpriteSize)
                    {
                        Position = Game.Vector2FromTilePosition(5, 6.5)
                    };
                    graphicsHandler.UpdateStaticSprite(gameObjects["staticBlinky"], GraphicsID.SpriteGhostRight0, PaletteID.Blinky);
                    break;
                case 2:
                    UntilNextFrame = 500;
                    tiles.DrawText(6, 7, "-SHADOW", PaletteID.Blinky);
                    break;
                case 3:
                    UntilNextFrame = 500;
                    tiles.DrawText(6, 18, "\"BLINKY\"", PaletteID.Blinky);
                    break;
                case 4:
                    UntilNextFrame = 1000;
                    gameObjects["staticPinky"] = new GameObject(GraphicsConstants.SpriteSize)
                    {
                        Position = Game.Vector2FromTilePosition(5, 9.5)
                    };
                    graphicsHandler.UpdateStaticSprite(gameObjects["staticPinky"], GraphicsID.SpriteGhostRight0, PaletteID.Pinky);
                    break;
                case 5:
                    UntilNextFrame = 500;
                    tiles.DrawText(9, 7, "-SPEEDY", PaletteID.Pinky);
                    break;
                case 6:
                    UntilNextFrame = 500;
                    tiles.DrawText(9, 18, "\"PINKY\"", PaletteID.Pinky);
                    break;
                case 7:
                    UntilNextFrame = 1000;
                    gameObjects["staticInky"] = new GameObject(GraphicsConstants.SpriteSize)
                    {
                        Position = Game.Vector2FromTilePosition(5, 12.5)
                    };
                    graphicsHandler.UpdateStaticSprite(gameObjects["staticInky"], GraphicsID.SpriteGhostRight0, PaletteID.Inky);
                    break;
                case 8:
                    UntilNextFrame = 500;
                    tiles.DrawText(12, 7, "-BASHFUL", PaletteID.Inky);
                    break;
                case 9:
                    UntilNextFrame = 500;
                    tiles.DrawText(12, 18, "\"INKY\"", PaletteID.Inky);
                    break;
                case 10:
                    UntilNextFrame = 1000;
                    gameObjects["staticClyde"] = new GameObject(GraphicsConstants.SpriteSize)
                    {
                        Position = Game.Vector2FromTilePosition(5, 15.5)
                    };
                    graphicsHandler.UpdateStaticSprite(gameObjects["staticClyde"], GraphicsID.SpriteGhostRight0, PaletteID.Clyde);
                    break;
                case 11:
                    UntilNextFrame = 500;
                    tiles.DrawText(15, 7, "-POKEY", PaletteID.Clyde);
                    break;
                case 12:
                    UntilNextFrame = 500;
                    tiles.DrawText(15, 18, "\"CLYDE\"", PaletteID.Clyde);
                    break;
                case 13:
                    UntilNextFrame = 1000;
                    gameObjects["pelletDemo"] = new GameObject(GraphicsConstants.TileSize)
                    {
                        Position = Game.Vector2FromTilePosition(10.5, 23.5)
                    };
                    graphicsHandler.UpdateStaticSprite(gameObjects["pelletDemo"], GraphicsID.TilePelletSmall, PaletteID.Pellet, Resources.Tiles);
                    tiles.DrawText(23, 12, "10");
                    tiles.DrawPts(23, 15);

                    gameObjects["powerPelletDemo"] = new PowerPelletObject(graphicsHandler)
                    {
                        Position = Game.Vector2FromTilePosition(10.5, 25.5)
                    };
                    tiles.DrawText(25, 12, "50");
                    tiles.DrawPts(25, 15);
                    break;
            }
        }
    }
}