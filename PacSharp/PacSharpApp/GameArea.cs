using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    /// <summary>
    /// Control wrapper that represents a drawable game area
    /// </summary>
    class GameArea
    {
        private Control control;

        internal double Left { get { return 0; } }
        internal double Right { get { return control.Width; } }
        internal double Top { get { return 0; } }
        internal double Bottom { get { return control.Height; } }
        internal Vector2 Origin { get { return new Vector2(control.Width / 2d, control.Height / 2d); } }
        internal Size Size { get { return control.Size; } }
        internal Point Location { get { return control.Location; } }

        internal GameArea(Control control)
        {
            this.control = control;
        }
    }
}
