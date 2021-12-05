namespace DP2D
{
    public class FallState : IState
    {
        public FallState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            player = stateMachine.player;
            animator = stateMachine.animator;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.FallHash, true);
        }

        public override void OnExit()
        {
            animator.SetBool(player.FallHash, false);
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
            controller.HorizontalCollisionCheck();
            controller.VerticalMove();
            controller.HorizontalMove(player.MoveInput.x);
        }
    }
}
