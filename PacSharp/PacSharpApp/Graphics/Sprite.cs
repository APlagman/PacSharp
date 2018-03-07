using System;
using System.Drawing;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    abstract class Sprite
    {
        private PaletteID palette = PaletteID.Blinky;
        private Direction orientation = Direction.Right;

        internal abstract Image Image { get; }
        internal PaletteID Palette { get => palette; set { palette = value; UpdatePalette(); } }
        internal Size Size => Image.Size;

        private protected abstract void UpdatePalette();
        protected internal virtual void Update(TimeSpan elapsedTime) {}
        internal bool Visible { get; set; } = true;
        internal virtual int ZIndex => 0;
        internal Direction Orientation
        {
            get => orientation;
            set { Turn(value); orientation = value; }
        }

        private protected virtual void Turn(Direction value) => RotateFlip(GetRFType(orientation, value));

        private protected virtual RotateFlipType GetRFType(Direction previousOrientation, Direction newOrientation)
        {
            if (previousOrientation == newOrientation)
                return RotateFlipType.RotateNoneFlipNone;
            int deltaDegrees = newOrientation - previousOrientation;
            if (deltaDegrees < 0)
                deltaDegrees += 360;
            switch (deltaDegrees)
            {
                default:
                    throw new Exception("Bad degrees.");
                case 90:
                    return RotateFlipType.Rotate90FlipNone;
                case 180:
                    return RotateFlipType.Rotate180FlipNone;
                case 270:
                    return RotateFlipType.Rotate270FlipNone;
            }
        }

        internal abstract void RotateFlip(RotateFlipType rfType);
    }
}
