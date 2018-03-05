using PacSharpApp.Graphics;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    class GhostObject : GameObject
    {
        private bool isScared = false;
        
        internal bool IsScared { get => isScared; set => IsScared = value; }

        internal GhostObject(GraphicsHandler handler)
            : base(GraphicsConstants.SpriteSize)
        {
        }
    }
}
