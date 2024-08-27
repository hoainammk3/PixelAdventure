using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private float pushForce = 25f;
    private static readonly int JumpAction = Animator.StringToHash("JumpAction");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool(JumpAction, true);
            Rigidbody2D _rgdb = other.gameObject.GetComponent<Rigidbody2D>();
            _rgdb.AddForce(pushForce * Vector3.up, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool(JumpAction, false);
        }
    }
}
