using System;
using UnityEngine;

namespace Script
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _velocityBullet = 20f;

        private bool _isRight = true;

        public bool IsRight
        {
            get => _isRight;
            set => _isRight = value;
        }

        private void Start()
        {
            Invoke(nameof(DestroyGameObject), 3);
        }

        private void FixedUpdate()
        {
            if (_isRight) transform.Translate(Time.fixedDeltaTime * _velocityBullet * Vector3.right);
            else transform.Translate(Time.fixedDeltaTime * _velocityBullet * Vector3.left);
        }

        void DestroyGameObject()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.tag);
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().TakeDamage(50);
            }
            if (!other.CompareTag("Enermy")) DestroyGameObject();
        }
    }
}