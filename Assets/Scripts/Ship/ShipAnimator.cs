using UnityEngine;
using DG.Tweening;
using System;

namespace Asteroids
{
    public class ShipAnimator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;

        public void SetSpriteActive(bool isActive) => sprite.enabled = isActive;

        public void Blink(Action OnBlinkComplete) => sprite.DOFade(0f, 0.2f).SetLoops(12, LoopType.Yoyo)
        .OnComplete(() => OnBlinkComplete?.Invoke());
    }
}
