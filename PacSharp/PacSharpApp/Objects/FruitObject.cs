using System;
using PacSharpApp.Graphics;
using PacSharpApp.Properties;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    class FruitObject : GameObject
    {
        private Sprite sprite;

        internal FruitObject(GraphicsHandler handler, GraphicsID spriteID)
            : base(GraphicsConstants.SpriteSize)
        {
            sprite = new StaticSprite(Resources.Sprites, spriteID, GraphicsConstants.SpriteWidth, Resources.Sprites.Width / GraphicsConstants.SpriteWidth)
            {
                Palette = spriteID.ToFruitPalette()
            };
            handler.Register(this, sprite);
            switch (spriteID)
            {
                default:
                    throw new Exception("Unhandled GraphicsID.");
                case GraphicsID.SpriteCherry:
                    Score = 100;
                    break;
                case GraphicsID.SpriteStrawberry:
                    Score = 300;
                    break;
                case GraphicsID.SpriteOrange:
                    Score = 500;
                    break;
                case GraphicsID.SpriteApple:
                    Score = 700;
                    break;
                case GraphicsID.SpriteMelon:
                    Score = 1000;
                    break;
                case GraphicsID.SpriteGalaxian:
                    Score = 2000;
                    break;
                case GraphicsID.SpriteBell:
                    Score = 3000;
                    break;
                case GraphicsID.SpriteKey:
                    Score = 5000;
                    break;
            }
        }

        internal int Score { get; }

        internal void DrawPoints(TileCollection tiles, int fruitRow, int fruitLeftCol)
        {
            switch (Score)
            {
                default:
                    throw new Exception("Unhandled score.");
                case 100:
                    tiles.SetTile(fruitRow, fruitLeftCol, GraphicsID.TilePoints10, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 1, GraphicsID.TilePoints00End, PaletteID.Text);
                    break;
                case 300:
                    tiles.SetTile(fruitRow, fruitLeftCol, GraphicsID.TilePoints30, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 1, GraphicsID.TilePoints00End, PaletteID.Text);
                    break;
                case 500:
                    tiles.SetTile(fruitRow, fruitLeftCol, GraphicsID.TilePoints50, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 1, GraphicsID.TilePoints00End, PaletteID.Text);
                    break;
                case 700:
                    tiles.SetTile(fruitRow, fruitLeftCol, GraphicsID.TilePoints70, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 1, GraphicsID.TilePoints00End, PaletteID.Text);
                    break;
                case 1000:
                    tiles.SetTile(fruitRow, fruitLeftCol, GraphicsID.TilePoints10LeftAligned, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 1, GraphicsID.TilePoints00Left, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 2, GraphicsID.TilePoints00Right, PaletteID.Text);
                    break;
                case 2000:
                    tiles.SetTile(fruitRow, fruitLeftCol, GraphicsID.TilePoints20Left, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 1, GraphicsID.TilePoints20Right, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 2, GraphicsID.TilePoints00Left, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 3, GraphicsID.TilePoints00Right, PaletteID.Text);
                    break;
                case 3000:
                    tiles.SetTile(fruitRow, fruitLeftCol, GraphicsID.TilePoints30Left, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 1, GraphicsID.TilePoints30Right, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 2, GraphicsID.TilePoints00Left, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 3, GraphicsID.TilePoints00Right, PaletteID.Text);
                    break;
                case 5000:
                    tiles.SetTile(fruitRow, fruitLeftCol, GraphicsID.TilePoints50Left, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 1, GraphicsID.TilePoints50Right, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 2, GraphicsID.TilePoints00Left, PaletteID.Text);
                    tiles.SetTile(fruitRow, fruitLeftCol + 3, GraphicsID.TilePoints00Right, PaletteID.Text);
                    break;
            }
        }
    }

    static class GraphicsIDFruitExtensions
    {
        internal static PaletteID ToFruitPalette(this GraphicsID id)
        {
            switch (id)
            {
                default:
                    throw new Exception("Unhandled GraphicsID.");
                case GraphicsID.SpriteCherry:
                    return PaletteID.Cherry;
                case GraphicsID.SpriteStrawberry:
                    return PaletteID.Strawberry;
                case GraphicsID.SpriteOrange:
                    return PaletteID.Orange;
                case GraphicsID.SpriteApple:
                    return PaletteID.Apple;
                case GraphicsID.SpriteMelon:
                    return PaletteID.Melon;
                case GraphicsID.SpriteGalaxian:
                    return PaletteID.Galaxian;
                case GraphicsID.SpriteBell:
                    return PaletteID.Bell;
                case GraphicsID.SpriteKey:
                    return PaletteID.Key;
            }
        }
    }
}
