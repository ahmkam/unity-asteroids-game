using UnityEngine;

namespace Asteroids
{
    public class FlyingSaucerFireBehaviour : MonoBehaviour
    {
        [SerializeField] BulletFire saucerFiring;
        [SerializeField] private float fireRate = 0.2f;
        private bool isFiring;

        public void SetIsFiring(bool firing)
        {
            isFiring = firing;
            saucerFiring.LastFireTime = Time.time + fireRate;
        }

        private void Update()
        {
            if (!isFiring)
                return;

            saucerFiring.Fire(Random.insideUnitCircle.normalized);
        }
    }
}
