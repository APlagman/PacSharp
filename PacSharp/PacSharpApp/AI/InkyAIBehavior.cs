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
            : base(owner, target, level)
        {
        }

        private protected override Point DestinationTile
            => (owner.IsRespawning) ? level.GhostRespawnTile : level.GhostFavoriteTiles[GhostType.Inky];

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
        }
    }
}
