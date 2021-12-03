using UnityEngine;
namespace DP2D
{
    public class WallClimbState : IState
    {
        public WallClimbState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.WallClimbHash, true);
            controller.IsClimbing = true;
        }

        public override void OnExit()
        {
            animator.SetBool(player.WallClimbHash, false);
        }

        public override void Tick()
        {

        }
    }
}
