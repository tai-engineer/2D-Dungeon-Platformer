using UnityEngine;

namespace DP2D
{
    public class SlideState : IState
    {
        float _startTime;
        public SlideState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            animator = stateMachine.animator;
            player = stateMachine.player;
        }
        public override void OnEnter()
        {
            controller.IsSliding = true;
            animator.SetBool(player.SlideHash, true);
            _startTime = Time.time;
        }

        public override void OnExit()
        {
            animator.SetBool(player.SlideHash, false);
            controller.SlideEnd();
        }

        public override void Tick()
        {
            if((Time.time - _startTime) > controller.SlideDuration)
            {
                controller.IsSliding = false;
                return;
            }
        }
    }
}
