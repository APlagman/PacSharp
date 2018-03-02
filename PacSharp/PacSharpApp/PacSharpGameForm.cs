using System.Drawing;
using System.Windows.Forms;
using PacSharpApp.Graphics;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    internal partial class PacSharpGameForm : Form, IGameUI
    {
        public PacSharpGameForm()
        {
            InitializeComponent();
            Size = new Size(
                GameUIConstants.WindowBorderWidth + GraphicsConstants.GridWidth * GraphicsConstants.TileWidth * GameUIConstants.GridScale,
                GameUIConstants.WindowBorderHeight + GraphicsConstants.GridHeight * GraphicsConstants.TileWidth * GameUIConstants.GridScale);
            Game game = new PacSharpGame(this, gameArea);
            game.Init();
            KeyDown += game.InputHandler.OnKeyDown;
            KeyUp += game.InputHandler.OnKeyUp;
            game.ScheduleReset();
        }

        public void OnNewGame()
        {
            throw new System.NotImplementedException();
        }
    }
}
