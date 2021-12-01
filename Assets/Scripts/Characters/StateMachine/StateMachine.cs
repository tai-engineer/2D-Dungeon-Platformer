using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace DP2D
{
    public class StateMachine
    {
        IState _currentState;

        Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        List<Transition> _anyTransitions = new List<Transition>();
        List<Transition> _currentTransitions = new List<Transition>();
        readonly List<Transition> EmptyTransition = new List<Transition>(0);
        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                SetState(transition.To); 
            }
            _currentState.Tick();
        }
        public void SetState(IState state)
        {
            //To avoid calling this function many times in same state
            if (_currentState == state)
            {
                return;
            }
            _currentState?.OnExit();
            if (_currentState != null)
                Debug.Log($"{_currentState.GetType()} ==> {state.GetType()}");
            _currentState = state;
            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransition;
            _currentState.OnEnter();
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
                if (anyTransition.Condition())
                    return anyTransition;
            }
            foreach (var transition in _currentTransitions)
            {
                if (transition.Condition())
                    return transition;
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
