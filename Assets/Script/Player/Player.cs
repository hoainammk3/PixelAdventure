using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hitForce = 5f;
    private Rigidbody2D _rb2d;
    private Animator _ani;
    private bool _isGround = true;
    private bool _canJump = false;
    private bool _canJump2 = true;
    private bool _isDie = false;
    private int _heal = 100;
    private float _inputHorizontal = 0;
    
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int VelocityY = Animator.StringToHash("velocityY");
    private static readonly int IsGround = Animator.StringToHash("isGround");
    private static readonly int IsJump1 = Animator.StringToHash("isJump1");
    private static readonly int IsJump2 = Animator.StringToHash("isJump2");
    private static readonly int Hit = Animator.StringToHash("OnHit");

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _ani = GetComponent<Animator>();
    }

    private void Start()
    {
        _ani.SetBool(IsGround, true);
        _ani.SetBool(IsJump1, false);
        _ani.SetBool(IsJump2, false);
        _ani.SetBool(IsRunning, false);
    }

    void Update()
    {
//        if (_heal <= 0) _isDie = true;
//        if (_isDie) {
//            Debug.Log("Die");
//        }
        if (Math.Abs(_rb2d.velocity.y) < 0.01)
        {
            _ani.SetBool(IsGround, true);
        }
        _inputHorizontal = MoveController.Instance.InputHorizontalTotal;
        if (MoveController.Instance.IsJumpTotal)
        {
            _canJump = true;
            MoveController.Instance.IsJumpKey = false;
            MoveController.Instance.IsJumpButton = false;
        }
    }


    private void FixedUpdate()
    {
//        if (_heal <= 0) _isDie = true;
//        if (_isDie) {
//            Debug.Log("Die");
//        }
        MoveHorizontal(_inputHorizontal);

        if (_canJump)
        {
            Jump();
            _canJump = false;
        }
              
    }

    private void Jump()
    {
        if (_isGround)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f); // Reset vận tốc Y để nhảy ổn định hơn
            _rb2d.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            _isGround = false;
//            Debug.Log("Jump1");
            _ani.SetBool(IsGround, false);
            _ani.SetBool(IsJump1, true);
            _ani.SetBool(IsJump2, false);
        }
        else
        {
            if (_canJump2)
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f); // Reset vận tốc Y để nhảy ổn định hơn
                _rb2d.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                _canJump2 = false;
//                Debug.Log("Jump2");
                _ani.SetBool(IsJump1, false);
                _ani.SetBool(IsJump2, true);
            }
        }
    }

    private void MoveHorizontal(float input)
    {
        float scaleX = transform.localScale.x;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position + new Vector3(scaleX * 0.4f, 0, 0) , Vector2.right * scaleX, 0.05f);
        
        
        if (Math.Abs(input) > 0.01) scaleX = Math.Sign(input) * Math.Abs(scaleX);
        _ani.SetBool(IsRunning, Math.Abs(input) > 0.01);
        _ani.SetFloat(VelocityY, _rb2d.velocity.y);
        var localScale = transform.localScale;
        localScale = new Vector3(scaleX, localScale.y, localScale.z);
        transform.localScale = localScale;

        if (!raycastHit2D.collider || !raycastHit2D.collider.CompareTag("Ground"))
        {
            transform.Translate(input * velocity * Time.fixedDeltaTime * Vector2.right);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
            _canJump2 = true;
            _ani.SetBool(IsGround, true);
            _ani.SetBool(IsJump1, false);
            _ani.SetBool(IsJump2, false);
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            OnHit(100);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            _ani.SetBool(Hit, false);
        }
    }

    private void OnHit(int damage)
    {
        _heal -= damage;
        _ani.SetBool(Hit, true);
        _rb2d.AddForce(hitForce * Vector3.up, ForceMode2D.Impulse);
    }
}
