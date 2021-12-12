using UnityEngine;
namespace DP2D
{
    public class CrouchAttackState : IState
    {
        public CrouchAttackState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.CrouchAttackHash, true);
            controller.IsAttacking = true;
            controller.SetHorizontalMovement(0f);
            controller.SetAttackDamage(controller.Attack1Damage);
            controller.EnableMeleeDamage();
        }

        public override void OnExit()
        {
            controller.IsAttacking = false;
            animator.SetBool(player.CrouchAttackHash, false);
            controller.DisableMeleeDamage();
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
        }
    }
}
