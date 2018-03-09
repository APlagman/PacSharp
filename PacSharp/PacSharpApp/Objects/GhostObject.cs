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
    class GhostObject : GameObject
    {
        private int levelNumber;
        private bool shouldScatter = true;
        private GhostState state;
        private readonly GhostSprite sprite;
        private readonly PaletteID normalPalette;

        internal GhostObject(GraphicsHandler handler, GhostType type, PacmanObject target, Maze level)
            : base(GraphicsConstants.SpriteSize)
        {
            normalPalette = type.ToPalette();
            sprite = new GhostSprite() { Palette = normalPalette };
            handler.Register(this, sprite);
            Behavior = GhostAIBehavior.FromGhostType(type, this, target, level);
            State = new GhostScatterState(this);
        }

        internal Direction Direction { get; set; }
        internal bool IsFrightened => State is GhostFrightenedState;
        internal bool IsChasing => State is GhostChaseState;
        internal bool IsRespawning => State is GhostRespawningState;
        internal bool IsScattering => State is GhostScatterState;
        internal bool IsWarping => State is GhostWarpingState;
        internal int LevelNumber { private get => levelNumber; set { levelNumber = value; } }

        private GhostState State
        {
            get => state;
            set
            {
                GhostState prevState = state;
                state = value;
                OnStateChanged(prevState);
            }
        }

        internal bool ShouldScatter
        {
            get => shouldScatter;
            set
            {
                shouldScatter = value;
                OnShouldScatterChanged();
            }
        }

        private double CurrentSpeed
        {
            get
            {
                if (IsFrightened)
                {
                    if (LevelNumber == 0)
                        return PacSharpGame.MovementSpeed * 0.50;
                    else if (LevelNumber < 4)
                        return PacSharpGame.MovementSpeed * 0.55;
                    else
                        return PacSharpGame.MovementSpeed * 0.60;
                }
                else if (IsWarping)
                {
                    if (LevelNumber == 0)
                        return PacSharpGame.MovementSpeed * 0.40;
                    else if (LevelNumber < 4)
                        return PacSharpGame.MovementSpeed * 0.45;
                    else
                        return PacSharpGame.MovementSpeed * 0.50;
                }
                else
                {
                    if (LevelNumber == 0)
                        return PacSharpGame.MovementSpeed * 0.75;
                    else if (LevelNumber < 4)
                        return PacSharpGame.MovementSpeed * 0.85;
                    else
                        return PacSharpGame.MovementSpeed * 0.95;
                }
            }
        }

        internal Point WarpStartPosition { get; private set; } = Point.Empty;
        
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

        private void OnShouldScatterChanged()
        {
            if (IsChasing || IsScattering)
            {
                if (ShouldScatter && !IsScattering)
                    State = new GhostScatterState(this);
                else if (!IsChasing)
                    State = new GhostChaseState(this);
            }
        }

        private void OnStateChanged(GhostState prevState)
        {
            Velocity = DirectionVelocity(Direction);
            if ((prevState is GhostChaseState || prevState is GhostScatterState) && !IsWarping)
                (Behavior as GhostAIBehavior).ChangeDirection();
            if (IsFrightened && (State as GhostFrightenedState).TurnBlue)
            {
                sprite.UpdateAnimationSet(GhostSprite.AnimationID.Afraid.ToString());
                sprite.Palette = PaletteID.GhostAfraid;
            }
            else
                sprite.UpdateAnimationSet(Direction.ToGhostSpriteAnimationID().ToString());
            if (IsRespawning)
            {
                sprite.Palette = PaletteID.GhostRespawning;
            }
            else if (IsChasing || IsScattering || IsWarping)
            {
                sprite.Palette = normalPalette;
            }
        }

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            State.Update(elapsedTime);
        }

        internal void Flash()
        {
            if (sprite.Palette == PaletteID.GhostAfraid)
                sprite.Palette = PaletteID.GhostWhite;
            else if (sprite.Palette == PaletteID.GhostWhite)
                sprite.Palette = PaletteID.GhostAfraid;
        }

        internal virtual void PerformTurn(Direction dir)
        {
            Direction = dir;
            Velocity = DirectionVelocity(dir);
            if (!IsFrightened)
                sprite.UpdateAnimationSet(Direction.ToGhostSpriteAnimationID().ToString());
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

        internal bool CanTurnToTile(IReadOnlyCollection<RectangleF> walls, Point nextTile)
        {
            return !walls.Any(wall => wall.IntersectsWith(
                new RectangleF(
                    new PointF(nextTile.X * GraphicsConstants.TileWidth, nextTile.Y * GraphicsConstants.TileWidth),
                    GraphicsConstants.TileSize)));
        }

        internal void ReturnToMovementState()
        {
            State = (ShouldScatter) ? new GhostScatterState(this) : new GhostChaseState(this) as GhostState;
            WarpStartPosition = Point.Empty;
        }

        internal void BeginRespawning() => State = new GhostRespawningState(this);

        internal void BeginWarping()
        {
            WarpStartPosition = TilePosition;
            State = new GhostWarpingState(this);
        }

        internal void BecomeFrightened(bool turnBlue) => State = new GhostFrightenedState(this, turnBlue);

        #region State
        abstract class GhostState
        {
            private protected GhostObject owner;

            private protected GhostState(GhostObject owner)
            {
                this.owner = owner;
            }

            internal virtual void Update(TimeSpan elapsedTime) { }
        }

        class GhostChaseState : GhostState
        {
            internal GhostChaseState(GhostObject owner)
                : base(owner)
            { }
        }

        class GhostFrightenedState : GhostState
        {
            private static readonly ICollection<double> flashTimings = new List<double>() { 2, 1.75, 1.5, 1.25, 1, 0.75, 0.5, 0.25 };
            private static readonly TimeSpan afraidDuration = TimeSpan.FromSeconds(8);

            private TimeSpan untilUnafraid = afraidDuration;

            internal GhostFrightenedState(GhostObject owner, bool turnBlue)
                : base(owner)
            {
                TurnBlue = turnBlue;
            }

            internal bool TurnBlue { get; }

            internal override void Update(TimeSpan elapsedTime)
            {
                if (untilUnafraid <= elapsedTime)
                    owner.ReturnToMovementState();
                else
                {
                    TimeSpan previousRemaining = untilUnafraid;
                    untilUnafraid -= elapsedTime;
                    if (ShouldFlash(previousRemaining))
                        owner.Flash();
                }
            }

            private bool ShouldFlash(TimeSpan previousRemaining)
            {
                if (!TurnBlue)
                    return false;
                return flashTimings.Any(
                    timing => previousRemaining.TotalSeconds > timing
                           && untilUnafraid.TotalSeconds <= timing);
            }
        }

        class GhostRespawningState : GhostState
        {
            internal GhostRespawningState(GhostObject owner)
                : base(owner)
            { }
        }

        class GhostWarpingState : GhostState
        {
            internal GhostWarpingState(GhostObject owner)
                : base(owner)
            { }
        }

        class GhostScatterState : GhostState
        {
            internal GhostScatterState(GhostObject owner)
                : base(owner)
            { }
        }
        #endregion
    }
}
