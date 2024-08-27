using UnityEngine;

namespace Script
{
    public class StatePlayerController : StateController
    {
        private IState _currentState;
        
        public override void ChangeState(IState newState)
        {
            base.ChangeState(newState);
            {
                _currentState?.OnExit(this);
                _currentState = newState;
                _currentState.OnEnter(this);
            }
        }
    }
}