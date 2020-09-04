using System;
using UnityEngine;

namespace Asteroids
{
    public class GameObjectCollisionTrigger : MonoBehaviour
    {
        [SerializeField] private Collider2D collider2D;
        public Action<Collider2D> OnHitEvent;

        void Start() => collider2D = GetComponent<Collider2D>();

        private void OnTriggerEnter2D(Collider2D other) => OnHitEvent?.Invoke(other);

        public void SetColliderActive(bool isActive) => collider2D.enabled = isActive;
    }
}
