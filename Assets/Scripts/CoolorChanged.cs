using UnityEngine;

public class CoolorChanged : MonoBehaviour
{
  
    private Renderer cubeRenderer;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        RandomizeFaceColors();
    }

    public void RandomizeFaceColors()
    {
        // Ensure the cube has the correct number of materials assigned
        if (cubeRenderer.materials.Length == 6)
        {
            foreach (var material in cubeRenderer.materials)
            {
                material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
        else
        {
            Debug.LogWarning("Cube does not have 6 materials assigned.");
        }
    }
}
