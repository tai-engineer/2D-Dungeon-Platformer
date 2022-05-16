using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Action/SetVerticalMovement")]
    public class SetVerticalMovementSO : ActionSO
    {
        public float jumpSpeed;
        public override void Do(StateController stateController)
        {
            stateController.characterPhysic.SetVerticalMovement(jumpSpeed);
        }
    }
}
