using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Action/StopMovement")]
    public class StopMovementSO : ActionSO
    {
        public override void Do(StateController stateController)
        {
            stateController.characterPhysic.ResetMoveVector();
        }
    }
}
