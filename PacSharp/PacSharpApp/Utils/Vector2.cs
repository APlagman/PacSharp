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
    }
}
