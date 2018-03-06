using System;
using PacSharpApp.Graphics;
/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    enum GhostType
    {
        Blinky,
        Pinky,
        Inky,
        Clyde
    }

    internal static class GhostAIExtensions
    {
        internal static PaletteID ToPalette(this GhostType type)
        {
            switch (type)
            {
                case GhostType.Blinky:
                    return PaletteID.Blinky;
                case GhostType.Pinky:
                    return PaletteID.Pinky;
                case GhostType.Inky:
                    return PaletteID.Inky;
                case GhostType.Clyde:
                    return PaletteID.Clyde;
                default:
                    throw new Exception("Unhandled AI type.");
            }
        }
    }
}
