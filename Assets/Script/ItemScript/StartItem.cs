using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItem : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int CheckPoint = Animator.StringToHash("checkPoint");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool(CheckPoint, true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool(CheckPoint, false);
        }
    }
}
