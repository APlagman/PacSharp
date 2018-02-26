using System.Collections.Generic;
using System.Windows.Forms;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    class InputHandler
    {
        internal HashSet<Keys> PressedKeys { get; } = new HashSet<Keys>();
        internal HashSet<Keys> HeldKeys { get; } = new HashSet<Keys>();
        internal HashSet<Keys> ReleasedKeys { get; } = new HashSet<Keys>();

        internal void Update()
        {
            PressedKeys.Clear();
            ReleasedKeys.Clear();
        }

        internal void OnKeyDown(object sender, KeyEventArgs e)
        {
            PressedKeys.Add(e.KeyCode);
            HeldKeys.Add(e.KeyCode);
        }

        internal void OnKeyUp(object sender, KeyEventArgs e)
        {
            HeldKeys.Remove(e.KeyCode);
        }
    }
}
