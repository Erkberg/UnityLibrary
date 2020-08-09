using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class BaseState
    {
        public string name;

        public BaseState()
        {
        }

        public BaseState(string name)
        {
            this.name = name;
        }
        
        public virtual void Enter() { }
        
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}

