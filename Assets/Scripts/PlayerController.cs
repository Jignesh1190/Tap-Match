using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 0.05f;  // Lower = slower
    public float smoothTime = 0.2f;      // Smoothing feel

    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private Vector3 rotationVelocity;

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
                Vector2 delta = touch.deltaPosition;

                // Optional clamp to avoid large sudden swipes
                delta = Vector2.ClampMagnitude(delta, 20f);

                // Apply only small rotation increments
                targetRotation.y += delta.x * rotationSpeed;
                targetRotation.x -= delta.y * rotationSpeed;
            }
        }

#if UNITY_EDITOR
        HandleMouseInput(); // Editor drag test
#endif
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            targetRotation.y += mouseX * rotationSpeed * 10f * Time.deltaTime;
            targetRotation.x -= mouseY * rotationSpeed * 10f * Time.deltaTime;
        }
    }

    void SmoothRotate()
    {
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;
    }
}
