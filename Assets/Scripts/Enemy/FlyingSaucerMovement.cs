using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public class FlyingSaucerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 3f;
        [SerializeField] private Rigidbody2D gameObjectRigidbody2D;
        [SerializeField] private float directionFlipInterval = 5f;
        private Vector3 direction;
        private float lastDirectionSwitchTime = 0f;
        private bool isMoving = false;

        public void Move(Vector3 movementDirection)
        {
            direction = movementDirection;
            lastDirectionSwitchTime = Time.deltaTime;
            isMoving = true;
        }

        void FixedUpdate()
        {
            if (!isMoving)
                return;

            if (Time.time > lastDirectionSwitchTime)
            {
                lastDirectionSwitchTime = Time.time + directionFlipInterval;
                direction.y *= -1f;
            }
            gameObjectRigidbody2D.velocity = direction * movementSpeed;
        }
    }
}