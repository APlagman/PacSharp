using System;
using PacSharpApp.Objects;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    class BlinkyAIBehavior : GhostAIBehavior
    {
        internal BlinkyAIBehavior(PacmanObject pacman, GhostObject owner)
            : base(pacman, owner)
        {
        }

        internal override void Update(TimeSpan elapsedTime)
        {

        }
    }
}
