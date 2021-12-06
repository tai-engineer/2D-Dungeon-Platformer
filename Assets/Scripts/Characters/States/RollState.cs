using UnityEngine;
namespace DP2D
{
    public class RollState : IState
    {
        float _direction;
        const float _scale = 0.1f;
        public RollState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.RollHash, true);
            controller.IsRolling = true;
            _direction = controller.FaceDirection.x;
        }

        public override void OnExit()
        {
            animator.SetBool(player.RollHash, false);
            controller.IsRolling = false;
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
            controller.HorizontalCollisionCheck();
            controller.SetHorizontalMovement(_direction * controller.RollDistance * _scale);
        }
    }
}
