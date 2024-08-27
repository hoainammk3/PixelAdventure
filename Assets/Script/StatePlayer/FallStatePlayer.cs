using UnityEngine;

namespace Script.StatePlayer
{
    public class FallStatePlayer : MonoBehaviour, IState
    {
        private Animator _ani;
        
        private static readonly int VelocityY = Animator.StringToHash("velocityY");
        private void Awake()
        {
            _ani = GetComponent<Animator>();
        }

        public void OnEnter(StateController controller)
        {
            
        }

        public void UpdateState(StateController controller)
        {
            
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