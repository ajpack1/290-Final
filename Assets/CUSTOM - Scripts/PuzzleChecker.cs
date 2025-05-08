using UnityEngine;

public class PuzzleSnappingManager : MonoBehaviour
{
    public Transform[] puzzlePieces;
    public Transform[] targetPositions;
    public float snapThreshold = 0.2f;

    public Material glowMaterial;
    public GameObject wallToDisable;

    private bool puzzleCompleted = false;

    void Update()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            Renderer r = puzzlePieces[i].GetComponentInChildren<Renderer>();
            if (r == null || r.material == glowMaterial) continue;

            float dist = Vector3.Distance(puzzlePieces[i].position, targetPositions[i].position);
            if (dist <= snapThreshold)
            {
                puzzlePieces[i].position = targetPositions[i].position;
                puzzlePieces[i].rotation = targetPositions[i].rotation;

                // Apply glow
                r.material = glowMaterial;
            }
        }

        if (!puzzleCompleted && AllPiecesGlowing())
        {
            puzzleCompleted = true;
            if (wallToDisable != null)
                wallToDisable.SetActive(false);
        }
    }

    bool AllPiecesGlowing()
    {
        foreach (Transform piece in puzzlePieces)
        {
            Renderer r = piece.GetComponentInChildren<Renderer>();
            if (r == null || r.sharedMaterial.name != glowMaterial.name)
                return false;
        }
        return true;
    }
}
