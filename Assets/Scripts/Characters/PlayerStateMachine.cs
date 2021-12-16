using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using DP2D.Debugger;
#endif

namespace DP2D
{
    [RequireComponent(typeof(PlayerCharacter), typeof(Animator))]
    [RequireComponent(typeof(CharacterPhysic))]
    public class PlayerStateMachine : MonoBehaviour
    {
        StateMachine _stateMachine;
#if UNITY_EDITOR
        [SerializeField]
        StateMachineDebugger _stateMachineDebugger = default;
#endif
        [HideInInspector] public CharacterPhysic controller;
        [HideInInspector] public Animator animator;
        [HideInInspector] public PlayerCharacter player;
        void Awake()
        {
            controller = GetComponent<CharacterPhysic>();
            animator = GetComponent<Animator>();
            player = GetComponent<PlayerCharacter>();

            _stateMachine = new StateMachine();
#if UNITY_EDITOR
            _stateMachine.debugger = _stateMachineDebugger;
#endif
            var _idleState = new IdleState(this);
            var _runState = new MoveState(this);
            var _jumpState = new JumpState(this);
            var _fallState = new FallState(this);
            var _landState = new LandState(this);
            var _slideState = new SlideState(this);
            var _wallSlideState = new WallSlideState(this, false);
            var _wallSlideJumpState = new WallSlideJumpState(this);
            var _wallHangState = new WallHangState(this);
            var _wallClimbState = new WallClimbState(this);
            var _rollState = new RollState(this);
            var _attackState = new AttackState(this);
            var _crouchState = new CrouchState(this);
            var _crouchWalkState = new CrouchWalkState(this);
            var _crouchAttackState = new CrouchAttackState(this);
            var _hitState = new HitState(this);
            var _dieState = new DieState(this);

            At(_idleState, _runState, IsMoving);
            At(_idleState, _jumpState, JumpInput);
            At(_idleState, _fallState, IsNotGrounded);
            At(_idleState, _attackState, AttackInput);
            At(_idleState, _crouchState, CrouchInput);
            At(_idleState, _hitState, IsHit);
            At(_idleState, _dieState, IsDead);

            At(_runState, _idleState, IsStopMoving);
            At(_runState, _jumpState, JumpInput);
            At(_runState, _slideState, SlideInput);
            At(_runState, _fallState, IsNotGrounded);
            At(_runState, _rollState, RollInput);
            At(_runState, _attackState, AttackInput);
            At(_runState, _hitState, IsHit);
            At(_runState, _dieState, IsDead);

            At(_jumpState, _fallState, IsFalling);
            At(_jumpState, _hitState, IsHit);

            At(_fallState, _landState, IsGrounded);
            At(_fallState, _wallHangState, CanHang);
            At(_fallState, _wallSlideState, CanWallSlide);
            At(_fallState, _hitState, IsHit);
            At(_fallState, _dieState, IsDead);

            At(_landState, _idleState, FinishLanding);

            At(_slideState, _idleState, FinishSliding);
            At(_slideState, _fallState, IsNotGrounded);
            At(_slideState, _jumpState, JumpInput);
            At(_slideState, _hitState, IsHit);
            At(_slideState, _dieState, IsDead);

            At(_wallSlideState, _idleState, IsGrounded);
            At(_wallSlideState, _fallState, WallSlideCancel);
            At(_wallSlideState, _wallSlideJumpState, JumpInput);
            At(_wallSlideState, _hitState, IsHit);
            At(_wallSlideState, _dieState, IsDead);

            At(_wallSlideJumpState, _fallState, IsFalling);
            At(_wallSlideJumpState, _hitState, IsHit);
            At(_wallSlideJumpState, _dieState, IsDead);

            At(_wallHangState, _fallState, IsFalling);
            At(_wallHangState, _wallSlideJumpState, JumpInput);
            At(_wallHangState, _wallClimbState, ClimbInput);
            At(_wallHangState, _hitState, IsHit);
            At(_wallHangState, _dieState, IsDead);

            At(_wallClimbState, _idleState, FinishClimbing);
            At(_wallClimbState, _hitState, IsHit);
            At(_wallClimbState, _dieState, IsDead);

            At(_rollState, _idleState, FinishRolling);
            At(_rollState, _fallState, IsNotGrounded);

            At(_attackState, _idleState, IsNotAttacking);
            At(_attackState, _hitState, IsHit);
            At(_attackState, _dieState, IsDead);

            At(_crouchState, _idleState, CrouchInputCancel);
            At(_crouchState, _crouchWalkState, IsMoving);
            At(_crouchState, _crouchAttackState, AttackInput);
            At(_crouchState, _hitState, IsHit);
            At(_crouchState, _dieState, IsDead);

            At(_crouchWalkState, _idleState, CrouchInputCancel);
            At(_crouchWalkState, _crouchState, IsStopMoving);
            At(_crouchWalkState, _fallState, IsNotGrounded);
            At(_crouchWalkState, _crouchAttackState, AttackInput);
            At(_crouchWalkState, _hitState, IsHit);
            At(_crouchWalkState, _dieState, IsDead);

            At(_crouchAttackState, _crouchState, IsNotAttacking);
            At(_crouchAttackState, _hitState, IsHit);
            At(_crouchAttackState, _dieState, IsDead);

            At(_hitState, _idleState, IsGrounded);
            At(_hitState, _fallState, IsNotGrounded);
            At(_hitState, _dieState, IsDead);

            At(_dieState, _idleState, IsAlive);

            _stateMachine.SetState(_idleState);
#if UNITY_EDITOR
            _stateMachineDebugger.Awake(_stateMachine);
#endif
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
        bool IsGrounded() => controller.IsGrounded;
        bool IsNotGrounded() => !controller.IsGrounded;
        bool IsFalling() => controller.MoveVector.y < 0f;
        bool FinishLanding() => controller.IsLanding == false;
        bool SlideInput() => player.SlideInput && controller.CanSlide;
        bool FinishSliding() => controller.IsSliding == false;
        bool CanWallSlide() => controller.CanWallSlide;
        bool WallSlideCancel() => !controller.CanWallSlide;
        bool CanHang() => controller.CanWallhang;
        bool ClimbInput() => player.ClimbInput;
        bool FinishClimbing() => controller.IsClimbing == false;
        bool FinishRolling() => controller.IsRolling == false;
        bool RollInput() => player.RollInput;
        bool AttackInput() => player.AttackInput;
        bool IsNotAttacking() => !controller.IsAttacking;
        bool CrouchInput() => player.CrouchInput;
        bool CrouchInputCancel() => !player.CrouchInput;
        bool IsHit() => controller.IsHit;
        bool IsDead() => player.IsDead;
        bool IsAlive() => !player.IsDead;
    }
}
