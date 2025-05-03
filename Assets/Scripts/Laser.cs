using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserOrigin;
    public float laserLength = 100f;
    public LineRenderer lineRenderer;
    public LayerMask collisionMask;

    void Update()
    {
        Vector3 origin = laserOrigin.position;
        Vector3 direction = laserOrigin.forward;

        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, laserLength, collisionMask))
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Player"))
            {
                PlayerReset reset = hit.collider.GetComponent<PlayerReset>();
                if (reset != null)
                    reset.ResetPosition();
            }
        }
        else
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, origin + direction * laserLength);
        }
    }
}
