using System;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    static class GhostSpriteAnimationExtensions
    {
        internal static GhostSprite.AnimationID ToGhostSpriteAnimationID(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return GhostSprite.AnimationID.NormalUp;
                case Direction.Down:
                    return GhostSprite.AnimationID.NormalDown;
                case Direction.Left:
                    return GhostSprite.AnimationID.NormalLeft;
                case Direction.Right:
                    return GhostSprite.AnimationID.NormalRight;
                default:
                    throw new Exception("Unhandled direction.");
            }
        }
    }
}
