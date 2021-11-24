using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class LandState : IState
    {
        PlayerCharacter _player;
        Animator _animator;
        public LandState(PlayerCharacter player, Animator animator)
        {
            _player = player;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetBool(_player.LandHash, true);
        }

        public void OnExit()
        {
            _animator.SetBool(_player.LandHash, false);
        }

        public void Tick() { }
    }
}
