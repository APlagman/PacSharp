using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PacSharpApp.AI;
using PacSharpApp.Graphics;
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
        private AIBehavior behavior;
        private Vector2 position;
        private Vector2 velocity;

        internal GameObject(Size size)
        {
            Size = size;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
        }

        internal AIBehavior Behavior
        {
            get => behavior;
            set
            {
                if (behavior == null)
                    behavior = value;
            }
        }
        internal ref Vector2 Position { get { return ref position; } }
        internal ref Vector2 Velocity { get { return ref velocity; } }
        internal bool PreventMovement { get; set; } = false;
        internal Size Size { get; }
        internal Point TilePosition => new Point((int)Math.Floor(Position.X / GraphicsConstants.TileWidth), (int)Math.Floor(Position.Y / GraphicsConstants.TileWidth));

        internal RectangleF Bounds => new RectangleF(new PointF((float)Position.X - Size.Width / 2, (float)Position.Y - Size.Height / 2), Size);

        internal double Left => Position.X - Size.Width / 2d;
        internal double Right => Position.X + Size.Width / 2d;
        internal double Top => Position.Y - Size.Height / 2d;
        internal double Bottom => Position.Y + Size.Height / 2d;

        internal virtual void Update(TimeSpan elapsedTime)
        {
            if (Behavior != null)
                Behavior.Update(elapsedTime);
            if (!PreventMovement)
            {
                Position.X += elapsedTime.TotalMilliseconds * Velocity.X;
                Position.Y += elapsedTime.TotalMilliseconds * Velocity.Y;
            }
        }

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

        internal bool CollidingFromBelow(RectangleF rect)
        {
            return OriginBelow(rect.Bottom)
                && TopAbove(rect.Bottom)
                && LeftSideLeftOf(rect.Right)
                && RightSideRightOf(rect.Left);
        }

        internal bool CollidingFromAbove(RectangleF rect)
        {
            return OriginAbove(rect.Top)
                && BottomBelow(rect.Top)
                && LeftSideLeftOf(rect.Right)
                && RightSideRightOf(rect.Left);
        }

        internal bool CollidingFromLeft(RectangleF rect)
        {
            return OriginLeftOf(rect.Left)
                && RightSideRightOf(rect.Left)
                && BottomBelow(rect.Top)
                && TopAbove(rect.Bottom);
        }

        internal bool CollidingFromRight(RectangleF rect)
        {
            return OriginRightOf(rect.Right)
                && LeftSideLeftOf(rect.Right)
                && BottomBelow(rect.Top)
                && TopAbove(rect.Bottom);
        }

        internal bool CanTurnTo(IReadOnlyCollection<RectangleF> walls, Vector2 directionVelocity)
        {
            Vector2 temp = Position;
            Position.Round();
            Position += directionVelocity;
            bool canTurn = !walls.Any(wall => Bounds.IntersectsWith(wall));
            Position = temp;
            return canTurn;
        }
    }
}
