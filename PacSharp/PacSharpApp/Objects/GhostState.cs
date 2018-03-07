using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    abstract class GhostState
    {
        private protected GhostObject owner;

        private protected GhostState(GhostObject owner)
        {
            this.owner = owner;
        }

        internal abstract void Update(TimeSpan elapsedTime);
    }

    class NormalGhostState : GhostState
    {
        internal NormalGhostState(GhostObject owner)
            : base(owner)
        { }

        internal override void Update(TimeSpan elapsedTime)
        { }
    }

    class AfraidGhostState : GhostState
    {
        private static readonly ICollection<double> flashTimings = new List<double>() { 2, 1.75, 1.5, 1.25, 1, 0.75, 0.5, 0.25 };
        private static readonly TimeSpan afraidDuration = TimeSpan.FromSeconds(8);

        private TimeSpan untilUnafraid = afraidDuration;

        internal AfraidGhostState(GhostObject owner)
            : base(owner)
        { }

        internal override void Update(TimeSpan elapsedTime)
        {
            if (untilUnafraid < elapsedTime)
                owner.State = new NormalGhostState(owner);
            else
            {
                TimeSpan previousRemaining = untilUnafraid;
                untilUnafraid -= elapsedTime;
                if (ShouldFlash(previousRemaining))
                    owner.Flash();
            }
        }

        private bool ShouldFlash(TimeSpan previousRemaining)
        {
            return flashTimings.Any(
                timing => previousRemaining.TotalSeconds > timing
                       && untilUnafraid.TotalSeconds < timing);
        }
    }

    class RespawningGhostState : GhostState
    {
        internal RespawningGhostState(GhostObject owner)
            : base(owner)
        { }

        internal override void Update(TimeSpan elapsedTime)
        { }
    }
}
