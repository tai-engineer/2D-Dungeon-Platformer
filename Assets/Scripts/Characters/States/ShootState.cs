using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class ShootState : IState
    {
        CharacterPhysic _controller;
        PlayerCharacter _player;
        Animator _animator;

        float _coolDown = 0.5f;
        float _startTime;
        public ShootState(CharacterPhysic controller, PlayerCharacter player, Animator animator)
        {
            _controller = controller;
            _player = player;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetBool(_player.ShootHash, true);
            _startTime = 0f;
        }

        public void OnExit()
        {
            _animator.SetBool(_player.ShootHash, false);
        }

        public void Tick()
        {
            if(Mathf.Approximately(_startTime, 0f) || (Time.time - _startTime) >= _coolDown)
            {
                _controller.Shoot();
                _startTime = Time.time;
            }
        }
    }
}
