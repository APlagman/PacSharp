using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    public partial class GameForm : Form
    {
        private const int GridScale = 2;
        private const int WindowBorderWidth = 16;
        private const int WindowBorderHeight = 39;

        public GameForm()
        {
            InitializeComponent();
            Size = new Size(
                WindowBorderWidth + GraphicsHandler.GridWidth * GraphicsHandler.TileWidth * GridScale,
                WindowBorderHeight + GraphicsHandler.GridHeight * GraphicsHandler.TileWidth * GridScale);
            Game game = new PacSharpGame(this, gameArea);
            game.Init();
            game.Reset();
        }

        internal void UpdateControls(GameState state)
        {
            //throw new NotImplementedException();
        }

        internal void OnNewGame()
        {
            throw new NotImplementedException();
        }
    }
}
