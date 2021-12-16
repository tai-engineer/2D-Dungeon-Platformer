using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using DP2D.Debugger;
#endif
namespace DP2D
{
    public class StateMachine
    {
        internal IState currentState;

        Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        List<Transition> _anyTransitions = new List<Transition>();
        List<Transition> _currentTransitions = new List<Transition>();
        readonly List<Transition> EmptyTransition = new List<Transition>(0);
#if UNITY_EDITOR
        internal StateMachineDebugger debugger;
#endif
        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                SetState(transition.To); 
            }
            currentState.Tick();
        }
        public void SetState(IState state)
        {
            //To avoid calling this function many times in same state
            if (currentState == state)
            {
                return;
            }
            currentState?.OnExit();
            currentState = state;
            _transitions.TryGetValue(currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransition;
            currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }
            transitions.Add(new Transition(to, condition));
        }

        public void AddAnyTransition(IState to, Func<bool> condition)
        {
            _anyTransitions.Add(new Transition(to, condition));
        }
        Transition GetTransition()
        {
            foreach (var anyTransition in _anyTransitions)
            {
#if UNITY_EDITOR
                debugger.TransitionEvaluationBegin(anyTransition.To.ToString());
#endif
                if (anyTransition.Condition())
                {
#if UNITY_EDITOR
                    debugger.TransitionConditionResult(anyTransition.Condition.Method.Name);
                    debugger.TransitionEvaluationEnd();
#endif
                    return anyTransition;
                }
            }
            foreach (var transition in _currentTransitions)
            {
#if UNITY_EDITOR
                debugger.TransitionEvaluationBegin(transition.To.ToString());
#endif
                if (transition.Condition())
                {
#if UNITY_EDITOR
                    debugger.TransitionConditionResult(transition.Condition.Method.Name);
                    debugger.TransitionEvaluationEnd();
#endif
                    return transition;
                }
            }
            return null;
        }
        private class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }
            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }
    }
}
