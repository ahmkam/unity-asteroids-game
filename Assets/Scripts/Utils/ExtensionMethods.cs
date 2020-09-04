using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Utils
{
    public static class ExtensionMethods
    {
        public static void SetPositionX(this Rigidbody2D rigidbody2D, float x)
        {
            Vector2 position = rigidbody2D.position;
            position.x = x;
            rigidbody2D.position = position;
        }

        public static void SetPositionY(this Rigidbody2D rigidbody2D, float y)
        {
            Vector2 position = rigidbody2D.position;
            position.y = y;
            rigidbody2D.position = position;
        }

        public static void ClampMagnitude(this Rigidbody2D rigidbody2D, float maxSpeed) => rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxSpeed);
    }
}
