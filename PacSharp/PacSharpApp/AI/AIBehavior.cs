using System;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    abstract class AIBehavior
    {
        internal abstract void Update(TimeSpan elapsedTime);
    }
}
