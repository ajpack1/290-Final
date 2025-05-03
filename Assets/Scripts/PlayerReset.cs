using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    public Vector3 respawnPosition;

    void Start()
    {
        // Set respawn point at the start
        respawnPosition = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = respawnPosition;
    }
}
