namespace DP2D
{
    public class IdleState : IState
    {
        public IdleState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            player = stateMachine.player;
            animator = stateMachine.animator;
        }
        public override void OnEnter()
        {
            controller.ResetMoveVector();
        }

        public override void OnExit()
        {

        }

        public override void Tick()
        {
            
        }
    }
}
