using UnityEngine;

namespace Asteroids.DataType
{

    [CreateAssetMenu(fileName = "ReactiveInt", menuName = "ScriptableObjects/Reactive Int")]
    public class ReactiveInt : ReactiveVariable<int>
    {
        private int lastValue;

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
