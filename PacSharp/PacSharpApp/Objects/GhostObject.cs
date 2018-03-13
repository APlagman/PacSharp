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
        private const double GhostMovementSpeed = 0.088d;

        private bool isFrightened = false;
        private int levelNumber;
        private bool shouldScatter = true;
        private GhostState state;
        private readonly GhostSprite sprite;
        private readonly PaletteID normalPalette;
        private int pelletCounter = 0;

        internal GhostObject(GraphicsHandler handler, GhostType type, PacmanObject target, Maze level)
            : base(GraphicsConstants.SpriteSize)
        {
            normalPalette = type.ToPalette();
            sprite = new GhostSprite() { Palette = normalPalette };
            handler.Register(this, sprite);
            Behavior = GhostAIBehavior.FromGhostType(type, this, target, level);
            State = new GhostHomeState(this);
            PerformTurn(Direction.Down);
            Velocity = Vector2.Zero;
        }

        internal Direction Direction { get; set; }
        internal bool IsFrightened { get => isFrightened; private set => isFrightened = value; }
        internal bool IsChasing => State is GhostChaseState;
        internal bool IsRespawning => State is GhostRespawningState;
        internal bool IsScattering => State is GhostScatterState;
        internal bool IsWarping => State is GhostWarpingState;
        internal int LevelNumber { private get => levelNumber; set { levelNumber = value; } }
        internal bool ExitingGhostHouse { get; set; } = false;
        internal bool IsHome => State is GhostHomeState;

        internal int PelletCounter { get => pelletCounter; set { pelletCounter = value; (Behavior as GhostAIBehavior).PelletCounterUpdated(levelNumber); } }
        internal bool PelletCounterEnabled { get; set; } = false;

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
                if (IsWarping)
                {
                    if (LevelNumber == 0)
                        return GhostMovementSpeed * 0.40;
                    else if (LevelNumber < 4)
                        return GhostMovementSpeed * 0.45;
                    else
                        return GhostMovementSpeed * 0.50;
                }
                else if (IsFrightened)
                {
                    if (LevelNumber == 0)
                        return GhostMovementSpeed * 0.50;
                    else if (LevelNumber < 4)
                        return GhostMovementSpeed * 0.55;
                    else
                        return GhostMovementSpeed * 0.60;
                }
                else
                {
                    if (LevelNumber == 0)
                        return GhostMovementSpeed * (0.75 + CruiseElroyMode * 0.05);
                    else if (LevelNumber < 4)
                        return GhostMovementSpeed * (0.85 + CruiseElroyMode * 0.05);
                    else
                        return GhostMovementSpeed * (0.95 + CruiseElroyMode * 0.05);
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

        internal int CruiseElroyMode { get; set; } = 0;
        internal bool PauseTimers { get; set; } = false;

        private void OnShouldScatterChanged()
        {
            if (IsChasing || IsScattering)
            {
                if (ShouldScatter)
                    State = new GhostScatterState(this);
                else
                    State = new GhostChaseState(this);
            }
        }

        private void OnStateChanged(GhostState prevState)
        {
            Velocity = DirectionVelocity(Direction);
            if ((prevState is GhostChaseState || prevState is GhostScatterState) && !IsWarping)
                (Behavior as GhostAIBehavior).ChangeDirection();
            if (State is GhostFrightenedState)
            {
                IsFrightened = true;
                if ((State as GhostFrightenedState).TurnBlue)
                {
                    sprite.UpdateAnimationSet(GhostSprite.AnimationID.Afraid.ToString());
                    sprite.Palette = PaletteID.GhostAfraid;
                }
            }
            else
            {
                if (!(IsFrightened && IsWarping))
                    sprite.UpdateAnimationSet(Direction.ToGhostSpriteAnimationID().ToString());
                if (!IsWarping)
                    IsFrightened = false;
            }
            if (IsRespawning)
            {
                sprite.Palette = PaletteID.GhostRespawning;
                (Behavior as GhostAIBehavior).ChooseNewDirection(false, false);
            }
            else if (IsChasing || IsScattering || (IsWarping && !IsFrightened))
            {
                sprite.Palette = normalPalette;
            }
        }

        internal void EnteringGhostHouse()
        {
            State = new GhostHomeState(this);
            sprite.Palette = normalPalette;
            Velocity = Vector2.Zero;
        }

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            State.Update(elapsedTime, PauseTimers);
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
            if (Direction != dir)
                Position = Position.RoundedToNearest(2);
            Direction = dir;
            Velocity = DirectionVelocity(dir);
            if (!IsFrightened && sprite.CurrentAnimationSetID != Direction.ToGhostSpriteAnimationID().ToString())
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

        internal bool CanEnter(IReadOnlyCollection<RectangleF> walls, Point futureTile)
        {
            return !walls.Any(wall => wall.IntersectsWith(
                new RectangleF(
                    new PointF(futureTile.X * GraphicsConstants.TileWidth, futureTile.Y * GraphicsConstants.TileWidth),
                    GraphicsConstants.TileSize)));
        }

        internal void ReturnToMovementState()
        {
            State = (ShouldScatter) ? new GhostScatterState(this) : new GhostChaseState(this) as GhostState;
        }

        internal void BeginRespawning() => State = new GhostRespawningState(this);

        internal void BeginWarping()
        {
            WarpStartPosition = TilePosition;
            State = new GhostWarpingState(this,
                (State as GhostFrightenedState)?.UntilUnafraid ?? TimeSpan.MaxValue,
                (State as GhostFrightenedState)?.TurnBlue ?? false);
        }

        internal void EndWarping()
        {
            WarpStartPosition = Point.Empty;
            State = (IsFrightened
                ? (!(State as GhostWarpingState).UntilUnafraid.Equals(TimeSpan.MaxValue))
                    ? new GhostFrightenedState(this, (State as GhostWarpingState).TurnBlue, (State as GhostWarpingState).UntilUnafraid)
                    : new GhostFrightenedState(this, levelNumber, (State as GhostWarpingState).TurnBlue)
                : (ShouldScatter) ? new GhostScatterState(this) : new GhostChaseState(this) as GhostState);
        }

        internal void BecomeFrightened(bool turnBlue)
        {
            State = new GhostFrightenedState(this, levelNumber, turnBlue);
        }

        #region State
        abstract class GhostState
        {
            private protected static readonly ICollection<double> flashTimings = new List<double>() { 2.25, 2, 1.75, 1.5, 1.25, 1, 0.75, 0.5, 0.25 };

            private protected GhostObject owner;

            private protected GhostState(GhostObject owner)
            {
                this.owner = owner;
            }

            internal virtual void Update(TimeSpan elapsedTime, bool preventTimerUpdates) { }
        }

        class GhostChaseState : GhostState
        {
            internal GhostChaseState(GhostObject owner)
                : base(owner)
            { }
        }

        class GhostFrightenedState : GhostState
        {
            private TimeSpan untilUnafraid;

            internal GhostFrightenedState(GhostObject owner, int levelNumber, bool turnBlue)
                : base(owner)
            {
                untilUnafraid = FrightDuration(levelNumber);
                TurnBlue = turnBlue;
            }

            private TimeSpan FrightDuration(int levelNumber)
            {
                if (levelNumber == 0)
                    return TimeSpan.FromSeconds(6);
                else if (levelNumber == 1 || levelNumber == 5 || levelNumber == 9)
                    return TimeSpan.FromSeconds(5);
                else if (levelNumber == 2)
                    return TimeSpan.FromSeconds(4);
                else if (levelNumber == 3 || levelNumber == 13)
                    return TimeSpan.FromSeconds(3);
                else if (levelNumber < 8 || levelNumber == 10)
                    return TimeSpan.FromSeconds(2);
                else if (levelNumber < 16 || levelNumber == 17)
                    return TimeSpan.FromSeconds(1);
                else
                    return TimeSpan.MinValue;
            }

            internal GhostFrightenedState(GhostObject owner, bool turnBlue, TimeSpan untilUnafraid)
                : base(owner)
            {
                this.untilUnafraid = untilUnafraid;
                TurnBlue = turnBlue;
            }

            internal TimeSpan UntilUnafraid => untilUnafraid;
            internal bool TurnBlue { get; }

            internal override void Update(TimeSpan elapsedTime, bool preventTimerUpdates)
            {
                if (preventTimerUpdates)
                    return;
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
            private TimeSpan untilUnafraid;

            internal GhostWarpingState(GhostObject owner, TimeSpan frightenedTimeRemaining, bool turnBlue)
                : base(owner)
            {
                untilUnafraid = frightenedTimeRemaining;
                TurnBlue = turnBlue;
            }

            internal bool TurnBlue { get; }
            internal TimeSpan UntilUnafraid => untilUnafraid;

            internal override void Update(TimeSpan elapsedTime, bool preventTimerUpdates)
            {
                if (preventTimerUpdates)
                    return;
                if (UntilUnafraid == TimeSpan.MaxValue)
                    return;
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

        class GhostScatterState : GhostState
        {
            internal GhostScatterState(GhostObject owner)
                : base(owner)
            { }
        }

        class GhostHomeState : GhostState
        {
            internal GhostHomeState(GhostObject owner)
                : base(owner)
            { }
        }
        #endregion
    }
}
