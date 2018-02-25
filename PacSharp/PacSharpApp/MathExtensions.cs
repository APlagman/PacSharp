using System;
using System.Drawing;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    /// <summary>
    /// Utility class for common math operations and converting between screen-space and game-space
    /// </summary>
    static class MathExtensions
    {
        public static double Clamp(this double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static float Clamp(this float value, float min, float max)
        {
            return (float)((double)value).Clamp(min, max);
        }

        public static int Clamp(this int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static Point ToScreenLocation(this Vector2 value, GameArea screenArea, Size objectSize)
        {
            return ToScreenLocation(value, new Point(), screenArea.Size, objectSize);
        }

        public static Point ToScreenLocation(this Vector2 value, Point screenOrigin, Size screenSize, Size objectSize)
        {
            return new Point(
                ((int)Math.Round(value.X) - objectSize.Width / 2).Clamp(0, screenSize.Width - objectSize.Width) + screenOrigin.X,
                ((int)Math.Round(value.Y) - objectSize.Height / 2).Clamp(0, screenSize.Height - objectSize.Height) + screenOrigin.Y
            );
        }
    }
}
