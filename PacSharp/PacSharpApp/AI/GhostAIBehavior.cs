using System;
using System.Drawing;
using System.Linq;
using PacSharpApp.Graphics;
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
        private readonly GhostType type;

        protected GhostAIBehavior(GhostObject owner, PacmanObject target, Maze level, GhostType type)
        {
            this.owner = owner;
            this.target = target;
            this.level = level;
            this.type = type;
        }

        private protected abstract Point DestinationTile { get; }
        internal abstract int ReleasePriority { get; }
        private protected abstract int NormalDotLimit(int levelNumber);

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
            if (IsIntersection(owner.TilePosition))
                ChooseNewDirection(false);
            else if (IsIntersection(NextTile(owner.TilePosition, owner.Direction)))
                ChooseNewDirection();
        }

        internal void ChooseNewDirection(bool useNextTile = true, bool excludeOpposite = true)
        {
            Direction chosen;
            if (owner.ExitingGhostHouse)
            {
                owner.ExitingGhostHouse = false;
                chosen = Direction.Left;
            }
            else
            {
                var available =
                    Enum.GetValues(typeof(Direction)).Cast<Direction>()
                    .Where(dir => (!excludeOpposite || dir != owner.Direction.GetOpposite()) && owner.CanEnter(level.Walls, FutureTile(dir, useNextTile)));
                if (available.Count() == 0)
                    chosen = owner.Direction;
                else
                {
                    if (!owner.IsFrightened && level.GhostLimitedIntersections.Contains(useNextTile ? NextTile(owner.TilePosition, owner.Direction) : owner.TilePosition))
                        available = available.Where(dir => dir != Direction.Up);
                    if (available.Count() == 0)
                        chosen = owner.Direction; // Shouldn't normally be here
                    else
                    {
                        double minDistance = available.Min(dir => DistanceToTarget(dir, useNextTile));
                        var prioritized = available.Where(dir => DistanceToTarget(dir, useNextTile) == minDistance);

                        chosen = (prioritized.Count() == 1)
                            ? prioritized.First()
                            : prioritized.OrderBy(dir => Array.IndexOf(Enum.GetValues(typeof(Direction)), dir)).First();
                    }
                }
            }
            nextDirection = chosen;
        }

        internal void PelletCounterUpdated(int levelNumber)
        {
            if (owner.IsHome && owner.PelletCounter > NormalDotLimit(levelNumber))
                owner.ExitingGhostHouse = true;
        }

        private double DistanceToTarget(Direction dir, bool useNextTile = true) =>
            FutureTile(dir, useNextTile).DistanceTo(DestinationTile);

        private Point FutureTile(Direction dir, bool useNextTile = true)
        {
            Point reference = (useNextTile ? NextTile(owner.TilePosition, owner.Direction) : owner.TilePosition);
            switch (dir)
            {
                case Direction.Up:
                    return new Point(reference.X, reference.Y - 1);
                case Direction.Left:
                    return new Point(reference.X - 1, reference.Y);
                case Direction.Down:
                    return new Point(reference.X, reference.Y + 1);
                case Direction.Right:
                    return new Point(reference.X + 1, reference.Y);
                default:
                    throw new Exception("Unhandled direction.");
            }
        }

        private Point NextTile(Point reference, Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return new Point(reference.X, reference.Y - 1);
                case Direction.Left:
                    return new Point(reference.X - 1, reference.Y);
                case Direction.Down:
                    return new Point(reference.X, reference.Y + 1);
                case Direction.Right:
                    return new Point(reference.X + 1, reference.Y);
                default:
                    throw new Exception("Unhandled direction.");
            }
        }

        internal override void Update(TimeSpan elapsedTime)
        {
            if (owner.IsHome)
            {
                Vector2 pos = owner.Position.RoundedToNearest(1);
                if (owner.ExitingGhostHouse)
                {
                    if (pos.X == level.GhostHouseEntrance.X)
                        MoveVerticallyToExit(pos);
                    else
                        MoveHorizontallyToExit(pos);
                }
                else if (pos.Y == (level.GhostRespawnTile.Y + 0.5) * GraphicsConstants.TileWidth)
                    MoveToSpawnPosition(pos);
                return;
            }
            if (owner.IsRespawning)
            {
                Vector2 pos = owner.Position.RoundedToNearest(1);
                if (pos.X == level.GhostHouseEntrance.X && pos.Y == level.GhostHouseEntrance.Y)
                {
                    owner.PerformTurn(Direction.Down);
                    return;
                }
                else if (pos.X == level.GhostHouseEntrance.X && pos.Y == (level.GhostRespawnTile.Y + 0.5) * GraphicsConstants.TileWidth)
                {
                    owner.EnteringGhostHouse();
                    return;
                }
            }
            if (owner.TilePosition != lastTilePos && !owner.IsWarping)
            {
                lastTilePos = owner.TilePosition;
                if (IsIntersection(NextTile(owner.TilePosition, owner.Direction)) || owner.ExitingGhostHouse)
                    ChooseNewDirection();
            }
            if ((owner.Velocity.X == 0 && owner.Velocity.Y == 0) || owner.CanTurnTo(level.Walls, owner.DirectionVelocity(nextDirection)))
                owner.PerformTurn(nextDirection);
            if (EnsureNotMovingThroughWalls())
            {
                ChooseNewDirection(false);
                owner.PerformTurn(nextDirection);
            }
        }

        private bool EnsureNotMovingThroughWalls()
        {
            return !owner.IsHome
                && !(owner.IsRespawning
                    && owner.Position.RoundedToNearest(1).X == level.GhostHouseEntrance.X
                    && owner.Position.RoundedToNearest(1).Y <= (level.GhostRespawnTile.Y + 0.5) * GraphicsConstants.TileWidth
                    && owner.Direction == Direction.Down)
                && !owner.CanTurnTo(level.Walls, owner.Velocity);
        }

        private void MoveVerticallyToExit(Vector2 pos)
        {
            if (pos.Y == level.GhostHouseEntrance.Y)
            {
                owner.Position.X = level.GhostHouseEntrance.X - 0.5;
                owner.Position.Y = pos.Y;
                LeftGhostHouse();
            }
            else
                owner.PerformTurn(Direction.Up);
        }

        private void MoveHorizontallyToExit(Vector2 pos)
        {
            owner.PerformTurn(level.GhostHouseEntrance.X < pos.X ? Direction.Left : Direction.Right);
        }

        private void MoveToSpawnPosition(Vector2 pos)
        {
            if (pos.X == level.GhostSpawns[type].X + GraphicsConstants.TileWidth / 2)
            {
                owner.PerformTurn(Direction.Down);
                owner.Velocity = Vector2.Zero;
            }
            else
            {
                owner.PerformTurn(level.GhostSpawns[type].X + GraphicsConstants.TileWidth / 2 < pos.X ? Direction.Left : Direction.Right);
                owner.Velocity.Y = 0;
            }
        }

        private void LeftGhostHouse()
        {
            owner.ExitingGhostHouse = false;
            owner.PerformTurn(Direction.Left);
            owner.ReturnToMovementState();
            ChooseNewDirection();
        }

        private bool IsIntersection(Point point)
        {
            var testDirections =
                Enum.GetValues(typeof(Direction)).Cast<Direction>()
                .Where(dir => dir != owner.Direction && dir != owner.Direction.GetOpposite());
            if (!owner.IsFrightened && level.GhostLimitedIntersections.Contains(point))
                testDirections = testDirections.Where(dir => dir != Direction.Up);
            int availableDirections = testDirections.Count(dir => IsNotBlocked(point, dir));
            return (availableDirections > 0);
        }

        private bool IsNotBlocked(Point point, Direction dir)
        {
            return !level.Walls.Any(wall => wall.IntersectsWith(
                            new RectangleF(
                                new PointF(NextTile(point, dir).X * GraphicsConstants.TileWidth, NextTile(point, dir).Y * GraphicsConstants.TileWidth),
                                GraphicsConstants.TileSize)));
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

        internal abstract bool GlobalPelletReleaseReached(int globalPelletCounter);
    }
}
