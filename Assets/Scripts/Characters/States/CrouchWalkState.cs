using UnityEngine;
namespace DP2D
{
    public class CrouchWalkState : IState
    {
        const float speedMultiplier = 0.5f;
        readonly float _crouchSpeed;
        public CrouchWalkState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
            _crouchSpeed = speedMultiplier * controller.MaxGroundSpeed;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.CrouchWalkHash, true);
        }

        public override void OnExit()
        {
            animator.SetBool(player.CrouchWalkHash, false);
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
            controller.HorizontalCollisionCheck();
            controller.HorizontalMove(player.MoveInput.x, _crouchSpeed);
        }
    }
}
