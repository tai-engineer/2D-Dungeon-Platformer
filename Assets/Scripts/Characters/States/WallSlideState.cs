using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class WallSlideState : IState
    {
        bool _flip;
        bool _spriteFacingLeft;
        public WallSlideState(PlayerStateMachine stateMachine, bool spriteFacingLeft)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
            _spriteFacingLeft = spriteFacingLeft;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.WallSlideHash, true);
        }

        public override void OnExit()
        {
            animator.SetBool(player.WallSlideHash, false);
        }

        public override void Tick()
        {
            controller.VerticalMove();
            controller.HorizontalMove(player.MoveInput.x);
            // Character's backside must against the wall
            _flip = player.MoveInput.x > 0 ? !_spriteFacingLeft : player.MoveInput.x < 0 ? _spriteFacingLeft : _flip;
            controller.SpriteFlip(_flip);
        }
    }
}
