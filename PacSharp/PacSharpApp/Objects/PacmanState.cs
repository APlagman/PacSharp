using System.Windows.Forms;
using PacSharpApp.Utils;

namespace PacSharpApp.Objects
{
    internal abstract class PacmanState
    {
        private protected PacmanObject owner;

        private protected PacmanState(PacmanObject owner)
        {
            this.owner = owner;
        }

        internal abstract void HandleInput(InputHandler input);
    }

    internal class PacmanMovingState : PacmanState
    {
        internal PacmanMovingState(PacmanObject owner)
            : base(owner)
        { }

        internal override void HandleInput(InputHandler input)
        {
            bool turned = false;
            if (!turned && input.HeldKeys.Contains(Keys.W))
            {
                turned = owner.AttemptTurn(Direction.Up);
            }
            if (!turned && input.HeldKeys.Contains(Keys.A))
            {
                turned = owner.AttemptTurn(Direction.Left);
            }
            if (!turned && input.HeldKeys.Contains(Keys.S))
            {
                turned = owner.AttemptTurn(Direction.Down);
            }
            if (!turned && input.HeldKeys.Contains(Keys.D))
            {
                turned = owner.AttemptTurn(Direction.Right);
            }
            if (turned)
                owner.Position += owner.Velocity * 20;
        }
    }

    internal class PacmanWarpingState : PacmanState
    {
        internal PacmanWarpingState(PacmanObject owner)
            : base(owner)
        { }

        internal override void HandleInput(InputHandler input)
        { }
    }
}