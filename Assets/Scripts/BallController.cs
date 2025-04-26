using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float bounceForce = 8f;    // Speed of bounce
    public Color currentBallColor;    // Ball's current color
    private Rigidbody rb;             // Ball's Rigidbody for physics interaction
    private bool gameEnded = false;   // Track if the game ended
    private float maxY = 6f;          // Max Y limit for ball bounce

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetRandomBallColor(); // Set initial color of the ball
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameEnded) return;  // Prevent further collisions if game ended

        if (collision.gameObject.CompareTag("CubeFace"))
        {
            // Access the CubeFace script attached to the collided face
            CubeFace face = collision.gameObject.GetComponent<CubeFace>();

            if (face != null)
            {
                Color faceColor = face.faceColor;  // Get the face color

                // Check if the ball color matches the face color
                if (ColorsMatch(currentBallColor, faceColor))
                {
                    Debug.Log("✅ Color Match - Ball Bounces");

                    // Stop the ball's current movement, then apply bounce force
                    rb.velocity = Vector3.zero;
                    rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

                    // Set the ball's color to a random color after bounce
                    Invoke(nameof(SetRandomBallColor), 0.2f); // Delay color change
                }
                else
                {
                    Debug.Log("❌ Color Mismatch - Game Over");
                    gameEnded = true;
                    Invoke(nameof(RestartGame), 0.5f); // Restart game after a short delay
                }
            }
        }
    }

    void SetRandomBallColor()
    {
        // Randomly set the ball's color (you can expand this with more colors)
        Color[] possibleColors = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.cyan };
        currentBallColor = possibleColors[Random.Range(0, possibleColors.Length)];
        GetComponent<Renderer>().material.color = currentBallColor; // Change ball color
    }

    bool ColorsMatch(Color a, Color b)
    {
        // Tolerant color match (you can adjust the threshold as needed)
        return Mathf.Abs(a.r - b.r) < 0.1f && Mathf.Abs(a.g - b.g) < 0.1f && Mathf.Abs(a.b - b.b) < 0.1f;
    }

    void RestartGame()
    {
        Debug.Log("Restarting Game...");
        Time.timeScale = 1f; // Resume time if it's paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    }

}
