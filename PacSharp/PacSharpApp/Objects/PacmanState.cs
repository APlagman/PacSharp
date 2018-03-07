using System;
using System.Windows.Forms;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Objects
{
    internal abstract class PacmanState
    {
        private protected PacmanObject owner;

        private protected PacmanState(PacmanObject owner)
        {
            this.owner = owner;
        }

        public PacmanEatingState Score { get; internal set; }

        internal virtual void HandleInput(InputHandler input) { }
        internal virtual void Update(bool animationFinished) { }
    }

    internal class PacmanMovingState : PacmanState
    {
        internal PacmanMovingState(PacmanObject owner)
            : base(owner)
        { }

        internal override void HandleInput(InputHandler input)
        {
            if (owner.PreventMovement)
                return;
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
    }

    internal class PacmanRespawningState : PacmanState
    {
        private readonly Action onRespawn;

        internal PacmanRespawningState(PacmanObject owner, Action onRespawn)
            : base(owner)
        {
            this.onRespawn = onRespawn;
            owner.PreventMovement = true;
        }

        internal override void Update(bool animationFinished)
        {
            if (animationFinished)
                onRespawn();
        }
    }

    internal class PacmanDyingState : PacmanState
    {
        internal PacmanDyingState(PacmanObject owner)
            : base(owner)
        {
            owner.PreventMovement = true;
        }
    }

    internal class PacmanEatingState : PacmanState
    {
        private protected PacmanEatingState(PacmanObject owner)
            : base(owner)
        { }
    }
}