using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class LandState : IState
    {
        public LandState(PlayerStateMachine stateMachine)
        {
            controller = stateMachine.controller;
            player = stateMachine.player;
            animator = stateMachine.animator;
        }
        public override void OnEnter()
        {
            animator.SetBool(player.LandHash, true);
            controller.LandingPrepare();
            controller.ResetMoveVector();
        }

        public override void OnExit()
        {
            animator.SetBool(player.LandHash, false);
        }

        public override void Tick()
        {

        }
    }
}
