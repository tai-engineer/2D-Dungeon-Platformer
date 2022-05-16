using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    [CreateAssetMenu(menuName ="StateMachine_SO/State")]
    public class StateSO : ScriptableObject
    {
        public ActionSO[] enterActions;
        public ActionSO[] loopActions;
        public ActionSO[] exitActions;
        public Transition[] transitions;

        public void OnEnter(StateController stateController)
        {
            for (int i = 0; i < enterActions.Length; i++)
            {
                enterActions[i].Do(stateController);
            }
        }
        public void OnExit(StateController stateController)
        {
            for (int i = 0; i < exitActions.Length; i++)
            {
                exitActions[i].Do(stateController);
            }
        }
        public void UpdateState(StateController stateController)
        {
            DoActions(stateController);
            CheckTransitions(stateController);
        }
        void DoActions(StateController stateController)
        {
            for(int i = 0; i < loopActions.Length; i++)
            {
                loopActions[i].Do(stateController);
            }
        }

        void CheckTransitions(StateController stateController)
        {
            for(int i = 0; i < transitions.Length; i++)
            {
                bool decideSuceeded = transitions[i].decision.Decide(stateController);
                StateSO nextState = decideSuceeded ? transitions[i].trueState : transitions[i].falseState;

                stateController.TransitionToState(nextState);

                return;
            }
        }
    }
}
