using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserOrigin;
    public float laserLength = 100f;
    public LineRenderer lineRenderer;
    public LayerMask collisionMask;

    public Vector3 respawnPosition; 
    public GameObject OVRRig; // Reference to OVRCameraRig

    private CharacterController characterController;

    void Start()
    {
        characterController = OVRRig.GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 origin = laserOrigin.position;
        Vector3 direction = laserOrigin.forward;

        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, laserLength, collisionMask))
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Player") && characterController != null)
            {
                characterController.enabled = false;
                OVRRig.transform.position = respawnPosition;
                characterController.enabled = true;
            }
        }
        else
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, origin + direction * laserLength);
        }

        Debug.DrawRay(origin, direction * laserLength, Color.red);
    }
}
