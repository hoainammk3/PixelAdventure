using UnityEngine;

namespace Script
{
    public class ShotEnermy : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private bool canShoot = true;

        [SerializeField] private float delayTimeShoot = 1f;
        private Transform _bulletSpawnPos;
        private float _timer;

        [SerializeField] private State state;
        enum State
        {
            Chase, Shot, Hurt
        }
        private void Awake()
        {
            state = State.Chase;
            _bulletSpawnPos = GetComponentInChildren<Transform>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= delayTimeShoot)
            {
                _timer = 0;
                canShoot = true;
            }
            if (Vector3.Distance(transform.position, _player.transform.position) < 10f)
            {
                state = State.Shot;
            }
            else
            {
                state = State.Chase;
            }
        }

        private void FixedUpdate()
        {
            Debug.DrawLine(transform.position - 10 * Vector3.right, transform.position + 10f * Vector3.right);
            if (state == State.Shot && canShoot)
            {
                Shot(_player.transform);
                canShoot = false;
            }

        }

        void Shot(Transform player)
        {
            if (Vector3.Distance(player.position, transform.position) > 10f)
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
                Bullet temp = Instantiate(_bulletPrefab, _bulletSpawnPos.position, Quaternion.identity);
                temp.gameObject.SetActive(true);
                temp.IsRight = true;
            }

            if (distance.x < -0)
            {
                var localScale = transform.localScale;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                transform.localScale = localScale;
                Bullet temp = Instantiate(_bulletPrefab, _bulletSpawnPos.position, Quaternion.identity);
                temp.gameObject.SetActive(true);
                temp.IsRight = false;
            }
            AudioManager.Instance.PlayEnermyShotClip();
        }
    }
}