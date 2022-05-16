using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Decision")]
    public abstract class DecisionSO : ScriptableObject
    {
        public abstract bool Decide(StateController stateController);
    }
}
