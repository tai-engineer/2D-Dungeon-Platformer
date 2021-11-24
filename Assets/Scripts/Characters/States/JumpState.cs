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

        float _initialJumpForce;
        public JumpState(CharacterPhysic controller, PlayerCharacter player, Animator animator, float jumpForce)
        {
            _controller = controller;
            _player = player;
            _animator = animator;
            _initialJumpForce = jumpForce;
        }
        public void OnEnter()
        {
            _animator.SetBool(_player.JumpHash, true);
            // Initial jump force
            _controller.SetVerticalMovement(_initialJumpForce);
            _controller.LandingPrepare();
        }

        public void OnExit()
        {
            _animator.SetBool(_player.JumpHash, false);
        }

        public void Tick()
        {
            _controller.VerticalMove();
        }
    }
}
