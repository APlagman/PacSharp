using System;
using System.Collections.Generic;
using System.Drawing;
using PacSharpApp.AI;
using PacSharpApp.Graphics;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    class GhostObject : GameObject
    {
        private GhostState state;
        private readonly GhostSprite sprite;
        private readonly PaletteID normalPalette;

        internal GhostObject(GraphicsHandler handler, GhostType type, PacmanObject target, IReadOnlyCollection<RectangleF> walls, Vector2 respawnPoint)
            : base(GraphicsConstants.SpriteSize)
        {
            normalPalette = type.ToPalette();
            sprite = new GhostSprite() { Palette = normalPalette };
            handler.Register(this, sprite);
            Behavior = GhostAIBehavior.FromGhostType(type, this, target, walls, respawnPoint);
            State = new GhostNormalState(this);
        }

        internal bool IsAfraid => State is GhostAfraidState;
        internal bool IsNormal => State is GhostNormalState;
        internal bool IsRespawning => State is GhostRespawningState;
        
        internal GhostState State { get => state; set { state = value; OnStateChanged(); } }

        private void OnStateChanged()
        {
            if (IsAfraid)
            {
                sprite.UpdateAnimationSet(GhostSprite.AnimationID.Afraid.ToString());
                sprite.Palette = PaletteID.GhostAfraid;
            }
            else if (IsRespawning)
            {
                sprite.UpdateAnimationSet(sprite.Orientation.ToGhostSpriteAnimationID().ToString());
                sprite.Palette = PaletteID.GhostRespawning;
            }
            else if (IsNormal)
            {
                sprite.UpdateAnimationSet(sprite.Orientation.ToGhostSpriteAnimationID().ToString());
                sprite.Palette = normalPalette;
            }
        }

        internal override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            State.Update(elapsedTime);
        }

        internal void Flash()
        {
            if (sprite.Palette == PaletteID.GhostAfraid)
                sprite.Palette = PaletteID.GhostWhite;
            else
                sprite.Palette = PaletteID.GhostAfraid;
        }
    }
}
