using System;
using System.Collections.Generic;
using System.Drawing;
using PacSharpApp.Objects;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    abstract class GhostAIBehavior : AIBehavior
    {
        private protected GhostObject owner;
        private protected PacmanObject target;
        private protected IReadOnlyCollection<RectangleF> walls;
        private protected Vector2 respawnPoint;

        protected GhostAIBehavior(GhostObject owner, PacmanObject target, IReadOnlyCollection<RectangleF> walls, Vector2 respawnPoint)
        {
            this.owner = owner;
            this.target = target;
            this.walls = walls;
            this.respawnPoint = respawnPoint;
        }

        internal static AIBehavior FromGhostType(GhostType type, GhostObject owner, PacmanObject target, IReadOnlyCollection<RectangleF> walls, Vector2 respawnPoint)
        {
            switch (type)
            {
                case GhostType.Blinky:
                    return new BlinkyAIBehavior(owner, target, walls, respawnPoint);
                case GhostType.Pinky:
                    return new PinkyAIBehavior(owner, target, walls, respawnPoint);
                case GhostType.Inky:
                    return new InkyAIBehavior(owner, target, walls, respawnPoint);
                case GhostType.Clyde:
                    return new ClydeAIBehavior(owner, target, walls, respawnPoint);
                default:
                    throw new Exception("Unhandled ghost AI.");
            }
        }
    }
}
