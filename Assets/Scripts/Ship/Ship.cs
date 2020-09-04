using System.Collections;
using Asteroids.DataType;
using UnityEngine;

namespace Asteroids
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private ShipAnimator shipAnimator;
        [SerializeField] private ShipInput shipInput;
        [SerializeField] private ShipMovement shipMovement;
        [SerializeField] private BulletFire shipFiring;
        [SerializeField] private GameObjectCollisionTrigger shipCollision;
        [SerializeField] private AudioSource blastSfx;
        [SerializeField] private SharedData sharedData;

        private void Start() => SetComponentsActive(false);

        private void OnEnable()
        {
            shipInput.OnFiredEvent += OnFired;
            shipCollision.OnHitEvent += OnShipHit;
            GameEvents.OnGameStartEvent += OnGameStart;
        }

        private void OnDisable()
        {
            shipInput.OnFiredEvent -= OnFired;
            shipCollision.OnHitEvent -= OnShipHit;
            GameEvents.OnGameStartEvent -= OnGameStart;
        }

        private void SetComponentsActive(bool isActive)
        {
            shipMovement.enabled = isActive;
            shipFiring.enabled = isActive;
            shipInput.enabled = isActive;
            shipCollision.enabled = isActive;
            shipCollision.SetColliderActive(isActive);
            shipAnimator.SetSpriteActive(isActive);
        }

        private IEnumerator OnRespawnRoutine()
        {
            shipInput.enabled = false;
            shipAnimator.SetSpriteActive(false);
            shipCollision.SetColliderActive(false);
            Reset();

            yield return new WaitForSeconds(1f);

            shipInput.enabled = true;
            shipAnimator.SetSpriteActive(true);
            shipAnimator.Blink(() => shipCollision.SetColliderActive(true));
        }

        private void OnShipHit(Collider2D other)
        {
            if (sharedData.CurrentLives == 0)
            {
                SetComponentsActive(false);
                sharedData.SetGameOver(true);
                GameEvents.OnGameOverEvent?.Invoke();
                Reset();
            }
            else
            {
                sharedData.DecrementLives();
                StartCoroutine(OnRespawnRoutine());
            }

            blastSfx.Play();
        }

        private void OnGameStart() => SetComponentsActive(true);

        private void Reset() => shipMovement.Reset();

        private void OnFired() => shipFiring.Fire(transform.up);
    }
}