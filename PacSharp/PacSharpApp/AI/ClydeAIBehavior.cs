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
        {
        }

        private protected override Point DestinationTile
            => (owner.IsRespawning) ? level.GhostRespawnTile : level.GhostFavoriteTiles[GhostType.Clyde];

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
        }
    }
}
