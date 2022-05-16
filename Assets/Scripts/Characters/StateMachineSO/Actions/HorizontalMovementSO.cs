using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Action/HorizontalMovement")]
    public class HorizontalMovementSO : ActionSO
    {
        public override void Do(StateController stateController)
        {
            stateController.characterPhysic.HorizontalMove(stateController.character.MoveInput.x);
        }
    }
}
