using System;
using UnityEngine;

namespace Asteroids
{
    public class ShipInput : MonoBehaviour
    {
        public Action OnFiredEvent;
        public float RotationDirection => -Input.GetAxis("Horizontal");
        public float Thrust => Mathf.Clamp01(Input.GetAxis("Vertical"));

        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                OnFiredEvent?.Invoke();
            }
        }
    }
}
