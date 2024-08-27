using System;
using UnityEngine;

namespace Script.StatePlayer
{
    public class IdleStatePlayer : MonoBehaviour, IState
    {
        private Animator _ani;
        
        private static readonly int IsGround = Animator.StringToHash("isGround");
        private void Awake()
        {
            _ani = GetComponent<Animator>();
        }

        public void OnEnter(StateController controller)
        {
            _ani.SetBool(IsGround, true);
        }

        public void UpdateState(StateController controller)
        {
            throw new System.NotImplementedException();
        }

        public void FixedUpdateState(StateController controller)
        {
            throw new System.NotImplementedException();
        }

        public void OnHurt(StateController controller)
        {
            controller.ChangeState(controller.hurtState);
        }

        public void OnExit(StateController controller)
        {
            _ani.SetBool(IsGround, false);
        }
    }
}