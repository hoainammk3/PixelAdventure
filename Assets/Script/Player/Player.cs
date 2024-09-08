using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    private static readonly int IsDie = Animator.StringToHash("isDie");

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _ani = GetComponent<Animator>();
    }

    private void Start()
    {
        Reset();
    }

    public int Heal => _heal;

    void Update()
    {
        if (_heal <= 0) _isDie = true;
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
        
        if (_isDie) return;
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
            AddForceUp(jumpForce);
            _isGround = false;
            _ani.SetBool(IsGround, false);
            _ani.SetBool(IsJump1, true);
            _ani.SetBool(IsJump2, false);
            AudioManager.Instance.PlayPlayerJumpClip();
        }
        else
        {
            if (_canJump2)
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f); // Reset vận tốc Y để nhảy ổn định hơn
                _rb2d.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                _canJump2 = false;
                _ani.SetBool(IsJump1, false);
                _ani.SetBool(IsJump2, true);
                AudioManager.Instance.PlayPlayerJumpClip();
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
        if (other.gameObject.CompareTag("Trap"))
        {
            TakeDamage(50);
        }
    }

    public void TakeDamage(int damage)
    {
        _heal -= damage;
        if (_heal <= 0)
        {
            _isDie = true;
            _ani.SetBool(IsDie, true);
            Invoke(nameof(DestroyGameObject), 0.35f);
            return;
        }
        
        _ani.SetBool(Hit, true);
        AddForceUp(hitForce);
        Invoke(nameof(SetAnimationHitFalse), 0.3f);
        AudioManager.Instance.PlayOnHitClip();
    }

    void SetAnimationHitFalse()
    {
        _ani.SetBool(Hit, false);
    }

    void DestroyGameObject()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
        AudioManager.Instance.PlayDieClip();
    }
    public void Reset()
    {
        _heal = 100;
        _isDie = false;
        _ani.SetBool(Hit, false);
        _ani.SetBool(IsDie, false);
        _ani.SetBool(IsGround, true);
        _ani.SetBool(IsJump1, false);
        _ani.SetBool(IsJump2, false);
        _ani.SetBool(IsRunning, false);
        transform.position = new Vector3(-5.74f, 0.27f, 0);
    }

    public void AddForceUp(float force)
    {
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f); // Reset vận tốc Y để nhảy ổn định hơn
        _rb2d.AddForce(Vector3.up * force, ForceMode2D.Impulse);
    }

    public void StandGround()
    {
        _isGround = true;
        _canJump2 = true;
        _ani.SetBool(IsGround, true);
        _ani.SetBool(IsJump1, false);
        _ani.SetBool(IsJump2, false);
    }
}
