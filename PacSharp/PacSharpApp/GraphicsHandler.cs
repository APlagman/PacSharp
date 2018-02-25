using System.Collections.Generic;
using System.Windows.Forms;
/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    /// <summary>
    /// Handles drawing logic before delegating to the actual form
    /// </summary>
    class GraphicsHandler
    {
        private GameForm ui;
        private GameArea gameArea;
        private Dictionary<GameObject, Control> gameObjectMap = new Dictionary<GameObject, Control>();

        internal GraphicsHandler(GameForm ui, GameArea gameArea)
        {
            this.ui = ui;
            this.gameArea = gameArea;
        }

        internal void Draw(bool gameStarted)
        {
            UpdateGameObjectGraphics();
            UpdateUI(gameStarted);
        }

        private void UpdateUI(bool gameStarted)
        {
            if (ui.InvokeRequired)
                ui.Invoke((MethodInvoker)delegate { UpdateUI(gameStarted); });
            else
                ui.UpdateControls(gameStarted);
            ui.Invalidate();
        }

        private void UpdateGameObjectGraphics()
        {
            foreach (var goPair in gameObjectMap)
            {
                GameObject obj = goPair.Key;
                Control graphic = goPair.Value;
                if (graphic.InvokeRequired)
                    graphic.Invoke((MethodInvoker)delegate { graphic.UpdateLocation(obj, gameArea); });
                else
                    graphic.UpdateLocation(obj, gameArea);
            }
        }

        internal void Register(GameObject obj, Control graphics)
        {
            gameObjectMap.Add(obj, graphics);
        }
        
        internal void Close()
        {
            ui.Close();
        }

        internal void OnNewGame()
        {
            ui.OnNewGame();
        }
    }
}
