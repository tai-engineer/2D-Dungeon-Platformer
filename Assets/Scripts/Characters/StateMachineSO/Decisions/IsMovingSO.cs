using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Decision/IsMoving")]
    public class IsMovingSO : DecisionSO
    {
        public override bool Decide(StateController stateController)
        {
            return !Mathf.Approximately(stateController.character.MoveInput.x, 0f);
        }
    }
}
