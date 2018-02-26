using System;

namespace PacSharpApp
{
    internal class MainMenuAnimation : Animation
    {
        private static readonly long[] FrameTimings = new long[] { 100 };

        public MainMenuAnimation()
            : base(FrameTimings)
        {
        }

        protected private override void UpdateImpl(GraphicsId[,] tiles)
        {
            switch (CurrentFrame)
            {
                case 0:
                    EmptyTiles(tiles);
                    tiles[0, 3] = GraphicsId.Tile1;
                    tiles[0, 4] = GraphicsId.TileU;
                    tiles[0, 5] = GraphicsId.TileP;

                    tiles[0, 9] = GraphicsId.TileH;
                    tiles[0, 10] = GraphicsId.TileI;
                    tiles[0, 11] = GraphicsId.TileG;
                    tiles[0, 12] = GraphicsId.TileH;
                    tiles[0, 14] = GraphicsId.TileS;
                    tiles[0, 15] = GraphicsId.TileC;
                    tiles[0, 16] = GraphicsId.TileO;
                    tiles[0, 17] = GraphicsId.TileR;
                    tiles[0, 18] = GraphicsId.TileE;

                    tiles[0, 21] = GraphicsId.Tile2;
                    tiles[0, 22] = GraphicsId.TileU;
                    tiles[0, 23] = GraphicsId.TileP;

                    tiles[1, 5] = GraphicsId.Tile0;
                    tiles[1, 6] = GraphicsId.Tile0;

                    tiles[4, 7] = GraphicsId.TileC;
                    tiles[4, 8] = GraphicsId.TileH;
                    tiles[4, 9] = GraphicsId.TileA;
                    tiles[4, 10] = GraphicsId.TileR;
                    tiles[4, 11] = GraphicsId.TileA;
                    tiles[4, 12] = GraphicsId.TileC;
                    tiles[4, 13] = GraphicsId.TileT;
                    tiles[4, 14] = GraphicsId.TileE;
                    tiles[4, 15] = GraphicsId.TileR;
                    tiles[4, 17] = GraphicsId.TileForwardSlash;
                    tiles[4, 19] = GraphicsId.TileN;
                    tiles[4, 20] = GraphicsId.TileI;
                    tiles[4, 21] = GraphicsId.TileC;
                    tiles[4, 22] = GraphicsId.TileK;
                    tiles[4, 23] = GraphicsId.TileN;
                    tiles[4, 24] = GraphicsId.TileA;
                    tiles[4, 25] = GraphicsId.TileM;
                    tiles[4, 26] = GraphicsId.TileE;

                    tiles[35, 2] = GraphicsId.TileC;
                    tiles[35, 3] = GraphicsId.TileR;
                    tiles[35, 4] = GraphicsId.TileE;
                    tiles[35, 5] = GraphicsId.TileD;
                    tiles[35, 6] = GraphicsId.TileI;
                    tiles[35, 7] = GraphicsId.TileT;
                    tiles[35, 10] = GraphicsId.Tile0;
                    break;
                case 1:
                    break;
            }
        }
    }
}