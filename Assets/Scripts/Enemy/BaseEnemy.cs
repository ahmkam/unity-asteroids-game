using System;
using Asteroids.Utils;
using UnityEngine;

namespace Asteroids
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected ObjectPool objectPool;
        [SerializeField] protected ScoringData scoringData;
        [SerializeField] private GameObjectCollisionTrigger collision;
        public Action<BaseEnemy> OnDestroyedEvent;

        public abstract int DestroyScore { get; }

        public abstract void ReturnToPool();

        public virtual void OnEnable() => collision.OnHitEvent += OnHit;

        public virtual void OnDisable() => collision.OnHitEvent -= OnHit;

        public virtual void Spawn(Vector3 position) => transform.position = position;

        public virtual void OnHit(Collider2D other) => OnDestroyedEvent?.Invoke(this);
    }
}
