using System;
using System.Drawing;
using PacSharpApp.Objects;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    class InkyAIBehavior : GhostAIBehavior
    {
        internal InkyAIBehavior(GhostObject owner, PacmanObject target, Maze level)
            : base(owner, target, level, GhostType.Inky)
        { }

        internal override int ReleasePriority => 2;

        private protected override Point DestinationTile
            => (owner.IsRespawning) ? level.GhostRespawnTile : level.GhostFavoriteTiles[GhostType.Inky];

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
    }
}
