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
        private const string CharacterNicknameString = "CHARACTER / NICKNAME";
        private const string StaticBlinkyObjName = "staticBlinky";
        private const string BlinkyName = "SHADOW";
        private const string BlinkyNickname = "BLINKY";
        private const string StaticPinkyObjName = "staticPinky";
        private const string PinkyName = "SPEEDY";
        private const string PinkyNickname = "PINKY";
        private const string StaticInkyObjName = "staticInky";
        private const string InkyName = "BASHFUL";
        private const string InkyNickname = "INKY";
        private const string StaticClydeObjName = "staticClyde";
        private const string ClydeName = "POKEY";
        private const string ClydeNickname = "CLYDE";
        private const string PelletDemoObjName = "pelletDemo";
        private const string PowerPelletDemoObjName = "powerPelletDemo";

        public MainMenuAnimation(GraphicsHandler graphicsHandler, Action onCompletion)
            : base(graphicsHandler, 500, onCompletion)
        { }

        private protected override bool Repeat => false;
        private protected override int FrameCount => 14;

        private protected override void NextFrame(TileCollection tiles, IDictionary<string, GameObject> gameObjects)
        {
            switch (CurrentFrame)
            {
                case 0:
                    graphicsHandler.PreventAnimatedSpriteUpdates = true;
                    tiles.DrawText(4, 7, CharacterNicknameString);
                    break;
                case 1:
                    UntilNextFrame = 1000;
                    gameObjects[StaticBlinkyObjName] = new GameObject(GraphicsConstants.SpriteSize)
                    {
                        Position = Game.Vector2FromTilePosition(5, 6.5)
                    };
                    graphicsHandler.SetStaticSprite(gameObjects[StaticBlinkyObjName], GraphicsID.SpriteGhostRight0, PaletteID.Blinky);
                    break;
                case 2:
                    UntilNextFrame = 500;
                    tiles.DrawText(6, 7, $"-{BlinkyName}", PaletteID.Blinky);
                    break;
                case 3:
                    UntilNextFrame = 500;
                    tiles.DrawText(6, 18, $"\"{BlinkyNickname}\"", PaletteID.Blinky);
                    break;
                case 4:
                    UntilNextFrame = 1000;
                    gameObjects[StaticPinkyObjName] = new GameObject(GraphicsConstants.SpriteSize)
                    {
                        Position = Game.Vector2FromTilePosition(5, 9.5)
                    };
                    graphicsHandler.SetStaticSprite(gameObjects[StaticPinkyObjName], GraphicsID.SpriteGhostRight0, PaletteID.Pinky);
                    break;
                case 5:
                    UntilNextFrame = 500;
                    tiles.DrawText(9, 7, $"-{PinkyName}", PaletteID.Pinky);
                    break;
                case 6:
                    UntilNextFrame = 500;
                    tiles.DrawText(9, 18, $"\"{PinkyNickname}\"", PaletteID.Pinky);
                    break;
                case 7:
                    UntilNextFrame = 1000;
                    gameObjects[StaticInkyObjName] = new GameObject(GraphicsConstants.SpriteSize)
                    {
                        Position = Game.Vector2FromTilePosition(5, 12.5)
                    };
                    graphicsHandler.SetStaticSprite(gameObjects[StaticInkyObjName], GraphicsID.SpriteGhostRight0, PaletteID.Inky);
                    break;
                case 8:
                    UntilNextFrame = 500;
                    tiles.DrawText(12, 7, $"-{InkyName}", PaletteID.Inky);
                    break;
                case 9:
                    UntilNextFrame = 500;
                    tiles.DrawText(12, 18, $"\"{InkyNickname}\"", PaletteID.Inky);
                    break;
                case 10:
                    UntilNextFrame = 1000;
                    gameObjects[StaticClydeObjName] = new GameObject(GraphicsConstants.SpriteSize)
                    {
                        Position = Game.Vector2FromTilePosition(5, 15.5)
                    };
                    graphicsHandler.SetStaticSprite(gameObjects[StaticClydeObjName], GraphicsID.SpriteGhostRight0, PaletteID.Clyde);
                    break;
                case 11:
                    UntilNextFrame = 500;
                    tiles.DrawText(15, 7, $"-{ClydeName}", PaletteID.Clyde);
                    break;
                case 12:
                    UntilNextFrame = 500;
                    tiles.DrawText(15, 18, $"\"{ClydeNickname}\"", PaletteID.Clyde);
                    break;
                case 13:
                    UntilNextFrame = 1000;
                    gameObjects[PelletDemoObjName] = new GameObject(GraphicsConstants.TileSize)
                    {
                        Position = Game.Vector2FromTilePosition(10.5, 23.5)
                    };
                    graphicsHandler.SetStaticSprite(gameObjects[PelletDemoObjName], GraphicsID.TilePelletSmall, PaletteID.Pellet, Resources.Tiles, GraphicsConstants.TileWidth);
                    tiles.DrawText(23, 12, PelletObject.Worth.ToString());
                    tiles.DrawPts(23, 15);

                    gameObjects[PowerPelletDemoObjName] = new PowerPelletObject(graphicsHandler)
                    {
                        Position = Game.Vector2FromTilePosition(10.5, 25.5)
                    };
                    tiles.DrawText(25, 12, PowerPelletObject.Worth.ToString());
                    tiles.DrawPts(25, 15);
                    break;
            }
        }
    }
}