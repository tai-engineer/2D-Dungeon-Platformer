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
            controller.Box2D.enabled = false;
        }

        public override void OnExit()
        {
            animator.SetBool(player.WallClimbHash, false);
            controller.gameObject.transform.position = controller.GetClimbStandPosition();
            controller.Box2D.enabled = true;
        }

        public override void Tick()
        {

        }
    }
}
