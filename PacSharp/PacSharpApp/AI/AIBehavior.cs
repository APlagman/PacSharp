using System;

namespace PacSharpApp.AI
{
    abstract class AIBehavior
    {
        internal abstract void Update(TimeSpan elapsedTime);
    }
}
