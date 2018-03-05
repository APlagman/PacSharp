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
    }
}
