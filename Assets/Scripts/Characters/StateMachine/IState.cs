using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}
