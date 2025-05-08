using UnityEngine;

public class PuzzleSnappingManager : MonoBehaviour
{
    // Puzzle Setup
    public Transform[] puzzlePieces;          // Parent objects like Zoya CubeBOT R
    public Transform[] targetPositions;       // Empty GameObjects for correct locations
    public float snapThreshold = 0.2f;

    // Visuals for when in place
    public Material glowMaterial;             // Material with emission for glowing effect

    // Completion information
    public GameObject completionText;         // Optional TextMeshPro object
    private bool[] snapped;

    void Start()
    {
        snapped = new bool[puzzlePieces.Length];

        // Check if any pieces are already in the correct position
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            float dist = Vector3.Distance(puzzlePieces[i].position, targetPositions[i].position);
            if (dist <= snapThreshold)
            {
                // Snap into place
                puzzlePieces[i].position = targetPositions[i].position;
                puzzlePieces[i].rotation = targetPositions[i].rotation;
                snapped[i] = true;

                // Apply glow material
                Renderer r = puzzlePieces[i].GetComponentInChildren<Renderer>();
                if (r != null && glowMaterial != null)
                {
                    r.material = glowMaterial;
                }
            }
        }

        if (AllSnapped() && completionText != null)
            completionText.SetActive(true);
        else if (completionText != null)
            completionText.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (snapped[i]) continue;

            float dist = Vector3.Distance(puzzlePieces[i].position, targetPositions[i].position);
            if (dist <= snapThreshold)
            {
                // Snap into place
                puzzlePieces[i].position = targetPositions[i].position;
                puzzlePieces[i].rotation = targetPositions[i].rotation;
                snapped[i] = true;

                // Apply glow material
                Renderer r = puzzlePieces[i].GetComponentInChildren<Renderer>();
                if (r != null && glowMaterial != null)
                {
                    r.material = glowMaterial;
                }
            }
        }

        if (AllSnapped() && completionText != null)
        {
            completionText.SetActive(true);
        }
    }

    bool AllSnapped()
    {
        foreach (bool isSnapped in snapped)
            if (!isSnapped) return false;
        return true;
    }
}
