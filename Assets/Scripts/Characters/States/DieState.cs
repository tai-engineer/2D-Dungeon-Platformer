namespace DP2D
{
    public class DieState : IState
    {
        public DieState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            player = stateMachine.player;
            animator = stateMachine.animator;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.DieHash, true);
            controller.SetHorizontalMovement(0f);
            player.Die();
        }

        public override void OnExit()
        {
            animator.SetBool(player.DieHash, false);
            player.Revive();
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
            controller.VerticalMove();
            if (controller.IsGrounded)
            {
                controller.ResetMoveVector();
            }
        }
    }
}
