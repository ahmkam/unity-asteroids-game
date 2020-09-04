using UnityEngine;

namespace Asteroids.DataType
{
    [CreateAssetMenu(fileName = "ReactiveBool", menuName = "ScriptableObjects/Reactive Bool")]
    public class ReactiveBool : ReactiveVariable<bool>
    {
        private bool lastValue;

        private void OnValidate()
        {
            if (Value != lastValue)
            {
                lastValue = Value;
                FireChangeEvent();
            }
        }
    }
}
