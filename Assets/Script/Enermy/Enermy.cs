using System;
using UnityEngine;

namespace Script
{
    public class Enermy : MonoBehaviour
    {
        [SerializeField] private float _velocityChase = 10f;
        [SerializeField] private float _velocityPatrol = 20f;
        [SerializeField] private float _left;
        [SerializeField] private float _right;

        [SerializeField] private float _rangePatrol = 0f;
        [SerializeField] private Player _player;
        private bool _isRight = true;

        private Transform _target;
        [SerializeField] private State state;
        enum State
        {
            Chase, Patrol, Hurt
        }
        private void Awake()
        {
            state = State.Chase;
        }

        private void Update()
        {
            if (!_player)
            {
                state = State.Chase;
                return;
            }

            state = Vector3.Distance(transform.position, _player.transform.position) < _rangePatrol ? State.Patrol : State.Chase;
        }

        private void FixedUpdate()
        {
            Debug.DrawLine(transform.position - _rangePatrol * Vector3.right, transform.position + _rangePatrol * Vector3.right);
            if (state == State.Patrol) Patrol(_player.transform);
            if (state == State.Chase) Chase(_left, _right);

        }

        void Chase(float left, float right)
        {
            if (transform.position.x <= left)
            {
                var localScale = transform.localScale;
                localScale = new Vector3(1, localScale.y, localScale.z);
                transform.localScale = localScale;
                _isRight = true;
            }

            if (transform.position.x >= right)
            {
                var localScale = transform.localScale;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                transform.localScale = localScale;
                _isRight = false;
            }
            
            if (_isRight) transform.Translate(_velocityChase * Time.fixedDeltaTime * Vector2.right);
            else transform.Translate(_velocityChase * Time.fixedDeltaTime * Vector2.left);
        }

        void Patrol(Transform player)
        {
            if (Vector3.Distance(player.position, transform.position) > _rangePatrol)
            {
                state = State.Chase;
                return;
            }
            
            Vector3 distance = player.position - transform.position;
            distance = distance.normalized;
            Vector3 direction = new Vector3(distance.x, 0, 0);
            if (distance.x > 0)
            {
                var localScale = transform.localScale;
                localScale = new Vector3(1, localScale.y, localScale.z);
                transform.localScale = localScale;
                _isRight = true;
            }

            if (distance.x < -0)
            {
                var localScale = transform.localScale;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                transform.localScale = localScale;
                _isRight = false;
            }
            transform.Translate(_velocityPatrol * Time.fixedDeltaTime * direction);
        }
    }
}