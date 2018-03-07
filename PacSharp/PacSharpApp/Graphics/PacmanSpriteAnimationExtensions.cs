using System;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    static class PacmanSpriteAnimationExtensions
    {
        internal static PacmanSprite.AnimationID ToPacmanSpriteAnimationID(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return PacmanSprite.AnimationID.MovingUp;
                case Direction.Down:
                    return PacmanSprite.AnimationID.MovingDown;
                case Direction.Left:
                    return PacmanSprite.AnimationID.MovingLeft;
                case Direction.Right:
                    return PacmanSprite.AnimationID.MovingRight;
                default:
                    throw new Exception("Unhandled direction.");
            }
        }
    }
}
