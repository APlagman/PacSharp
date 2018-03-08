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
        private const double NormalSpeed = 0.031d;
        private const double FrightenedSpeed = 0.0155d;
        private const double RespawningSpeed = 0.062d;

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

        internal GhostState State
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
        private double CurrentSpeed => (IsFrightened? FrightenedSpeed : (IsRespawning? RespawningSpeed : NormalSpeed));

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
            if (prevState is GhostChaseState || prevState is GhostScatterState)
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
            else if (IsChasing || IsScattering)
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
            if (ShouldScatter)
                State = new GhostScatterState(this);
            else
                State = new GhostChaseState(this);
        }
    }
}
