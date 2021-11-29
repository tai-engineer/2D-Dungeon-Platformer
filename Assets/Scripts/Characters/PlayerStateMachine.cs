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

        public CharacterPhysic controller;
        public Animator animator;
        public PlayerCharacter player;
        void Awake()
        {
            controller = GetComponent<CharacterPhysic>();
            animator = GetComponent<Animator>();
            player = GetComponent<PlayerCharacter>();

            _stateMachine = new StateMachine();
            
            var _idleState = new IdleState(this);
            var _runState = new MoveState(this);
            //var _climpState = new ClimpState();
            var _jumpState = new JumpState(this);
            var _fallState = new FallState(this);
            var _landState = new LandState(this);
            var _slideState = new SlideState(this);

            At(_idleState, _runState, IsMoving);
            At(_idleState, _jumpState, JumpInput);
            At(_idleState, _fallState, IsNotGrounded);

            At(_runState, _idleState, IsStopMoving);
            At(_runState, _jumpState, JumpInput);
            At(_runState, _slideState, SlideInput);

            At(_jumpState, _fallState, IsFalling);

            At(_fallState, _landState, IsGrounded);

            At(_landState, _idleState, FinishLanding);

            At(_slideState, _idleState, FinishSliding);
            At(_slideState, _fallState, IsNotGrounded);
            At(_slideState, _jumpState, JumpInput);

            _stateMachine.SetState(_idleState);
        }
        void Update()
        {
            _stateMachine.Tick();
        }

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
        void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
        bool IsMoving() => !Mathf.Approximately(player.MoveInput.x, 0f);
        bool IsStopMoving() => !IsMoving();
        bool JumpInput() => player.JumpInput;
        bool IsGrounded() => controller.VerticalCollisionCheck(false);
        bool IsNotGrounded() => !IsGrounded();
        bool IsFalling() => controller.MoveVector.y < 0f;
        bool FinishLanding() => controller.IsLanding == false;
        bool SlideInput() => player.SlideInput && controller.CanSlide;
        bool FinishSliding() => controller.IsSliding == false;
    }
}
