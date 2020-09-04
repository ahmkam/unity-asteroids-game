using Asteroids.Utils;
using UnityEngine;

namespace Asteroids
{
    public class BulletFire : MonoBehaviour
    {
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private Color bulletColor;
        [SerializeField] private float speed = 10;
        [SerializeField] private Transform fireTip;
        [SerializeField] private float fireRate = 0.2f;
        [SerializeField] private AudioSource fireSfx;
        public float LastFireTime { get; set; }

        public void Fire(Vector3 direction)
        {
            if (Time.time > LastFireTime)
            {
                fireSfx.Play();
                LastFireTime = Time.time + fireRate;
                Bullet item = objectPool.GetBullet();
                item.Spawn(targetLayer, fireTip.position, direction, bulletColor, speed);
            }
        }
    }
}
