using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private PacmanSprite sprite;
        private PacmanState state;
        private IReadOnlyCollection<RectangleF> walls;

        public Direction Orientation => sprite.Orientation;

        internal PacmanObject(GraphicsHandler handler, IReadOnlyCollection<RectangleF> walls)
            : base(GraphicsConstants.SpriteSize)
        {
            sprite = new PacmanSprite
            {
                Orientation = Direction.Right
            };
            handler.Register(this, sprite);
            State = new PacmanMovingState(this);
            this.walls = walls;
        }

        internal PacmanState State { get => state; set { state = value; OnStateChanged(); } }

        private void OnStateChanged()
        {
            if (State is PacmanDyingState || State is PacmanRespawningState)
                sprite.UpdateAnimationSet(PacmanSprite.AnimationID.Dying.ToString());
            else if (State is PacmanMovingState)
                sprite.UpdateAnimationSet(sprite.Orientation.ToPacmanSpriteAnimationID().ToString());
        }

        public RectangleF MouthBounds => new RectangleF(new PointF((float)Position.X - 1.5f, (float)Position.Y - 1.5f), new Size(3, 3));

        internal void HandleInput(InputHandler input)
        {
            State.HandleInput(input);
        }

        internal bool AttemptTurn(Direction newDirection)
        {
            if (sprite.Orientation == newDirection || !CanTurnTo(DirectionVelocity(newDirection)))
                return false;
            PerformTurn(newDirection);
            return true;
        }

        internal void PerformTurn(Direction? newDirection)
        {
            if (newDirection == null)
                return;
            Velocity = DirectionVelocity(newDirection.Value);
            if (sprite.Orientation != newDirection)
                sprite.Orientation = newDirection.Value;
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

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            if ((state is PacmanRespawningState || state is PacmanDyingState))
                sprite.RepeatAnimation = false;
            state.Update(sprite.AnimationFinished);
        }

        internal bool CanTurnTo(Vector2 directionVelocity)
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
