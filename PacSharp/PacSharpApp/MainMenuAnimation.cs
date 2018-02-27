using System;

namespace PacSharpApp
{
    internal class MainMenuAnimation : Animation
    {
        private static readonly long[] FrameTimings = new long[] { 2000, 1000, 500, 500, 1000, 500, 500, 1000, 500, 500, 1000, 500, 500, 1000, 500, 500, 800, 800 };

        public MainMenuAnimation()
            : base(FrameTimings)
        {
        }

        private protected override bool Repeat => false;

        private protected override void UpdateTiles(Tile[,] tiles)
        {
            switch (CurrentFrame)
            {
                case 0:
                    EmptyTiles(tiles);
                    tiles[0, 3] = new Tile(GraphicsIDs.Tile1, PaletteID.Text);
                    tiles[0, 4] = new Tile(GraphicsIDs.TileU, PaletteID.Text);
                    tiles[0, 5] = new Tile(GraphicsIDs.TileP, PaletteID.Text);

                    tiles[0, 9] = new Tile(GraphicsIDs.TileH, PaletteID.Text);
                    tiles[0, 10] = new Tile(GraphicsIDs.TileI, PaletteID.Text);
                    tiles[0, 11] = new Tile(GraphicsIDs.TileG, PaletteID.Text);
                    tiles[0, 12] = new Tile(GraphicsIDs.TileH, PaletteID.Text);
                    tiles[0, 14] = new Tile(GraphicsIDs.TileS, PaletteID.Text);
                    tiles[0, 15] = new Tile(GraphicsIDs.TileC, PaletteID.Text);
                    tiles[0, 16] = new Tile(GraphicsIDs.TileO, PaletteID.Text);
                    tiles[0, 17] = new Tile(GraphicsIDs.TileR, PaletteID.Text);
                    tiles[0, 18] = new Tile(GraphicsIDs.TileE, PaletteID.Text);

                    tiles[0, 21] = new Tile(GraphicsIDs.Tile2, PaletteID.Text);
                    tiles[0, 22] = new Tile(GraphicsIDs.TileU, PaletteID.Text);
                    tiles[0, 23] = new Tile(GraphicsIDs.TileP, PaletteID.Text);

                    tiles[1, 5] = new Tile(GraphicsIDs.Tile0, PaletteID.Text);
                    tiles[1, 6] = new Tile(GraphicsIDs.Tile0, PaletteID.Text);

                    tiles[4, 7] = new Tile(GraphicsIDs.TileC, PaletteID.Text);
                    tiles[4, 8] = new Tile(GraphicsIDs.TileH, PaletteID.Text);
                    tiles[4, 9] = new Tile(GraphicsIDs.TileA, PaletteID.Text);
                    tiles[4, 10] = new Tile(GraphicsIDs.TileR, PaletteID.Text);
                    tiles[4, 11] = new Tile(GraphicsIDs.TileA, PaletteID.Text);
                    tiles[4, 12] = new Tile(GraphicsIDs.TileC, PaletteID.Text);
                    tiles[4, 13] = new Tile(GraphicsIDs.TileT, PaletteID.Text);
                    tiles[4, 14] = new Tile(GraphicsIDs.TileE, PaletteID.Text);
                    tiles[4, 15] = new Tile(GraphicsIDs.TileR, PaletteID.Text);
                    tiles[4, 17] = new Tile(GraphicsIDs.TileForwardSlash, PaletteID.Text);
                    tiles[4, 19] = new Tile(GraphicsIDs.TileN, PaletteID.Text);
                    tiles[4, 20] = new Tile(GraphicsIDs.TileI, PaletteID.Text);
                    tiles[4, 21] = new Tile(GraphicsIDs.TileC, PaletteID.Text);
                    tiles[4, 22] = new Tile(GraphicsIDs.TileK, PaletteID.Text);
                    tiles[4, 23] = new Tile(GraphicsIDs.TileN, PaletteID.Text);
                    tiles[4, 24] = new Tile(GraphicsIDs.TileA, PaletteID.Text);
                    tiles[4, 25] = new Tile(GraphicsIDs.TileM, PaletteID.Text);
                    tiles[4, 26] = new Tile(GraphicsIDs.TileE, PaletteID.Text);

                    tiles[35, 2] = new Tile(GraphicsIDs.TileC, PaletteID.Text);
                    tiles[35, 3] = new Tile(GraphicsIDs.TileR, PaletteID.Text);
                    tiles[35, 4] = new Tile(GraphicsIDs.TileE, PaletteID.Text);
                    tiles[35, 5] = new Tile(GraphicsIDs.TileD, PaletteID.Text);
                    tiles[35, 6] = new Tile(GraphicsIDs.TileI, PaletteID.Text);
                    tiles[35, 7] = new Tile(GraphicsIDs.TileT, PaletteID.Text);
                    tiles[35, 10] = new Tile(GraphicsIDs.Tile0, PaletteID.Text);
                    break;
                case 1:
                    break;
            }
        }
    }
}