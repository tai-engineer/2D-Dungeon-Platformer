using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [System.Serializable]
    public class Transition
    {
        public DecisionSO decision;
        public StateSO trueState;
        public StateSO falseState;
    }
}
