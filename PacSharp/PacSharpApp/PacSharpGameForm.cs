using System.Drawing;
using PacSharpApp.Graphics;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    internal partial class PacSharpGameForm : GameUI
    {
        private protected override int GridScale => 2;

        public PacSharpGameForm()
        {
            InitializeComponent();
            Size = new Size(
                WindowBorderWidth + GraphicsHandler.GridWidth * GraphicsHandler.TileWidth * GridScale,
                WindowBorderHeight + GraphicsHandler.GridHeight * GraphicsHandler.TileWidth * GridScale);
            Game game = new PacSharpGame(this, gameArea);
            game.Init();
            game.Reset();
        }
    }
}
