using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    public float _gravity = 0f;

    private void FixedUpdate()
    {
        transform.position = transform.position - 0.5f * _gravity * Time.fixedDeltaTime * Time.fixedDeltaTime * Vector3.up;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(Fall), 0.5f);
        }
    }

    void Fall()
    {
        _gravity = 700;
        Invoke(nameof(BackToPosition), 1.5f);
    }
    void BackToPosition()
    {
        _gravity = -700;
        Invoke(nameof(Stand), 1.5f);
    }

    void Stand()
    {
        _gravity = 0;
    }
}
