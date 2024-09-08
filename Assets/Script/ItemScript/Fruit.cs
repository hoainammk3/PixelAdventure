using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private static readonly int IsDestroy = Animator.StringToHash("isDestroy");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayClaimClip();
            GameController.Instance.AddScore(10);
            animator.SetBool(IsDestroy, true);
            Invoke(nameof(DestroyObject), 0.2f);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
