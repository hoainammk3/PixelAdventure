using System;
using UnityEngine;

namespace Script.StatePlayer
{
    public class RunStatePlayer : MonoBehaviour, IState
    {
        [SerializeField] private float velocity = 5f;
        private float _inputHorizontal = 0;
        private Rigidbody2D _rb2d;
        private Animator _ani;
        
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int VelocityY = Animator.StringToHash("velocityY");

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _ani = GetComponent<Animator>();
        }

        public void OnEnter(StateController controller)
        {
            _ani.SetBool(IsRunning, true);
        }

        public void UpdateState(StateController controller)
        {
            _inputHorizontal = MoveController.Instance.InputHorizontalTotal;
        }

        public void FixedUpdateState(StateController controller)
        {
            MoveHorizontal(_inputHorizontal);
        }

        public void OnHurt(StateController controller)
        {
            controller.ChangeState(controller.hurtState);
        }

        public void OnExit(StateController controller)
        {
            _ani.SetBool(IsRunning, false);
        }
        
        private void MoveHorizontal(float input)
        {
            float scaleX = transform.localScale.x;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position + new Vector3(scaleX * 0.4f, 0, 0) , Vector2.right * scaleX, 0.05f);
            
            if (Math.Abs(input) > 0.01) scaleX = Math.Sign(input) * Math.Abs(scaleX);
            
            _ani.SetFloat(VelocityY, _rb2d.velocity.y);
            var localScale = transform.localScale;
            localScale = new Vector3(scaleX, localScale.y, localScale.z);
            transform.localScale = localScale;

            if (!raycastHit2D.collider || !raycastHit2D.collider.CompareTag("Ground"))
            {
                transform.Translate(input * velocity * Time.fixedDeltaTime * Vector2.right);
            }
        }
    }
}