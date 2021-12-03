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

        [HideInInspector] public CharacterPhysic controller;
        [HideInInspector] public Animator animator;
        [HideInInspector] public PlayerCharacter player;
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
            var _wallSlideState = new WallSlideState(this, false);
            var _wallSlideJumpState = new WallSlideJumpState(this);
            var _wallHangState = new WallHangState(this);
            var _wallClimbState = new WallClimbState(this);

            At(_idleState, _runState, IsMoving);
            At(_idleState, _jumpState, JumpInput);
            At(_idleState, _fallState, IsNotGrounded);

            At(_runState, _idleState, IsStopMoving);
            At(_runState, _jumpState, JumpInput);
            At(_runState, _slideState, SlideInput);

            At(_jumpState, _fallState, IsFalling);

            At(_fallState, _landState, IsGrounded);
            At(_fallState, _wallHangState, CanHang);
            At(_fallState, _wallSlideState, WallCollided);

            At(_landState, _idleState, FinishLanding);

            At(_slideState, _idleState, FinishSliding);
            At(_slideState, _fallState, IsNotGrounded);
            At(_slideState, _jumpState, JumpInput);

            At(_wallSlideState, _idleState, IsGrounded);
            At(_wallSlideState, _fallState, WallSlideCancel);
            At(_wallSlideState, _wallSlideJumpState, JumpInput);

            At(_wallSlideJumpState, _fallState, IsFalling);

            At(_wallHangState, _fallState, IsFalling);
            At(_wallHangState, _wallSlideJumpState, JumpInput);
            At(_wallHangState, _wallClimbState, ClimbInput);

            At(_wallClimbState, _idleState, FinishClimbing);

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
        bool WallCollided() => controller.HorizontalCollisionCheck();
        bool WallSlideCancel() => !WallCollided();
        bool CanHang() => controller.HorizontalCollisionCheck() && controller.CanHang;
        bool ClimbInput() => player.ClimbInput;
        bool FinishClimbing() => controller.IsClimbing == false;
    }
}
