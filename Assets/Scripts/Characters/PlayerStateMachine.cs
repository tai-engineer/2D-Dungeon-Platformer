using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace DP2D
{
    [RequireComponent(typeof(PlayerCharacter), typeof(Animator))]
    [RequireComponent(typeof(CharacterPhysic), typeof(Animator))]
    public class PlayerStateMachine : MonoBehaviour
    {
        StateMachine _stateMachine;
        CharacterPhysic _controller;
        Animator _animator;
        PlayerCharacter _player;
        void Awake()
        {
            _controller = GetComponent<CharacterPhysic>();
            _animator = GetComponent<Animator>();
            _player = GetComponent<PlayerCharacter>();

            _stateMachine = new StateMachine();
            
            var _idleState = new IdleState(_controller, _player, _animator);
            var _runState = new MoveState(_controller, _player, _animator);
            //var _climpState = new ClimpState();
            var _jumpState = new JumpState(_controller, _player, _animator);
            var _fallState = new FallState(_controller, _player, _animator);
            var _landState = new LandState(_controller, _player, _animator);
            //var _shootState = new ShootState(_controller, _player, _animator);

            At(_idleState, _runState, IsMoving);
            At(_idleState, _jumpState, IsJumping);
            At(_idleState, _fallState, IsNotGrounded);

            At(_runState, _idleState, IsStopMoving);

            At(_jumpState, _fallState, IsFalling);

            At(_fallState, _landState, IsGrounded);

            At(_landState, _idleState, FinishLanding);

            //At(_shootState, _idleState, IsStopMoving);
            //At(_shootState, _runState, IsMoving);
            //At(_shootState, _jumpState, IsJumping);
            //At(_shootState, _fallState, IsFalling);
            //AtAny(_shootState, () => _player.ShootInput);
            _stateMachine.SetState(_idleState);
        }
        void Update()
        {
            _stateMachine.Tick();
        }

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
        void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
        bool IsMoving() => !Mathf.Approximately(_player.MoveInput.x, 0f);
        bool IsStopMoving() => !IsMoving();
        bool IsJumping() => _player.JumpInput;
        bool IsGrounded() => _controller.VerticalCollisionCheck(false);
        bool IsNotGrounded() => !IsGrounded();
        bool IsFalling() => _controller.MoveVector.y < 0f;
        bool FinishLanding() => _controller.IsLanding == false;
    }
}
