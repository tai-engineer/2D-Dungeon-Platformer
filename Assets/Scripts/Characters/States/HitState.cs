using UnityEngine;
namespace DP2D
{
    public class HitState : IState
    {
        ParticleSystem _splashBlood = null;
        public HitState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            player = stateMachine.player;
            animator = stateMachine.animator;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.HitHash, true);
            controller.ResetMoveVector();
            if (_splashBlood  == null)
            {
                GameObject obj = MonoBehaviour.Instantiate(player.splashBlood, player.bloodTransform);
                _splashBlood = obj.GetComponent<ParticleSystem>();
            }
            else
            {
                _splashBlood.Play();
            }
        }

        public override void OnExit()
        {
            animator.SetBool(player.HitHash, false);
            controller.IsHit = false;
        }

        public override void Tick()
        {
            controller.VerticalCollisionCheck(false);
        }
    }
}
