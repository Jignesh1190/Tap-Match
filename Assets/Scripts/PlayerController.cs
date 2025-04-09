using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 1f; // Sensitivity of rotation
    public float smoothTime = 0.5f;    // Smoothing time

    private Vector3 lastTouchPosition;
    private Vector3 touchDelta;
    private Vector3 currentVelocity;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    void Start()
    {
        currentRotation = transform.localEulerAngles;
        targetRotation = currentRotation;
    }

    void Update()
    {
        HandleTouchInput();
        SmoothRotate();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                touchDelta = touch.deltaPosition;

                targetRotation.y += touchDelta.x * rotationSpeed;
                targetRotation.x -= touchDelta.y * rotationSpeed;
            }
            else
            {
                // No swipe movement — stop changing rotation
                touchDelta = Vector2.zero;
            }
        }
    }
      void SmoothRotate()
    {
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref currentVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;
    }
}
