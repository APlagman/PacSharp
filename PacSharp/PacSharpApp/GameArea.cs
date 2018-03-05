using System;
using System.Drawing;
using System.Windows.Forms;
using PacSharpApp.Utils;

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
        private readonly Control control;

        internal GameArea(Size size, Control control, PaintEventHandler OnPaint)
        {
            this.control = control;
            Size = size;
        }

        internal double Left { get { return 0; } }
        internal double Right { get { return Size.Width; } }
        internal double Top { get { return 0; } }
        internal double Bottom { get { return Size.Height; } }
        internal Vector2 Origin { get { return new Vector2(Size.Width / 2d, Size.Height / 2d); } }
        internal Size Size { get; }
        internal Size ScreenSize => control.Size;

        internal void Render(Image screenImage) =>
            control.CreateGraphics().DrawImage(screenImage, new Rectangle(control.Location, control.Size));
    }
}
