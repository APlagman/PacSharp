using System;
/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    class Tile
    {
        public Tile(GraphicsID graphicsId, PaletteID palette)
        {
            GraphicsId = graphicsId;
            Palette = palette;
        }

        internal GraphicsID GraphicsId { get; }
        internal PaletteID Palette { get; }
        internal bool Updated { get; set; } = true;
    }

    static class TileExtensions
    {
        internal static void DrawInteger(this Tile[,] tiles, int tileYPosition, int tileXPositionOfLastDigit, int number, PaletteID palette = PaletteID.Text)
        {
            if (number < 0)
                throw new ArgumentException("Number must be positive.");
            if (number < 10)
            {
                // Draw "0#"
                tiles[tileYPosition, tileXPositionOfLastDigit] = new Tile(DigitTile(number), palette);
                if (tileXPositionOfLastDigit > 0)
                    tiles[tileYPosition, tileXPositionOfLastDigit - 1] = new Tile(GraphicsID.Tile0, palette);
            }
            else
            {
                int tileXPosition = tileXPositionOfLastDigit;
                while (number > 0)
                {
                    int digit = number % 10;
                    number /= 10;
                    tiles[tileYPosition, tileXPosition] = new Tile(DigitTile(digit), palette);
                }
                while (tileXPosition >= 0)
                    tiles[tileYPosition, tileXPosition--] = new Tile(GraphicsID.TileEmpty, PaletteID.Empty);
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

        internal static void DrawText(this Tile[,] tiles, int tileYPosition, int tileXPositionOfFirstLetter, string text, PaletteID palette = PaletteID.Text)
        {
            for (int c = 0; c < text.Length; ++c)
                tiles[tileYPosition, tileXPositionOfFirstLetter + c] = new Tile(TextChararacterTile(text[c]), palette);
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

        internal static void DrawPts(this Tile[,] tiles, int tileYPosition, int tileXPosition)
        {
            tiles[tileYPosition, tileXPosition] = new Tile(GraphicsID.TilePts0, PaletteID.Text);
            tiles[tileYPosition, tileXPosition + 1] = new Tile(GraphicsID.TilePts1, PaletteID.Text);
            tiles[tileYPosition, tileXPosition + 2] = new Tile(GraphicsID.TilePts2, PaletteID.Text);
        }

        internal static void ClearTile(this Tile[,] tiles, int row, int col)
        {
            tiles[row, col] = new Tile(GraphicsID.TileEmpty, PaletteID.Empty);
        }

        internal static void Clear(this Tile[,] tiles)
        {
            for (int row = 0; row < tiles.GetLength(0); ++row)
                for (int col = 0; col < tiles.GetLength(1); ++col)
                    tiles.ClearTile(row, col);
        }
    }
}