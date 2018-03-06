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
        private bool isAfraid = false;
        private readonly GhostSprite sprite;
        private readonly PaletteID normalPalette;

        internal GhostObject(GraphicsHandler handler, GhostType type, PacmanObject target)
            : base(GraphicsConstants.SpriteSize)
        {
            normalPalette = type.ToPalette();
            sprite = new GhostSprite() { Palette = normalPalette };
            handler.Register(this, sprite);
            Behavior = GhostAIBehavior.FromGhostType(type, target, this);
        }

        internal bool IsAfraid { get => isAfraid; set { IsAfraid = value; OnIsAfraidUpdated(); } }

        private void OnIsAfraidUpdated()
        {
            if (IsAfraid)
            {
                sprite.Palette = PaletteID.GhostAfraid;
            }
            else
            {
                sprite.Palette = normalPalette;
            }
        }
    }
}
