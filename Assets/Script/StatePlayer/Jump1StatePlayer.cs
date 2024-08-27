using UnityEngine;

namespace Script.StatePlayer
{
    public class Jump1StatePlayer : MonoBehaviour, IState
    {
        [SerializeField] private float velocity = 5f;
        [SerializeField] private float jumpForce = 10f;
        
        private Rigidbody2D _rb2d;
        private Animator _ani;
        private bool _isJump = false;
        private static readonly int IsJump1 = Animator.StringToHash("isJump1");
        
        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _ani = GetComponent<Animator>();
        }
        
        public void OnEnter(StateController controller)
        {
            _ani.SetBool(IsJump1, true);
        }

        public void UpdateState(StateController controller)
        {
            if (MoveController.Instance.IsJumpTotal)
            {
                _isJump = true;
                MoveController.Instance.IsJumpKey = false;
                MoveController.Instance.IsJumpButton = false;
            }
        }

        public void FixedUpdateState(StateController controller)
        {
            if (_isJump)
            {
                Jump();
                _isJump = false;
            }
        }

        public void OnHurt(StateController controller)
        {
            controller.ChangeState(controller.hurtState);
        }

        public void OnExit(StateController controller)
        {
            _ani.SetBool(IsJump1, false);
        }

        void Jump()
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f); // Reset vận tốc Y để nhảy ổn định hơn
            _rb2d.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}