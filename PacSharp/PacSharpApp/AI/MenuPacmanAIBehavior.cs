using System;
using System.Collections.Generic;
using PacSharpApp.Objects;

namespace PacSharpApp.AI
{
    class MenuPacmanAIBehavior : AIBehavior
    {
        private readonly IDictionary<string, GameObject> objects;

        internal MenuPacmanAIBehavior(IDictionary<string, GameObject> objects)
        {
            this.objects = objects;
        }

        internal override void Update(TimeSpan elapsedTime)
        {
            if (objects.ContainsKey("EatenPellet"))
            {

            }
        }
    }
}
