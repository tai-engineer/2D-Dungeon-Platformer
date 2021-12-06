using UnityEngine;
namespace DP2D
{
    public class WallSlideState : IState
    {
        bool _flip;
        bool _spriteFacingLeftOrigin;
        bool _flipOrigin;
        float _speedReduction = 0.2f;
        float _oldGravity;
        public WallSlideState(PlayerStateMachine stateMachine, bool spriteFacingLeftOrigin)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
            _spriteFacingLeftOrigin = spriteFacingLeftOrigin;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.WallSlideHash, true);
            _flipOrigin = controller.CurrentSpriteFlip;
            _flip = !_flipOrigin;

            _oldGravity = controller.Gravity;
            controller.Gravity *= _speedReduction;
        }

        public override void OnExit()
        {
            animator.SetBool(player.WallSlideHash, false);
            controller.SpriteFlip(_flipOrigin);
            controller.Gravity = _oldGravity;
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
            controller.VerticalMove();
            controller.HorizontalMove(player.MoveInput.x);
            // Character's backside must against the wall
            _flip = player.MoveInput.x > 0 ? !_spriteFacingLeftOrigin : player.MoveInput.x < 0 ? _spriteFacingLeftOrigin : _flip;
            controller.SpriteFlip(_flip);
        }
    }
}
