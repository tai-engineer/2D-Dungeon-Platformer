using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class LandState : IState
    {
        PlayerCharacter _player;
        Animator _animator;
        CharacterPhysic _controller;
        public LandState(CharacterPhysic controller, PlayerCharacter player, Animator animator)
        {
            _controller = controller;
            _player = player;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetBool(_player.LandHash, true);
            _controller.LandingPrepare();
            _controller.ResetMoveVector();
        }

        public void OnExit()
        {
            _animator.SetBool(_player.LandHash, false);
        }

        public void Tick()
        {
        }
    }
}
