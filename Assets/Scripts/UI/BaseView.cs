using UnityEngine;

namespace Asteroids.UI
{
    public abstract class BaseView : MonoBehaviour
    {
        public virtual void Show() => gameObject.SetActive(true);

        public virtual void Hide() => gameObject.SetActive(false);
    }
}
