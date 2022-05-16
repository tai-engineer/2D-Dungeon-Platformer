using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Action/VerticalCollisionCheck")]
    public class VerticalCollisionCheckSO : ActionSO
    {
        public bool top;
        public override void Do(StateController stateController)
        {
            stateController.characterPhysic.VerticalCollisionCheck(top);
        }
    }
}
