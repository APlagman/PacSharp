using System.Drawing;
/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Utils
{
    static class RectangleExtensions
    {
        internal static PointF GetCenter(this RectangleF rect)
        {
            return new PointF(rect.Location.X + rect.Width / 2, rect.Location.Y + rect.Height / 2);
        }

        internal static Point Center(this Rectangle rect)
        {
            return new Point(rect.Location.X + rect.Width / 2, rect.Location.Y + rect.Height / 2);
        }
    }
}
