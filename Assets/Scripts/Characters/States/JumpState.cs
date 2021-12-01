namespace DP2D
{
    public class JumpState : IState
    {
        public JumpState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            player = stateMachine.player;
            animator = stateMachine.animator;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.JumpHash, true);
            // Initial jump force
            controller.SetVerticalMovement(controller.MaxJumpSpeed);
        }

        public override void OnExit()
        {
            animator.SetBool(player.JumpHash, false);
        }

        public override void Tick()
        {
            controller.VerticalMove();
            controller.HorizontalCollisionCheck();
            controller.HorizontalMove(player.MoveInput.x);
        }
    }
}
