using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPlayer : MonoBehaviour
{
    [SerializeField] private float force = 5f;
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBoxEnermy"))
        {
            if (transform.position.y > other.gameObject.transform.position.y)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
                _player.AddForceUp(force);
            }
        }

        if (other.CompareTag("Ground"))
        {
            _player.StandGround();
        }
    }
}
