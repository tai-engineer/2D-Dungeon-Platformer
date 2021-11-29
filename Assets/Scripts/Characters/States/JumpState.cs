using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class JumpState : IState
    {
        CharacterPhysic _controller;
        PlayerCharacter _player;
        Animator _animator;
        public JumpState(CharacterPhysic controller, PlayerCharacter player, Animator animator)
        {
            _controller = controller;
            _player = player;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetBool(_player.JumpHash, true);
            // Initial jump force
            _controller.SetVerticalMovement(_controller.MaxJumpSpeed);
        }

        public void OnExit()
        {
            _animator.SetBool(_player.JumpHash, false);
        }

        public void Tick()
        {
            _controller.VerticalMove();
            _controller.HorizontalMove(_player.MoveInput.x);
        }
    }
}
