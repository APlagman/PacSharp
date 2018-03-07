using System;
using PacSharpApp.AI;
using PacSharpApp.Graphics;

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

        internal GhostObject(GraphicsHandler handler, GhostType type, PacmanObject target)
            : base(GraphicsConstants.SpriteSize)
        {
            normalPalette = type.ToPalette();
            sprite = new GhostSprite() { Palette = normalPalette };
            handler.Register(this, sprite);
            Behavior = GhostAIBehavior.FromGhostType(type, target, this);
            State = new NormalGhostState(this);
        }

        internal bool IsAfraid => State is AfraidGhostState;
        internal bool IsNormal => State is NormalGhostState;
        internal bool IsRespawning => State is RespawningGhostState;
        
        internal GhostState State { get => state; set { state = value; OnStateChanged(); } }

        private void OnStateChanged()
        {
            if (IsAfraid)
            {
                sprite.UpdateAnimationSet(GhostSprite.AnimationID.Afraid.ToString());
                sprite.Palette = PaletteID.GhostAfraid;
            }
            else
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
