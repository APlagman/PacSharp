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
            : base(owner, target, level)
        {
        }

        private protected override Point DestinationTile
            => (owner.IsRespawning) ? level.GhostRespawnTile : level.GhostFavoriteTiles[GhostType.Pinky];

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
        }
    }
}
