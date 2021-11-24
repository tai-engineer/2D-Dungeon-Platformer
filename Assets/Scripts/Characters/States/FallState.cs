using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class FallState : IState
    {
        PlayerCharacter _player;
        Animator _animator;
        CharacterPhysic _controller;
        public FallState(CharacterPhysic controller, PlayerCharacter player, Animator animator)
        {
            _controller = controller;
            _player = player;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetBool(_player.FallHash, true);
        }

        public void OnExit()
        {
            _animator.SetBool(_player.FallHash, false);
        }

        public void Tick()
        {
            _controller.VerticalMove();
            if (Mathf.Approximately(_controller.MoveVector.y, _controller.Gravity))
            {
                _controller.SetVerticalMovement(_controller.Gravity);
            }
        }
    }
}
