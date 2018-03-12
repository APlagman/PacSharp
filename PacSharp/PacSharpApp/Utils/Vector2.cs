using System;
using System.Drawing;
using PacSharpApp.Graphics;
/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Utils
{
    /// <summary>
    /// Utility class for 2D floating-point arithmetic
    /// </summary>
    struct Vector2
    {
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"({X.ToString("N2")}, {Y.ToString("N2")})";
        }

        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
        }

        public static Vector2 operator *(Vector2 vector, long scale)
        {
            return new Vector2(vector.X * scale, vector.Y * scale);
        }

        public static Vector2 operator *(Vector2 vector, double scale)
        {
            return new Vector2(vector.X * scale, vector.Y * scale);
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Vector2 operator -(Vector2 lhs, Vector2 right)
        {
            return lhs + (right * -1);
        }

        public void Round()
        {
            X = Math.Round(X);
            Y = Math.Round(Y);
        }
    }

    static class Vector2Extensions
    {
        public static PointF ToPointF(this Vector2 vec)
        {
            return new PointF((float)vec.X, (float)vec.Y);
        }

        public static Vector2 RoundedToNearest(this Vector2 vec, double val)
        {
            double x = vec.X - vec.X % val;
            if (vec.X % val > val / 2)
                x += val;
            double y = vec.Y - vec.Y % val;
            if (vec.Y % val > val / 2)
                y += val;
            return new Vector2(x, y);
        }

        public static Point ToTilePoint(this Vector2 vec)
        {
            return new Point((int)Math.Floor(vec.X / GraphicsConstants.TileWidth), (int)Math.Floor(vec.Y / GraphicsConstants.TileWidth));
        }
    }
}
