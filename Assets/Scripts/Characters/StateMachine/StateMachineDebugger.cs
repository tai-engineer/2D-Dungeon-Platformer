#if UNITY_EDITOR

using System;
using System.Text;
using UnityEngine;
namespace DP2D.Debugger
{
    [Serializable]
    public class StateMachineDebugger
    {
        [SerializeField]
        [Tooltip("Issues a debug log when a state transition is triggered")]
        internal bool _debugTransition = false;

        [SerializeField]
        [Tooltip("List all conditions evaluated, the resutl is read: ConditionName == BooleanResult [Passed Test]")]
        internal bool _appendConditionsInfo = true;

        [SerializeField]
        [Tooltip("The current State name [Readonly]")]
        internal string _currentState;

        StateMachine _stateMachine;
        StringBuilder _logBuilder;
        string _targetState = string.Empty;

        const string CHECK_MARK = "\u2714";
        const string UNCHECK_MARK = "\u2718";
        const string THICK_ARROW = "\u279C";
        const string SHARP_ARROW = "\u27A4";
        

        internal void Awake(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _logBuilder = new StringBuilder();

            _currentState = stateMachine.currentState.ToString();

        }

        void PrintDebugLog()
        {
            _logBuilder.AppendLine();
            _logBuilder.AppendLine("--------------------------");
            Debug.Log(_logBuilder.ToString());
        }
        public void TransitionConditionResult(string conditionName)
        {
            if (!_debugTransition || _logBuilder.Length == 0 || !_appendConditionsInfo)
                return;

            _logBuilder.Append($" {THICK_ARROW} {conditionName} == true");

            _logBuilder.AppendLine($" [{CHECK_MARK}]");
        }
        public void TransitionEvaluationBegin(string targetState)
        {
            _targetState = targetState;

            if (!_debugTransition)
                return;

            _logBuilder.Clear();
            _logBuilder.Append($"{_currentState}  {SHARP_ARROW}  {_targetState}");

            if(_appendConditionsInfo)
            {
                _logBuilder.Append(" || Condition:");
            }
        }

        public void TransitionEvaluationEnd()
        {
            _currentState = _targetState;

            if (!_debugTransition || _logBuilder.Length == 0)
                return;

            PrintDebugLog();

            _logBuilder.Clear();
        }
    }
}
#endif
