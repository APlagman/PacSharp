using System;
using PacSharpApp.Objects;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    abstract class GhostAIBehavior : AIBehavior
    {
        private protected PacmanObject pacman;
        private protected GhostObject owner;

        protected GhostAIBehavior(PacmanObject pacman, GhostObject owner)
        {
            this.pacman = pacman;
            this.owner = owner;
        }

        internal static AIBehavior FromGhostType(GhostType ghostAI, PacmanObject target, GhostObject owner)
        {
            switch (ghostAI)
            {
                case GhostType.Blinky:
                    return new BlinkyAIBehavior(target, owner);
                case GhostType.Pinky:
                    return new PinkyAIBehavior(target, owner);
                case GhostType.Inky:
                    return new InkyAIBehavior(target, owner);
                case GhostType.Clyde:
                    return new ClydeAIBehavior(target, owner);
                default:
                    throw new Exception("Unhandled ghost AI.");
            }
        }
    }
}
