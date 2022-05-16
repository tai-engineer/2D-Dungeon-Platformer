using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Decision/IsHit")]
    public class IsHitSO : DecisionSO
    {
        public override bool Decide(StateController stateController)
        {
            return stateController.characterPhysic.IsHit;
        }
    }
}
