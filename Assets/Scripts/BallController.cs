using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float bounceForce = 8f;    // Speed of bounce
    public int currentID = 0;
    public CubeFace[] cubeFaces;
    private Rigidbody rb;             // Ball's Rigidbody for physics interaction
    private bool gameEnded = false;   // Track if the game ended
    //private float maxY = 6f;          // Max Y limit for ball bounce
    private MeshRenderer meshRenderer;
    private Color[] possibleColors = new Color[6];

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        SetPossibleColours();
        SetRandomBallColor();

    }
    private void SetPossibleColours() 
    {
        for (int i = 0; i < cubeFaces.Length; i++)
        {
            possibleColors[i] = cubeFaces[i].faceColor;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameEnded) return;  // Prevent further collisions if game ended

        if(collision.gameObject.TryGetComponent(out CubeFace face)) 
        {
            if (face.CompareID(currentID)) 
            {
                Debug.Log("✅ Color Match - Ball Bounces");

                rb.angularVelocity = Vector3.zero;
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

                // Set the ball's color to a random color after bounce
                Invoke(nameof(SetRandomBallColor), 0.1f); // Delay color change
            }
            else 
            {
                Debug.Log("❌ Color Mismatch - Game Over");
                gameEnded = true;
                Invoke(nameof(RestartGame), 0.5f); // Restart game after a short delay
            }
        }
    }

    void SetRandomBallColor()
    {
        int randID = Random.Range(0, possibleColors.Length);

        currentID = randID;

        meshRenderer.material.color = possibleColors[randID];
    }
    void RestartGame()
    {
        Debug.Log("Restarting Game...");
        Time.timeScale = 1f; // Resume time if it's paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    }

}
