using System;
using System.Drawing;

namespace PacSharpApp.Graphics
{
    abstract class Sprite
    {
        private PaletteID palette = PaletteID.Blinky;

        internal abstract Image Image { get; }
        internal PaletteID Palette { private protected get => palette; set { palette = value; UpdatePalette(); } }
        internal Size Size => Image.Size;

        private protected abstract void UpdatePalette();
        protected internal virtual void Update(TimeSpan elapsedTime) {}
        internal bool Visible { get; set; } = true;

        internal abstract void RotateFlip(RotateFlipType rfType);
    }
}
