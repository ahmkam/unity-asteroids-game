using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Utils
{
    public class LightWeightObjectPool
    {
        private Queue<GameObject> poolItems = new Queue<GameObject>();
        private List<GameObject> spawnedItems = new List<GameObject>();
        private GameObject prefab;
        private Transform parent;

        public LightWeightObjectPool(Transform parent, GameObject prefab, int initialPoolSize)
        {
            this.parent = parent;
            this.prefab = prefab;
            CreatePool(initialPoolSize);
        }

        private void CreatePool(int initialPoolSize)
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject item = InstantiatePrefab(prefab);
                poolItems.Enqueue(item);
            }
        }

        public GameObject GetItem(Action OnSpawnCallback = null)
        {
            GameObject item = null;

            if (poolItems.Count > 0)
                item = poolItems.Dequeue();
            else
                item = InstantiatePrefab(prefab);

            spawnedItems.Add(item);
            OnSpawnCallback?.Invoke();
            item.transform.SetParent(null);
            item.SetActive(true);
            return item;
        }

        public void ReturnItem(GameObject item)
        {
            item.transform.SetParent(parent);
            item.SetActive(false);
            poolItems.Enqueue(item);
            spawnedItems.Remove(item);
        }

        public void ReturnAllItems()
        {
            for (int i = spawnedItems.Count - 1; i != -1; i--)
            {
                ReturnItem(spawnedItems[i]);
            }
        }

        private GameObject InstantiatePrefab(GameObject prefab)
        {
            GameObject item = GameObject.Instantiate(prefab);
            item.transform.SetParent(parent);
            item.SetActive(false);
            return item;
        }
    }
}