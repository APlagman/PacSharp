using System;
using System.Drawing;
using PacSharpApp.Objects;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    class InkyAIBehavior : GhostAIBehavior
    {
        private GhostObject reference;

        internal InkyAIBehavior(GhostObject owner, PacmanObject target, Maze level)
            : base(owner, target, level, GhostType.Inky)
        { }

        internal override int ReleasePriority => 2;

        private protected override Point DestinationTile
        {
            get
            {
                if (owner.IsRespawning)
                    return level.GhostHouseEntrance.ToTilePoint();
                else if (owner.IsChasing)
                {
                    Point dest = target.TilePosition;
                    switch (target.Orientation)
                    {
                        case Direction.Down:
                            dest.Y += 2;
                            break;
                        case Direction.Up:
                            dest.Y -= 2;
                            dest.X -= 2; // Replicating original game bug
                            break;
                        case Direction.Left:
                            dest.X -= 2;
                            break;
                        case Direction.Right:
                            dest.X += 2;
                            break;
                        default:
                            throw new Exception("Unhandled direction.");
                    }
                    dest.X -= (reference.TilePosition.X - dest.X);
                    dest.Y -= (reference.TilePosition.Y - dest.Y);
                    return dest;
                }
                else
                    return level.GhostFavoriteTiles[GhostType.Inky];
            }
        }

        internal override bool GlobalPelletReleaseReached(int globalPelletCounter) => globalPelletCounter >= 17;

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
        }

        private protected override int NormalDotLimit(int levelNumber)
        {
            if (levelNumber == 0)
                return 30;
            return 0;
        }

        internal GhostObject Reference { get => reference; set => reference = value; }
    }
}
