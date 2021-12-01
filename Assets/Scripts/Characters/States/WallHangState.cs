using UnityEngine;
namespace DP2D
{
    public class WallHangState : IState
    {
        public WallHangState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.WallHangHash, true);
            controller.ResetMoveVector();
        }

        public override void OnExit()
        {
            animator.SetBool(player.WallHangHash, false);
        }

        public override void Tick()
        {

        }
    }
}
