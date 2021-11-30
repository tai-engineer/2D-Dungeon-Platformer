namespace DP2D
{
    public class WallSlideJumpState : IState
    {
        public WallSlideJumpState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.JumpHash, true);
            controller.SetVerticalMovement(controller.MaxJumpSpeed);
            controller.SetHorizontalMovement(1f * -controller.FaceDirection.x);
        }

        public override void OnExit()
        {
            animator.SetBool(player.JumpHash, false);
        }

        public override void Tick()
        {
            controller.VerticalMove();
        }
    }
}
