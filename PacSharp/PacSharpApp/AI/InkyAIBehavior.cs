using System;
using PacSharpApp.Objects;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    class InkyAIBehavior : GhostAIBehavior
    {
        internal InkyAIBehavior(PacmanObject pacman, GhostObject owner)
            : base(pacman, owner)
        {
        }

        internal override void Update(TimeSpan elapsedTime)
        {
            //throw new NotImplementedException();
        }
    }
}
