using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Action/VerticalMovement")]
    public class VerticalMovementSO : ActionSO
    {
        public override void Do(StateController stateController)
        {
            stateController.characterPhysic.VerticalMove();
        }
    }
}
