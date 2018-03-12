using System;
using System.Drawing;
using PacSharpApp.Objects;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    class ClydeAIBehavior : GhostAIBehavior
    {
        internal ClydeAIBehavior(GhostObject owner, PacmanObject target, Maze level)
            : base(owner, target, level, GhostType.Clyde)
        { }

        internal override int ReleasePriority => 3;
        
        private protected override Point DestinationTile
        {
            get
            {
                if (owner.IsRespawning)
                    return level.GhostRespawnTile;
                else if (owner.IsChasing)
                    return target.TilePosition.DistanceTo(owner.TilePosition) < 8
                        ? level.GhostFavoriteTiles[GhostType.Clyde]
                        : target.TilePosition;
                else
                    return level.GhostFavoriteTiles[GhostType.Clyde];
            }
        }

        internal override bool GlobalPelletReleaseReached(int globalPelletCounter) => globalPelletCounter >= 32;

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
        }

        private protected override int NormalDotLimit(int levelNumber)
        {
            if (levelNumber == 0)
                return 60;
            else if (levelNumber == 1)
                return 50;
            return 0;
        }
    }
}
