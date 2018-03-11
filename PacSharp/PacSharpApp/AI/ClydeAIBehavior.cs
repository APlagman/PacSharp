using System;
using System.Drawing;
using PacSharpApp.Objects;

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
            => (owner.IsRespawning) ? level.GhostRespawnTile : level.GhostFavoriteTiles[GhostType.Clyde];

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
