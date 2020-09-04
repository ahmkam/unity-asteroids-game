using Asteroids.Utils;
using UnityEngine;

public class WrapPosition : MonoBehaviour
{
    [SerializeField] private float wrapMargin = 0.25f;
    private Camera gameCamera;
    private Vector3 screenLimits;
    private float gameHalfHeight;
    private Rigidbody2D gameObjectRb2D;

    void Start()
    {
        gameCamera = Camera.main;
        gameObjectRb2D = GetComponent<Rigidbody2D>();
        gameHalfHeight = gameCamera.orthographicSize;
        screenLimits.x = gameHalfHeight * gameCamera.aspect;
        screenLimits.y = gameHalfHeight;
    }

    //to handle editor orthographic re-sizing
#if UNITY_EDITOR
    void Update() => screenLimits.x = gameHalfHeight * gameCamera.aspect;
#endif

    void FixedUpdate() => Wrap();

    public void Wrap()
    {
        if (gameObjectRb2D.position.x + wrapMargin < -screenLimits.x)
        {
            gameObjectRb2D.SetPositionX(screenLimits.x + wrapMargin);
        }
        else if (gameObjectRb2D.position.x - wrapMargin > screenLimits.x)
        {
            gameObjectRb2D.SetPositionX(-screenLimits.x - wrapMargin);
        }

        if (gameObjectRb2D.position.y + wrapMargin < -screenLimits.y)
        {
            gameObjectRb2D.SetPositionY(screenLimits.y + wrapMargin);
        }
        else if (gameObjectRb2D.position.y - wrapMargin > screenLimits.y)
        {
            gameObjectRb2D.SetPositionY(-screenLimits.y - wrapMargin);
        }
    }
}
