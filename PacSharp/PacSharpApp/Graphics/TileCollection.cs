﻿using System;
/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    class TileCollection
    {
        internal class Tile
        {
            public Tile(GraphicsID graphicsId, PaletteID palette)
            {
                GraphicsID = graphicsId;
                Palette = palette;
            }

            internal GraphicsID GraphicsID { get; }
            internal PaletteID Palette { get; }
            internal bool Updated { get; set; } = true;
        }

        private Tile[,] tiles;

        internal TileCollection(int width, int height)
        {
            tiles = new Tile[height, width];
        }

        internal Tile this[int row, int column] => tiles[row, column];

        internal int Width => tiles.GetLength(1);
        internal int Height => tiles.GetLength(0);

        internal void SetTile(int row, int column, GraphicsID graphics, PaletteID palette)
        {
            if (tiles[row, column] != null && tiles[row, column].GraphicsID == graphics && tiles[row, column].Palette == palette)
                return;
            tiles[row, column] = new Tile(graphics, palette);
        }

        internal bool HasBeenUpdated
        {
            get
            {
                for (int r = 0; r < Height; ++r)
                    for (int c = 0; c < Width; ++c)
                        if (tiles[r, c].Updated)
                            return true;
                return false;
            }
        }

        internal void DrawInteger(int row, int lastDigitColumn, int toDraw, PaletteID palette = PaletteID.Text)
        {
            if (toDraw < 0)
                throw new ArgumentException("Number must be positive.");
            if (toDraw < 10)
            {
                // Draw "0#"
                tiles[row, lastDigitColumn] = new Tile(DigitTile(toDraw), palette);
                if (lastDigitColumn > 0)
                    tiles[row, lastDigitColumn - 1] = new Tile(GraphicsID.Tile0, palette);
            }
            else
            {
                int col = lastDigitColumn;
                int digits = 0;
                while (toDraw > 0)
                {
                    int digit = toDraw % 10;
                    toDraw /= 10;
                    tiles[row, col] = new Tile(DigitTile(digit), palette);
                    --col;
                    ++digits;
                }
                while (col > lastDigitColumn - digits)
                    tiles[row, col--] = new Tile(GraphicsID.TileEmpty, PaletteID.Empty);
            }
        }

        private static GraphicsID DigitTile(int digit)
        {
            switch (digit)
            {
                case 0:
                    return GraphicsID.Tile0;
                case 1:
                    return GraphicsID.Tile1;
                case 2:
                    return GraphicsID.Tile2;
                case 3:
                    return GraphicsID.Tile3;
                case 4:
                    return GraphicsID.Tile4;
                case 5:
                    return GraphicsID.Tile5;
                case 6:
                    return GraphicsID.Tile6;
                case 7:
                    return GraphicsID.Tile7;
                case 8:
                    return GraphicsID.Tile8;
                case 9:
                    return GraphicsID.Tile9;
                default:
                    throw new ArgumentException("Not a digit.");
            }
        }

        internal void DrawText(int row, int startColumn, string text, PaletteID palette = PaletteID.Text)
        {
            for (int c = 0; c < text.Length; ++c)
                tiles[row, startColumn + c] = new Tile(TextChararacterTile(text[c]), palette);
        }

        private static GraphicsID TextChararacterTile(char letter)
        {
            if (int.TryParse(letter.ToString(), out int val))
                return DigitTile(val);
            switch (letter)
            {
                default:
                    throw new ArgumentException("Invalid character.");
                case 'A':
                    return GraphicsID.TileA;
                case 'B':
                    return GraphicsID.TileB;
                case 'C':
                    return GraphicsID.TileC;
                case 'D':
                    return GraphicsID.TileD;
                case 'E':
                    return GraphicsID.TileE;
                case 'F':
                    return GraphicsID.TileF;
                case 'G':
                    return GraphicsID.TileG;
                case 'H':
                    return GraphicsID.TileH;
                case 'I':
                    return GraphicsID.TileI;
                case 'J':
                    return GraphicsID.TileJ;
                case 'K':
                    return GraphicsID.TileK;
                case 'L':
                    return GraphicsID.TileL;
                case 'M':
                    return GraphicsID.TileM;
                case 'N':
                    return GraphicsID.TileN;
                case 'O':
                    return GraphicsID.TileO;
                case 'P':
                    return GraphicsID.TileP;
                case 'Q':
                    return GraphicsID.TileQ;
                case 'R':
                    return GraphicsID.TileR;
                case 'S':
                    return GraphicsID.TileS;
                case 'T':
                    return GraphicsID.TileT;
                case 'U':
                    return GraphicsID.TileU;
                case 'V':
                    return GraphicsID.TileV;
                case 'W':
                    return GraphicsID.TileW;
                case 'X':
                    return GraphicsID.TileX;
                case 'Y':
                    return GraphicsID.TileY;
                case 'Z':
                    return GraphicsID.TileZ;
                case '-':
                    return GraphicsID.TileHyphen;
                case '/':
                    return GraphicsID.TileForwardSlash;
                case '"':
                    return GraphicsID.TileQuotationMark;
                case ' ':
                    return GraphicsID.TileEmpty;
                case '!':
                    return GraphicsID.TileExclamationMark;
            }
        }

        internal void DrawPts(int row, int column)
        {
            tiles[row, column] = new Tile(GraphicsID.TilePts0, PaletteID.Text);
            tiles[row, column + 1] = new Tile(GraphicsID.TilePts1, PaletteID.Text);
            tiles[row, column + 2] = new Tile(GraphicsID.TilePts2, PaletteID.Text);
        }

        internal void ClearTile(int row, int column)
        {
            SetTile(row, column, GraphicsID.TileEmpty, PaletteID.Empty);
        }

        internal void Clear()
        {
            for (int row = 0; row < Height; ++row)
                for (int col = 0; col < Width; ++col)
                    ClearTile(row, col);
        }

        internal void DrawRange((int row, int col) start, (int row, int col) end, GraphicsID graphics, PaletteID palette)
        {
            for (int row = start.row; row <= end.row; ++row)
                for (int col = start.col; col <= end.col; ++col)
                    SetTile(row, col, graphics, palette);
        }

        internal void DrawFruit(int upperLeftRow, int UpperLeftCol, GraphicsID graphicsID)
        {
            Tile[,] fruitTiles = new Tile[2, 2];
            switch (graphicsID)
            {
                case GraphicsID.SpriteCherry:
                    {
                        fruitTiles[0, 0] = new Tile(GraphicsID.TileCherryTopLeft, PaletteID.Cherry);
                        fruitTiles[0, 1] = new Tile(GraphicsID.TileCherryTopRight, PaletteID.Cherry);
                        fruitTiles[1, 0] = new Tile(GraphicsID.TileCherryBottomLeft, PaletteID.Cherry);
                        fruitTiles[1, 1] = new Tile(GraphicsID.TileCherryBottomRight, PaletteID.Cherry);
                        break;
                    }
                case GraphicsID.SpriteStrawberry:
                    {
                        fruitTiles[0, 0] = new Tile(GraphicsID.TileStrawberryTopLeft, PaletteID.Strawberry);
                        fruitTiles[0, 1] = new Tile(GraphicsID.TileStrawberryTopRight, PaletteID.Strawberry);
                        fruitTiles[1, 0] = new Tile(GraphicsID.TileStrawberryBottomLeft, PaletteID.Strawberry);
                        fruitTiles[1, 1] = new Tile(GraphicsID.TileStrawberryBottomRight, PaletteID.Strawberry);
                        break;
                    }
                case GraphicsID.SpriteOrange:
                    {
                        fruitTiles[0, 0] = new Tile(GraphicsID.TileOrangeTopLeft, PaletteID.Orange);
                        fruitTiles[0, 1] = new Tile(GraphicsID.TileOrangeTopRight, PaletteID.Orange);
                        fruitTiles[1, 0] = new Tile(GraphicsID.TileOrangeBottomLeft, PaletteID.Orange);
                        fruitTiles[1, 1] = new Tile(GraphicsID.TileOrangeBottomRight, PaletteID.Orange);
                        break;
                    }
                case GraphicsID.SpriteMelon:
                    {
                        fruitTiles[0, 0] = new Tile(GraphicsID.TileMelonTopLeft, PaletteID.Melon);
                        fruitTiles[0, 1] = new Tile(GraphicsID.TileMelonTopRight, PaletteID.Melon);
                        fruitTiles[1, 0] = new Tile(GraphicsID.TileMelonBottomLeft, PaletteID.Melon);
                        fruitTiles[1, 1] = new Tile(GraphicsID.TileMelonBottomRight, PaletteID.Melon);
                        break;
                    }
                case GraphicsID.SpriteKey:
                    {
                        fruitTiles[0, 0] = new Tile(GraphicsID.TileKeyTopLeft, PaletteID.Key);
                        fruitTiles[0, 1] = new Tile(GraphicsID.TileKeyTopRight, PaletteID.Key);
                        fruitTiles[1, 0] = new Tile(GraphicsID.TileKeyBottomLeft, PaletteID.Key);
                        fruitTiles[1, 1] = new Tile(GraphicsID.TileKeyBottomRight, PaletteID.Key);
                        break;
                    }
                case GraphicsID.SpriteGalaxian:
                    {
                        fruitTiles[0, 0] = new Tile(GraphicsID.TileGalaxianTopLeft, PaletteID.Galaxian);
                        fruitTiles[0, 1] = new Tile(GraphicsID.TileGalaxianTopRight, PaletteID.Galaxian);
                        fruitTiles[1, 0] = new Tile(GraphicsID.TileGalaxianBottomLeft, PaletteID.Galaxian);
                        fruitTiles[1, 1] = new Tile(GraphicsID.TileGalaxianBottomRight, PaletteID.Galaxian);
                        break;
                    }
                case GraphicsID.SpriteBell:
                    {
                        fruitTiles[0, 0] = new Tile(GraphicsID.TileBellTopLeft, PaletteID.Bell);
                        fruitTiles[0, 1] = new Tile(GraphicsID.TileBellTopRight, PaletteID.Bell);
                        fruitTiles[1, 0] = new Tile(GraphicsID.TileBellBottomLeft, PaletteID.Bell);
                        fruitTiles[1, 1] = new Tile(GraphicsID.TileBellBottomRight, PaletteID.Bell);
                        break;
                    }
            }
            tiles[upperLeftRow, UpperLeftCol] = fruitTiles[0, 0];
            tiles[upperLeftRow, UpperLeftCol + 1] = fruitTiles[0, 1];
            tiles[upperLeftRow + 1, UpperLeftCol] = fruitTiles[1, 0];
            tiles[upperLeftRow + 1, UpperLeftCol + 1] = fruitTiles[1, 1];
        }
    }
}