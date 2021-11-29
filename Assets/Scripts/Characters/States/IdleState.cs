using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class IdleState : IState
    {
        CharacterPhysic _controller;
        PlayerCharacter _player;
        Animator _animator;
        public IdleState(CharacterPhysic controller, PlayerCharacter player, Animator animator)
        {
            _controller = controller;
            _player = player;
            _animator = animator;
        }
        public void OnEnter()
        {
            _controller.ResetMoveVector();
        }

        public void OnExit()
        {

        }

        public void Tick()
        {
            
        }
    }
}
