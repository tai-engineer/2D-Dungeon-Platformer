using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Action/HorizontalCollisionCheck")]
    public class HorizontalCollisionCheckSO : ActionSO
    {
        public override void Do(StateController stateController)
        {
            stateController.characterPhysic.HorizontalCollisionCheck();
        }
    }
}
