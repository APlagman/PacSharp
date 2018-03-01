using System;
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
        internal GameArea(Control control, PaintEventHandler OnPaint) => Control = control;

        private Control Control { get; }

        internal double Left { get { return 0; } }
        internal double Right { get { return Control.Width; } }
        internal double Top { get { return 0; } }
        internal double Bottom { get { return Control.Height; } }
        internal Vector2 Origin { get { return new Vector2(Control.Width / 2d, Control.Height / 2d); } }
        internal Size Size { get { return Control.Size; } }
        internal Point Location { get { return Control.Location; } }

        internal void Render(Image screenImage) =>
            Control.CreateGraphics().DrawImage(screenImage, new Rectangle(Control.Location, Control.Size));
    }
}
