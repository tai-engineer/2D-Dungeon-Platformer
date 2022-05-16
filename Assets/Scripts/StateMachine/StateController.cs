using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class StateController : MonoBehaviour
    {
        public StateSO currentState;
        public StateSO remainState;
        [HideInInspector] public CharacterPhysic characterPhysic;
        [HideInInspector] public PlayerCharacter character;
        [HideInInspector] public Animator characterAnimator;
        void Awake()
        {
            characterPhysic = GetComponent<CharacterPhysic>();
            character = GetComponent<PlayerCharacter>();
            characterAnimator = GetComponent<Animator>();
        }
        void Update()
        {
            currentState.UpdateState(this);
        }
        public void TransitionToState(StateSO state)
        {
            if (currentState == remainState)
                return;
            currentState.OnExit(this);
            currentState = state;
            Debug.Log("currentState = " + currentState);
            currentState.OnEnter(this);
        }
    }
}
