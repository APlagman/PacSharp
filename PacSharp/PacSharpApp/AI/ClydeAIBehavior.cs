﻿using System;
using System.Collections.Generic;
using System.Drawing;
using PacSharpApp.Objects;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.AI
{
    class ClydeAIBehavior : GhostAIBehavior
    {
        internal ClydeAIBehavior(GhostObject owner, PacmanObject target, IReadOnlyCollection<RectangleF> walls, Vector2 respawnPoint)
            : base(owner, target, walls, respawnPoint)
        {
        }

        internal override void Update(TimeSpan elapsedTime)
        {
            //throw new NotImplementedException();
        }
    }
}
