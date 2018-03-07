using System;
using PacSharpApp.Graphics;
using PacSharpApp.Properties;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    class ScoreObject : GameObject
    {
        internal ScoreObject(GraphicsHandler handler, int score)
            : base(GraphicsConstants.SpriteSize)
        {
            handler.SetStaticSprite(this, score.ToGraphicsID(), PaletteID.Text, Resources.Sprites, GraphicsConstants.SpriteWidth);
        }
    }

    static class IntScoreExtensions
    {
        public static GraphicsID ToGraphicsID(this int val)
        {
            switch (val)
            {
                case 200:
                    return GraphicsID.SpritePoints200;
                case 400:
                    return GraphicsID.SpritePoints400;
                case 800:
                    return GraphicsID.SpritePoints800;
                case 1600:
                    return GraphicsID.SpritePoints1600;
                default:
                    throw new Exception("Unhandled score.");
            }
        }
    }
}
