using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public abstract class ActionSO : ScriptableObject
    {
        public abstract void Do(StateController stateController);
    }
}
