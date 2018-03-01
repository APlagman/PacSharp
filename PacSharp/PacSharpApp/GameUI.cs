using System.Windows.Forms;

namespace PacSharpApp
{
    internal class GameUI : Form
    {
        private protected virtual int GridScale { get; }
        private protected const int WindowBorderWidth = 16;
        private protected const int WindowBorderHeight = 39;

        internal virtual void OnNewGame()
        {
        }
    }
}