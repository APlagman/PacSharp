using System;
using System.Drawing;

namespace PacSharpApp.Utils
{
    static class PointExtensions
    {
        public static double DistanceTo(this Point self, Point other)
        {
            return Math.Sqrt(Math.Pow(self.X - other.X, 2) + Math.Pow(self.Y - other.Y, 2));
        }
    }
}
