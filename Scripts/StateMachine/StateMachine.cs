using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class StateMachine
    {
        List<BaseState> states = new List<BaseState>();
        BaseState currentState;

        public void AddState(BaseState state)
        {
            states.Add(state);
        }
 
        public void ChangeState(BaseState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
        
        public void ChangeState(string newStateName)
        {
            BaseState newState = GetStateByName(newStateName);

            if (newState != null)
            {
                ChangeState(newState);
            }
        }
 
        public void Update()
        {
            currentState?.Update();
        }
        
        private BaseState GetStateByName(string name)
        {
            BaseState matchedState = null;

            foreach (BaseState state in states)
            {
                if (state.name == name)
                {
                    matchedState = state;
                    break;
                }
            }

            return matchedState;
        }
    }
}