namespace DP2D
{
    public class HitState : IState
    {
        public HitState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            player = stateMachine.player;
            animator = stateMachine.animator;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.HitHash, true);
            controller.ResetMoveVector();
        }

        public override void OnExit()
        {
            animator.SetBool(player.HitHash, false);
            controller.IsHit = false;
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
        }
    }
}
