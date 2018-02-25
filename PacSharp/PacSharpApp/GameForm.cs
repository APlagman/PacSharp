using System;
using System.Windows.Forms;

namespace PacSharpApp
{
    public partial class GameForm : Form
    {
        private Game game;

        public GameForm()
        {
            game = new PacSharpGame(this, gameArea);
            game.Init();
            InitializeComponent();
        }

        internal void UpdateControls(bool gameStarted)
        {
            throw new NotImplementedException();
        }

        internal void OnNewGame()
        {
            throw new NotImplementedException();
        }
    }
}
