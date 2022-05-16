using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Decision/IsLanded")]
    public class IsLandedSO : DecisionSO
    {
        public override bool Decide(StateController stateController)
        {
            return !stateController.characterPhysic.IsLanding;
        }
    }
}
