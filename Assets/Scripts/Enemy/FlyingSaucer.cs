using System;
using Asteroids.Utils;
using UnityEngine;

namespace Asteroids
{
    public class FlyingSaucer : BaseEnemy
    {
        [SerializeField] private FlyingSaucerMovement flyingSaucerMovement;
        [SerializeField] private FlyingSaucerFireBehaviour flyingSaucerFireBehaviour;

        public override void OnEnable()
        {
            base.OnEnable();
            GameEvents.OnGameOverEvent += OnGameOver;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            GameEvents.OnGameOverEvent -= OnGameOver;
        }

        public override void OnHit(Collider2D other)
        {
            base.OnHit(other);
            flyingSaucerFireBehaviour.SetIsFiring(false);
        }

        public override void ReturnToPool() => objectPool.ReturnFlyingSaucer(this);

        public override int DestroyScore => scoringData.saucerScore;

        public void SpawnFlyingSaucer(Vector3 position, Vector3 movementDirection)
        {
            base.Spawn(position);
            flyingSaucerMovement.Move(movementDirection);
            flyingSaucerFireBehaviour.SetIsFiring(true);
        }

        private void OnGameOver() => flyingSaucerFireBehaviour.SetIsFiring(false);
    }

}