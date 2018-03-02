using System;
using System.Drawing;

namespace PacSharpApp.Graphics
{
    abstract class Sprite
    {
        private protected PaletteID palette = PaletteID.Blinky;
        private protected readonly Bitmap source;

        private protected Sprite(Bitmap source)
        {
            this.source = source;
        }

        internal abstract Image Image { get; }
        internal PaletteID Palette { private protected get => palette; set { palette = value; UpdatePalette(); } }
        internal Size Size => Image.Size;

        private protected abstract void UpdatePalette();
        protected internal virtual void Update(TimeSpan elapsedTime) {}
    }
}
