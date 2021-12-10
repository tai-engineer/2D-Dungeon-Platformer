using UnityEngine;
namespace DP2D
{
    public class CrouchState : IState
    {
        public CrouchState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.CrouchHash, true);
            controller.ResetMoveVector();
        }

        public override void OnExit()
        {
            animator.SetBool(player.CrouchHash, false);
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
        }
    }
}
