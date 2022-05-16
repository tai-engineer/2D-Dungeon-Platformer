using UnityEngine;
using UnityEngine.Assertions;
namespace DP2D
{
    public class StateController : MonoBehaviour
    {
        public StateSO currentState;

        [HideInInspector] public CharacterPhysic characterPhysic;
        [HideInInspector] public PlayerCharacter character;
        [HideInInspector] public Animator characterAnimator;
        void Awake()
        {
            characterPhysic = GetComponent<CharacterPhysic>();
            character = GetComponent<PlayerCharacter>();
            characterAnimator = GetComponent<Animator>();
        }

        void Start()
        {
            Assert.IsNotNull(currentState, "current state cannot be null.");
            currentState.OnEnter(this);
        }
        void Update()
        {
            currentState.UpdateState(this);
        }
        public void TransitionToState(StateSO state)
        {
            if (currentState == state)
                return;
            currentState.OnExit(this);
            Debug.Log("previousState = " + currentState);
            currentState = state;
            Debug.Log("currentState = " + currentState);
            currentState.OnEnter(this);
        }
    }
}
