using System;
using UnityEngine;

namespace Asteroids.DataType
{
    public class ReactiveVariable<T> : ScriptableObject
    {
        [SerializeField] private T value;
        private Action<T> OnValueChangedEvent;

        public void Subscribe(Action<T> callback, bool forceUpdate = false)
        {
            OnValueChangedEvent += callback;
            if (forceUpdate)
                callback?.Invoke(Value);
        }

        public void Unsubscribe(Action<T> callback) => OnValueChangedEvent -= callback;

        public void FireChangeEvent() => OnValueChangedEvent?.Invoke(this.value);

        public T Value
        {
            get => this.value;
            set
            {
                this.value = value;
                FireChangeEvent();
            }
        }
    }
}
