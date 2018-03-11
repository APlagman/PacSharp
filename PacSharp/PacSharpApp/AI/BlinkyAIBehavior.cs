using System;
using System.Drawing;
using PacSharpApp.Objects;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    class BlinkyAIBehavior : GhostAIBehavior
    {
        private static readonly Point FavoredTile = new Point(0, 3);

        internal BlinkyAIBehavior(GhostObject owner, PacmanObject target, Maze level)
            : base(owner, target, level, GhostType.Blinky)
        { }

        internal override int ReleasePriority => 0;

        private protected override Point DestinationTile
        {
            get
            {
                if (owner.IsRespawning)
                    return level.GhostRespawnTile;
                else if (owner.IsChasing)
                    return target.TilePosition;
                else
                    return level.GhostFavoriteTiles[GhostType.Blinky];
            }
        }

        internal override bool GlobalPelletReleaseReached(int globalPelletCounter) => true;

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
        }

        private protected override int NormalDotLimit(int levelNumber) => 0;
    }
}
