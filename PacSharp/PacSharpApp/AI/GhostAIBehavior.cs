using System;
using System.Drawing;
using System.Linq;
using PacSharpApp.Objects;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    abstract class GhostAIBehavior : AIBehavior
    {
        private protected readonly GhostObject owner;
        private protected readonly PacmanObject target;
        private protected readonly Maze level;
        private Point lastTilePos;
        private Direction nextDirection;

        protected GhostAIBehavior(GhostObject owner, PacmanObject target, Maze level)
        {
            this.owner = owner;
            this.target = target;
            this.level = level;
        }

        private protected abstract Point DestinationTile { get; }

        internal void ChangeDirection()
        {
            Direction chosen;
            var availableDirections =
                Enum.GetValues(typeof(Direction)).Cast<Direction>()
                .Where(dir => owner.CanTurnTo(level.Walls, owner.DirectionVelocity(dir)));
            if (availableDirections.Contains(owner.Direction.GetOpposite()))
                chosen = owner.Direction.GetOpposite();
            else
                chosen = availableDirections.First();
            owner.PerformTurn(chosen);
            nextDirection = chosen;
        }

        internal Direction ChooseNewDirection()
        {
            var available =
                Enum.GetValues(typeof(Direction)).Cast<Direction>()
                .Where(dir => dir != owner.Direction.GetOpposite() && owner.CanTurnToTile(level.Walls, NextTile(dir)));
            if (available.Count() == 0)
                return owner.Direction;
            if (!owner.IsFrightened && level.GhostLimitedIntersections.Contains(owner.TilePosition))
                available = available.Where(dir => dir != Direction.Up);
            double minDistance = available.Min(dir => DistanceToTarget(dir));
            var prioritized = available.Where(dir => DistanceToTarget(dir) == minDistance);

            return (prioritized.Count() == 1)
                ? prioritized.First()
                : prioritized.OrderBy(dir => Array.IndexOf(Enum.GetValues(typeof(Direction)), dir)).First();
        }

        private double DistanceToTarget(Direction dir)
        {
            Point nextTile = NextTile(dir);
            return Math.Sqrt(Math.Pow(DestinationTile.X - nextTile.X, 2) + Math.Pow(DestinationTile.Y - nextTile.Y, 2));
        }

        private Point NextTile(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return new Point(owner.TilePosition.X, owner.TilePosition.Y - 1);
                case Direction.Left:
                    return new Point(owner.TilePosition.X - 1, owner.TilePosition.Y);
                case Direction.Down:
                    return new Point(owner.TilePosition.X, owner.TilePosition.Y + 1);
                case Direction.Right:
                    return new Point(owner.TilePosition.X + 1, owner.TilePosition.Y);
                default:
                    throw new Exception("Unhandled direction.");
            }
        }

        internal override void Update(TimeSpan elapsedTime)
        {
            if (owner.TilePosition != lastTilePos && !owner.IsWarping)
            {
                lastTilePos = owner.TilePosition;
                nextDirection = ChooseNewDirection();
            }
            if ((owner.Velocity.X == 0 && owner.Velocity.Y == 0) || owner.CanTurnTo(level.Walls, owner.DirectionVelocity(nextDirection)))
                owner.PerformTurn(nextDirection);
        }

        internal static AIBehavior FromGhostType(GhostType type, GhostObject owner, PacmanObject target, Maze level)
        {
            switch (type)
            {
                case GhostType.Blinky:
                    return new BlinkyAIBehavior(owner, target, level);
                case GhostType.Pinky:
                    return new PinkyAIBehavior(owner, target, level);
                case GhostType.Inky:
                    return new InkyAIBehavior(owner, target, level);
                case GhostType.Clyde:
                    return new ClydeAIBehavior(owner, target, level);
                default:
                    throw new Exception("Unhandled ghost AI.");
            }
        }
    }
}
