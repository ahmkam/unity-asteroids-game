using UnityEngine;

namespace Asteroids.Utils
{
    [CreateAssetMenu(fileName = "ObjectPool", menuName = "ScriptableObjects/Object Pool")]
    public class ObjectPool : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject smallAsteroidPrefab;
        [SerializeField] private GameObject largeAsteroidPrefab;
        [SerializeField] private GameObject flyingSaucerPrefab;
        private bool isInitialized = false;
        private LightWeightObjectPool bulletPool;
        private LightWeightObjectPool smallAsteroidPool;
        private LightWeightObjectPool largeAsteroidPool;
        private LightWeightObjectPool flyingSaucerPool;

        public void Initialize()
        {
            if (isInitialized)
                return;

            isInitialized = true;
            Transform parent = new GameObject("PoolItems").transform;
            bulletPool = new LightWeightObjectPool(parent, bulletPrefab, 10);
            smallAsteroidPool = new LightWeightObjectPool(parent, smallAsteroidPrefab, 10);
            largeAsteroidPool = new LightWeightObjectPool(parent, largeAsteroidPrefab, 10);
            flyingSaucerPool = new LightWeightObjectPool(parent, flyingSaucerPrefab, 10);
        }

        public void ReturnAllEnemies()
        {
            smallAsteroidPool.ReturnAllItems();
            largeAsteroidPool.ReturnAllItems();
            flyingSaucerPool.ReturnAllItems();
        }

        public void ReturnAsteroid(Asteroid asteroid)
        {
            if (asteroid.IsLarge)
                ReturnLargeAsteroid(asteroid);
            else
                ReturnSmallAsteroid(asteroid);
        }

        public void ReturnAllBullets() => bulletPool.ReturnAllItems();

        public Bullet GetBullet() => bulletPool.GetItem().GetComponent<Bullet>();

        public void ReturnBullet(GameObject bullet) => bulletPool.ReturnItem(bullet);

        public Asteroid GetSmallAsteroid() => smallAsteroidPool.GetItem().GetComponent<Asteroid>();

        private void ReturnSmallAsteroid(Asteroid asteroid) => smallAsteroidPool.ReturnItem(asteroid.gameObject);

        public Asteroid GetLargeAsteroid() => largeAsteroidPool.GetItem().GetComponent<Asteroid>();

        private void ReturnLargeAsteroid(Asteroid asteroid) => largeAsteroidPool.ReturnItem(asteroid.gameObject);

        public FlyingSaucer GetFlyingSaucer() => flyingSaucerPool.GetItem().GetComponent<FlyingSaucer>();

        public void ReturnFlyingSaucer(FlyingSaucer saucer) => flyingSaucerPool.ReturnItem(saucer.gameObject);

        public void OnAfterDeserialize() => isInitialized = false;

        public void OnBeforeSerialize() { }
    }
}