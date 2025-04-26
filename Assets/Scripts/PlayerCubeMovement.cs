using UnityEngine;

public class PlayerCubeMovement : MonoBehaviour
{
    public float rotationSpeed = 5f;  // How fast the cube rotates
    private Vector2 touchStartPos;
    private bool isTouching = false;
    private Vector3 targetRotation;

    void Update()
    {
        HandleTouchInput();
        RotateCube();
    }

    void HandleTouchInput()
    {
        // Detect if there is touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // If it's the beginning of the touch, store the touch position
            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Calculate the difference in touch position
                Vector2 touchDelta = touch.position - touchStartPos;

                // Rotate the cube based on touch movement
                targetRotation = new Vector3(touchDelta.y, -touchDelta.x, 0) * rotationSpeed;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }
        else
        {
            isTouching = false;
        }
    }

    void RotateCube()
    {
        // Smoothly rotate the cube towards the target rotation
        if (isTouching)
        {
            transform.Rotate(targetRotation * Time.deltaTime);
        }
        else
        {
            // Smooth stop when there's no touch input
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * rotationSpeed);
        }
    }
}
