using System.Drawing;
using PacSharpApp.Graphics;

namespace PacSharpApp.Objects
{
    class PowerPellet : GameObject
    {
        internal PowerPellet(Size size, GraphicsHandler handler)
            : base(size)
        {
            handler.RegisterAnimatedSprite(this, new PowerPelletSprite() { Palette = PaletteID.Pellet });
        }
    }
}
