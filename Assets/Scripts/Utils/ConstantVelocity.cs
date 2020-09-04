using System.Collections;
using UnityEngine;

namespace Asteroids.Utils
{
    public class ConstantVelocity : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 3f;
        [SerializeField] private Rigidbody2D gameObjectRigidbody2D;
        private Vector3 direction;

        void FixedUpdate() => gameObjectRigidbody2D.velocity = direction * movementSpeed;

        public void Move(Vector3 movementDirection, float speed)
        {
            direction = movementDirection;
            movementSpeed = speed;
        }

        public void Move(Vector3 movementDirection) => direction = movementDirection;
    }
}
