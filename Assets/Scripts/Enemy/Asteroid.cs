using System;
using Asteroids.Utils;
using UnityEngine;

namespace Asteroids
{
    public class Asteroid : BaseEnemy
    {
        [SerializeField] private AsteroidType type;
        [SerializeField] private ConstantVelocity constantVelocity;
        public bool IsLarge => type == AsteroidType.Large;

        public override int DestroyScore => IsLarge ? scoringData.largeAsteroidScore : scoringData.smallAsteroidScore;

        public override void ReturnToPool() => objectPool.ReturnAsteroid(this);

        public void SpawnAsteroid(Vector3 position, Vector3 movementDirection)
        {
            base.Spawn(position);
            constantVelocity.Move(movementDirection);
        }
    }

    public enum AsteroidType : byte
    {
        Small = 0,
        Large = 1
    }
}
