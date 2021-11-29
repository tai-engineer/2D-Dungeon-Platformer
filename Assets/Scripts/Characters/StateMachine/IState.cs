using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public abstract class IState
    {
        protected CharacterPhysic controller;
        protected PlayerCharacter player;
        protected Animator animator;
        public abstract void Tick();
        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
