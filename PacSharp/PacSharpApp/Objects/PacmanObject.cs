using System;
using System.Drawing;
using PacSharpApp.Graphics;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    class PacmanObject : GameObject
    {
        private const double DefaultSpeed = 0.03d;

        private AnimatedSprite sprite = new PacmanSprite();
        private Func<Vector2, bool> canTurnTo;

        public Direction Orientation => sprite.Orientation;

        internal PacmanObject(GraphicsHandler graphics, Func<Vector2, bool> canTurn = null)
            : base(GraphicsConstants.SpriteSize)
        {
            State = new PacmanMovingState(this);
            graphics.UpdateAnimatedSprite(this, sprite);
            canTurnTo = canTurn;
        }

        internal PacmanState State { get; set; }
        public RectangleF MouthBounds => new RectangleF(new PointF((float)Position.X - 1.5f, (float)Position.Y - 1.5f), new Size(3, 3));

        internal void HandleInput(InputHandler input)
        {
            State.HandleInput(input);
        }

        internal bool AttemptTurn(Direction newDirection)
        {
            if (sprite.Orientation == newDirection || !canTurnTo(DirectionVelocity(newDirection)))
                return false;
            PerformTurn(newDirection);
            return true;
        }

        internal void PerformTurn(Direction newDirection)
        {
            Velocity = DirectionVelocity(newDirection);
            if (sprite.Orientation != newDirection)
                sprite.Orientation = newDirection;
            // Round position to nearest pixel to help with collision
            Position.Round();
        }

        internal static Vector2 DirectionVelocity(Direction newDirection)
        {
            switch (newDirection)
            {
                case Direction.Up:
                    return new Vector2(0, Game.UpMultiplier * DefaultSpeed);
                case Direction.Down:
                    return new Vector2(0, Game.DownMultiplier * DefaultSpeed);
                case Direction.Left:
                    return new Vector2(-1 * DefaultSpeed, 0);
                case Direction.Right:
                    return new Vector2(1 * DefaultSpeed, 0);
                default:
                    throw new Exception("Unhandled direction.");
            }
        }
    }
}
