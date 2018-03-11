using System;
using System.Drawing;
using PacSharpApp.Objects;

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
            => (owner.IsRespawning) ? level.GhostRespawnTile : level.GhostFavoriteTiles[GhostType.Pinky];

        internal override bool GlobalPelletReleaseReached(int globalPelletCounter) => globalPelletCounter >= 7;

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
        }

        private protected override int NormalDotLimit(int levelNumber) => 0;
    }
}
