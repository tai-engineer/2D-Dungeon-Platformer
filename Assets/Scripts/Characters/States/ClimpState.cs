using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class ClimpState : IState
    {
        public ClimpState() { }
        public void OnEnter()
        {
            Debug.Log($"Enter {this.GetType()}");
        }

        public void OnExit()
        {
            Debug.Log($"Exit {this.GetType()}");
        }

        public void Tick()
        {
            Debug.Log($"Tick {this.GetType()}");
        }
    }
}
