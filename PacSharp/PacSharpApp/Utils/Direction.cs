using System;
/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Utils
{
    enum Direction
    {
        Up = 0,
        Left = 270,
        Down = 180,
        Right = 90
    }

    static class DirectionExtensions
    {
        public static Direction GetOpposite(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    throw new Exception("Unhandled direction.");
            }
        }
    }
}
