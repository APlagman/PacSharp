using System;
using System.Collections.Generic;

namespace PacSharpApp
{
    internal class MainMenuAnimation : Animation
    {
        private static readonly long[] FrameTimings = new long[]
        {
            2000,
            1000,
            500,
            500,
            1000,
            500,
            500,
            1000,
            500,
            500,
            1000,
            500,
            500,
            1000,
            500,
            500,
            800,
            800
        };

        public MainMenuAnimation(GraphicsHandler graphicsHandler)
            : base(graphicsHandler, FrameTimings)
        { }

        private protected override bool Repeat => false;

        private protected override void NextFrame(Tile[,] tiles, IDictionary<string, GameObject> gameObjects)
        {
            switch (CurrentFrame)
            {
                case 0:
                    Game.EmptyTiles(tiles);
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
                    break;
            }
        }
    }
}