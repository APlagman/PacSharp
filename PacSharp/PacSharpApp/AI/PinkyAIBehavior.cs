using System;
using System.Drawing;
using PacSharpApp.Objects;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    class PinkyAIBehavior : GhostAIBehavior
    {
        internal PinkyAIBehavior(GhostObject owner, PacmanObject target, Maze level)
            : base(owner, target, level, GhostType.Pinky)
        { }

        internal override int ReleasePriority => 1;

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
                            dest.Y += 4;
                            break;
                        case Direction.Up:
                            dest.Y -= 4;
                            dest.X -= 4; // Replicating original game bug
                            break;
                        case Direction.Left:
                            dest.X -= 4;
                            break;
                        case Direction.Right:
                            dest.X += 4;
                            break;
                        default:
                            throw new Exception("Unhandled direction.");
                    }
                    return dest;
                }
                else
                    return level.GhostFavoriteTiles[GhostType.Pinky];
            }
        }

        internal override bool GlobalPelletReleaseReached(int globalPelletCounter) => globalPelletCounter >= 7;

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
        }

        private protected override int NormalDotLimit(int levelNumber) => 0;
    }
}
