namespace DP2D
{
    public class MoveState : IState
    {
        public MoveState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.MoveHash, true);
        }

        public override void OnExit()
        {
            animator.SetBool(player.MoveHash, false);
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
            controller.HorizontalCollisionCheck();
            controller.HorizontalMove(player.MoveInput.x);
        }
    }
}
