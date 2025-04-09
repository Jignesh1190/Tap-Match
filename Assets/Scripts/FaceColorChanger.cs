using UnityEngine;

public class FaceColorChanger : MonoBehaviour
{
    public Color[] possibleColors; // Array to hold possible colors

    private Renderer objectRenderer;
    private Material[] materials;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        materials = objectRenderer.materials;

        // Ensure each material is unique to this object
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = new Material(materials[i]);
        }
        objectRenderer.materials = materials;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detects mouse click or touch
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
                if (hitRenderer != null && hitRenderer == objectRenderer)
                {
                    // Determine which material index was hit
                    int materialIndex = GetMaterialIndex(hit);
                    if (materialIndex >= 0 && materialIndex < materials.Length)
                    {
                        // Assign a random color from the possibleColors array
                        materials[materialIndex].color = possibleColors[Random.Range(0, possibleColors.Length)];
                        objectRenderer.materials = materials;
                    }
                }
            }
        }
    }

    int GetMaterialIndex(RaycastHit hit)
    {
        // Implement logic to determine which material index corresponds to the hit point
        // This is non-trivial and depends on the mesh's UV mapping and collider setup
        // For simplicity, this function returns -1, indicating an undetermined index
        return -1;
    }
}
