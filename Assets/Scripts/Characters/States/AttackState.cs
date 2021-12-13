using UnityEngine;
namespace DP2D
{
    public class AttackState : IState
    {
        public AttackState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.Attack1Hash, true);

            controller.IsAttacking = true;
            controller.SetAttackDamage(controller.Attack1Damage);
            controller.EnableMeleeDamage();
            controller.SetHorizontalMovement(0f);

            player.DisableInputAttack();
        }

        public override void OnExit()
        {
            controller.IsAttacking = false;
            animator.SetBool(player.Attack1Hash, false);
            animator.SetBool(player.Attack2Hash, false);

            controller.DisableMeleeDamage();

            player.EnableInputAttack();
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
            controller.HorizontalCollisionCheck();
            if(player.AttackInputEnable && player.AttackInput)
            {
                NextAttack();
            }
            controller.AddHorizontalMovement(controller.MeleeAttackDash * controller.FaceDirection.x * Time.deltaTime);
        }

        void NextAttack()
        {
            controller.NextAttackEnable = true;
            Attack2();
        }

        void Attack2()
        {
            animator.SetBool(player.Attack2Hash, true);
            controller.SetAttackDamage(controller.Attack2Damage);
            controller.EnableMeleeDamage();
        }
    }
}
