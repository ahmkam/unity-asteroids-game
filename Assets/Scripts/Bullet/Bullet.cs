using System.Collections;
using Asteroids.Utils;
using UnityEngine;

namespace Asteroids
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private float lifeTime = 1.5f;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private ConstantVelocity constantVelocity;
        [SerializeField] private GameObjectCollisionTrigger bulletCollision;

        private void OnEnable() => bulletCollision.OnHitEvent += OnHit;

        private void OnDisable() => bulletCollision.OnHitEvent -= OnHit;

        public void Spawn(LayerMask layer, Vector3 position,
         Vector3 movementDirection, Color spriteColor, float speed)
        {
            string name = LayerMask.LayerToName((int)(Mathf.Log(layer.value, 2)));
            gameObject.layer = LayerMask.NameToLayer(name);
            constantVelocity.Move(movementDirection, speed);
            bulletCollision.SetColliderActive(true);
            transform.position = position;
            sprite.color = spriteColor;
            StartCoroutine(LifeTimeRoutine());
        }

        private void ReturnBullet()
        {
            bulletCollision.SetColliderActive(false);
            objectPool.ReturnBullet(gameObject);
        }

        private IEnumerator LifeTimeRoutine()
        {
            yield return new WaitForSeconds(lifeTime);
            ReturnBullet();
        }

        private void OnHit(Collider2D other) => ReturnBullet();
    }
}
