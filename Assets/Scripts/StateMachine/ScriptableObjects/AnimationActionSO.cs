using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName = "StateMachine_SO/Action/Animation")]
    public class AnimationActionSO : ActionSO
    {
        public ParameterType parameterType;

        public string parameterName;
        public bool boolValue;
        public float floatValue;
        public int intValue;

        public override void Do(StateController stateController)
        {
            if(parameterType == ParameterType.Bool)
            {
                stateController.characterAnimator.SetBool(parameterName, boolValue);
            }
            else if(parameterType == ParameterType.Float)
            {
                stateController.characterAnimator.SetFloat(parameterName, floatValue);
            }
            else if(parameterType == ParameterType.Int)
            {
                stateController.characterAnimator.SetInteger(parameterName, intValue);
            }
            else
            {
                stateController.characterAnimator.SetTrigger(parameterName);
            }
        }
        public enum ParameterType
        { Bool, Float, Int, Trigger }
    }
}
