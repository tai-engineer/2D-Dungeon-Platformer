using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Decision/IsFalling")]
    public class IsFallingSO : DecisionSO
    {
        public override bool Decide(StateController stateController)
        {
            return stateController.characterPhysic.MoveVector.y < 0f;
        }
    }
}
