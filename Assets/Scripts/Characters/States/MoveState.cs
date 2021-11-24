using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class MoveState : IState
    {
        CharacterPhysic _controller;
        PlayerCharacter _player;
        Animator _animator;
        public MoveState(CharacterPhysic controller, PlayerCharacter player, Animator animator)
        {
            _controller = controller;
            _animator = animator;
            _player = player;
        }
        public void OnEnter()
        {
            _animator.SetBool(_player.MoveHash, true);
        }

        public void OnExit()
        {
            _animator.SetBool(_player.MoveHash, false);
        }

        public void Tick()
        {
            _controller.HorizontalMove(_player.MoveInput.x);
        }
    }
}
