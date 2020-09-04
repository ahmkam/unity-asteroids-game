using Asteroids.Utils;
using UnityEngine;

namespace Asteroids
{
    public class ShipMovement : MonoBehaviour
    {
        [SerializeField] private float turnRate;
        [SerializeField] private float thurstSpeed;
        [SerializeField] private float maxSpeed;
        private ShipInput shipInput;
        private Rigidbody2D shipRb2D;
        private Vector3 startingPosition;

        void Start()
        {
            shipRb2D = GetComponent<Rigidbody2D>();
            shipInput = GetComponent<ShipInput>();
            startingPosition = transform.position;
        }

        public void Reset()
        {
            shipRb2D.velocity = Vector2.zero;
            shipRb2D.angularVelocity = 0;
            shipRb2D.rotation = 0;
            transform.position = startingPosition;
        }

        void FixedUpdate()
        {
            ApplyRotation(shipInput.RotationDirection);
            ApplyThrust(shipInput.Thrust);
            ClampMagnitude();
        }

        private void ApplyRotation(float direction) => shipRb2D.rotation += direction * turnRate * Time.fixedDeltaTime;

        private void ApplyThrust(float thrustValue) => shipRb2D.AddForce(transform.up * thrustValue * thurstSpeed * Time.fixedDeltaTime);

        private void ClampMagnitude() => shipRb2D.ClampMagnitude(maxSpeed);
    }
}