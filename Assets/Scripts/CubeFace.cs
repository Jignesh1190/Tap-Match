using UnityEngine;

public class CubeFace : MonoBehaviour
{
    public Color faceColor;  // Each face has its own color

    void Start()
    {
        // Make sure that the color is applied to the cube face (Renderer)
        Renderer cubeRenderer = GetComponentInChildren<Renderer>();

        if (cubeRenderer != null)
        {
            cubeRenderer.material.color = faceColor;  // Set face color
        }
        else
        {
            Debug.LogError("No Renderer found on this face.");
        }
    }
}
