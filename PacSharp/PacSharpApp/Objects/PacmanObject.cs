using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
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

        private PacmanState State { get => state; set { state = value; OnStateChanged(); } }

        private void OnStateChanged()
        {
            if (State is PacmanDyingState || State is PacmanRespawningState)
                sprite.UpdateAnimationSet(PacmanSprite.AnimationID.Dying.ToString());
            else if (State is PacmanMovingState)
                sprite.UpdateAnimationSet(sprite.Orientation.ToPacmanSpriteAnimationID().ToString());
        }

        internal RectangleF MouthBounds => new RectangleF(new PointF((float)Position.X - 1.5f, (float)Position.Y - 1.5f), new Size(3, 3));

        internal bool IsMoving => State is PacmanMovingState;

        internal void HandleInput(InputHandler input)
        {
            State.HandleInput(input);
        }

        internal bool AttemptTurn(Direction newDirection)
        {
            if (sprite.Orientation == newDirection || !CanTurnTo(walls, DirectionVelocity(newDirection)))
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

        internal void ReturnToMovementState()
        {
            if (State is PacmanWarpingState)
                State = new PacmanMovingState(this);
        }

        internal void BeginRespawning(Action onRespawn) => State = new PacmanRespawningState(this, onRespawn);

        internal void BeginDeath(Action onDeath) => State = new PacmanDyingState(this, onDeath);

        internal void BeginWarping() => State = new PacmanWarpingState(this);

        #region State
        abstract class PacmanState
        {
            private protected PacmanObject owner;

            private protected PacmanState(PacmanObject owner)
            {
                this.owner = owner;
            }

            public PacmanEatingState Score { get; internal set; }

            internal virtual void HandleInput(InputHandler input) { }
            internal virtual void Update(bool animationFinished) { }
        }

        class PacmanMovingState : PacmanState
        {
            internal PacmanMovingState(PacmanObject owner)
                : base(owner)
            { }

            internal override void HandleInput(InputHandler input)
            {
                if (owner.PreventMovement)
                    return;
                bool turned = false;
                if (!turned && input.HeldKeys.Contains(Keys.W))
                {
                    turned = owner.AttemptTurn(Direction.Up);
                }
                if (!turned && input.HeldKeys.Contains(Keys.A))
                {
                    turned = owner.AttemptTurn(Direction.Left);
                }
                if (!turned && input.HeldKeys.Contains(Keys.S))
                {
                    turned = owner.AttemptTurn(Direction.Down);
                }
                if (!turned && input.HeldKeys.Contains(Keys.D))
                {
                    turned = owner.AttemptTurn(Direction.Right);
                }
                if (turned)
                    owner.Position += owner.Velocity * 20;
            }
        }

        class PacmanWarpingState : PacmanState
        {
            internal PacmanWarpingState(PacmanObject owner)
                : base(owner)
            { }
        }

        class PacmanRespawningState : PacmanState
        {
            private readonly Action onRespawn;

            internal PacmanRespawningState(PacmanObject owner, Action onRespawn)
                : base(owner)
            {
                this.onRespawn = onRespawn;
                owner.PreventMovement = true;
            }

            internal override void Update(bool animationFinished)
            {
                if (animationFinished)
                    onRespawn();
            }
        }

        class PacmanDyingState : PacmanState
        {
            private readonly Action onDeath;

            internal PacmanDyingState(PacmanObject owner, Action onDeath)
                : base(owner)
            {
                owner.PreventMovement = true;
                this.onDeath = onDeath;
            }

            internal override void Update(bool animationFinished)
            {
                if (animationFinished)
                    onDeath();
            }
        }

        class PacmanEatingState : PacmanState
        {
            private protected PacmanEatingState(PacmanObject owner)
                : base(owner)
            { }
        }
        #endregion
    }
}
