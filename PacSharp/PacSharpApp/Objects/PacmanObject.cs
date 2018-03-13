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
        private const double PlayerMovementSpeed = 0.088d;

        private int levelNumber;
        private PacmanSprite sprite;
        private PacmanState state;
        private IReadOnlyCollection<RectangleF> walls;

        internal Direction Orientation => sprite.Orientation;

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

        internal int LevelNumber { private get => levelNumber; set { levelNumber = value; } }
        internal bool GhostsAreFrightened { private get; set; } = false;
        private double CurrentSpeed
        {
            get
            {
                if (GhostsAreFrightened)
                {
                    if (LevelNumber == 0)
                        return PlayerMovementSpeed * 0.8;
                    else if (LevelNumber < 4 || LevelNumber > 19)
                        return PlayerMovementSpeed * 0.9;
                    else
                        return PlayerMovementSpeed;
                }
                else
                {
                    if (LevelNumber == 0)
                        return PlayerMovementSpeed * 0.9;
                    else if (LevelNumber < 4)
                        return PlayerMovementSpeed * 0.95;
                    else
                        return PlayerMovementSpeed;
                }
            }
        }

        private PacmanState State { get => state; set { state = value; OnStateChanged(); } }

        internal Point WarpStartPosition { get; private set; } = Point.Empty;

        private void OnStateChanged()
        {
            if (State is PacmanDyingState || State is PacmanRespawningState)
                sprite.UpdateAnimationSet(PacmanSprite.AnimationID.Dying.ToString());
            else if (State is PacmanMovingState)
                sprite.UpdateAnimationSet(sprite.Orientation.ToPacmanSpriteAnimationID().ToString());
        }

        internal RectangleF MouthBounds => new RectangleF(new PointF((float)Position.X - 1.5f, (float)Position.Y - 1.5f), new Size(3, 3));

        internal bool IsMoving => State is PacmanMovingState;
        internal bool IsWarping => State is PacmanWarpingState;

        private Direction Direction => sprite.Orientation;

        internal int FramesToDelayMotion { get; set; } = 0;

        internal bool IsFacingMazeEdge
        {
            get
            {
                switch (Direction)
                {
                    case Direction.Up:
                        return TilePosition.Y < GraphicsConstants.GridHeight / 2;
                    case Direction.Left:
                        return TilePosition.X < GraphicsConstants.GridWidth / 2;
                    case Direction.Down:
                        return TilePosition.Y >= GraphicsConstants.GridHeight / 2;
                    case Direction.Right:
                        return TilePosition.X >= GraphicsConstants.GridWidth / 2;
                    default:
                        throw new Exception("Unhandled direction.");
                }
            }
        }

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
            // Round position to nearest 2 pixels to help with collision
            Position = Position.RoundedToNearest(2);
        }

        internal Vector2 DirectionVelocity(Direction newDirection)
        {
            switch (newDirection)
            {
                case Direction.Up:
                    return new Vector2(0, Game.UpMultiplier * CurrentSpeed);
                case Direction.Down:
                    return new Vector2(0, Game.DownMultiplier * CurrentSpeed);
                case Direction.Left:
                    return new Vector2(-1 * CurrentSpeed, 0);
                case Direction.Right:
                    return new Vector2(1 * CurrentSpeed, 0);
                default:
                    throw new Exception("Unhandled direction.");
            }
        }

        internal override void Update(TimeSpan elapsedTime)
        {
            Vector2 tempVelocity = Velocity;
            if (FramesToDelayMotion > 0)
                Velocity = Vector2.Zero;
            base.Update(elapsedTime);
            if (FramesToDelayMotion > 0)
            {
                Velocity = tempVelocity;
                --FramesToDelayMotion;
            }
            if ((state is PacmanRespawningState || state is PacmanDyingState))
                sprite.RepeatAnimation = false;
            state.Update(sprite.AnimationFinished);
        }

        internal void ReturnToMovementState()
        {
            if (State is PacmanWarpingState)
            {
                State = new PacmanMovingState(this);
                WarpStartPosition = Point.Empty;
            }
        }

        internal void BeginRespawning(Action onRespawn) => State = new PacmanRespawningState(this, onRespawn);

        internal void BeginDeath(Action onDeath) => State = new PacmanDyingState(this, onDeath);

        internal void BeginWarping()
        {
            WarpStartPosition = TilePosition;
            State = new PacmanWarpingState(this);
        }

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
