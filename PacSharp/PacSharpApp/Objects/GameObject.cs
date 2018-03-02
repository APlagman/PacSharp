using System.Drawing;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    /// <summary>
    /// Contains data representing a game object
    /// </summary>
    class GameObject
    {
        private Vector2 position;
        private Vector2 velocity;

        internal GameObject(Size size)
        {
            Size = size;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
        }

        internal ref Vector2 Position { get { return ref position; } }
        internal ref Vector2 Velocity { get { return ref velocity; } }
        internal Size Size { get; }

        internal double Left => Position.X - Size.Width / 2d;
        internal double Right => Position.X + Size.Width / 2d;
        internal double Top => Position.Y - Size.Height / 2d;
        internal double Bottom => Position.Y + Size.Height / 2d;

        internal bool OriginAbove(double v) => Position.Y < v;
        internal bool OriginBelow(double v) => Position.Y > v;
        internal bool OriginLeftOf(double v) => Position.X < v;
        internal bool OriginRightOf(double v) => Position.X > v;

        internal bool TopAbove(double v) => Top < v;
        internal bool BottomBelow(double v) => Bottom > v;
        internal bool LeftSideLeftOf(double v) => Left < v;
        internal bool RightSideRightOf(double v) => Right > v;

        internal Point ScreenPosition(GameArea screenArea) => Position.ToScreenLocation(screenArea, Size);

        internal void BoundPositionWithin(Rectangle boundingBox)
        {
            Position.X = Position.X.Clamp(boundingBox.Location.X + Size.Width / 2d, boundingBox.Location.X + boundingBox.Size.Width - Size.Width / 2d);
            Position.Y = Position.Y.Clamp(boundingBox.Location.Y + Size.Height / 2d, boundingBox.Location.Y + boundingBox.Size.Height - Size.Height / 2d);
        }
    }
}
