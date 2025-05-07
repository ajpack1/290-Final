using UnityEngine;

public class FloatingMotion : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 randomOffset;
    private float speed;
    private float range;
    private float fixedY;

    private Vector3 spinAxis;
    private float spinSpeed;

    private Renderer objRenderer;
    private Material objMaterial;

    private Color glowColorA = new Color(1f, 0f, 1f);   // Pink/Purple (normalized)
    private Color glowColorB = new Color(0f, 1f, 1f);   // Turquoise Blue (normalized)
    private float glowSpeed = 2f;                       // Color transition speed

    private float intensity = 2f;                      // Fixed emission intensity

    public void Init(float floatSpeed, float floatRange, float yLevel)
    {
        startPos = transform.position;
        speed = floatSpeed;
        range = floatRange;
        fixedY = yLevel;

        randomOffset = new Vector3(
            Random.Range(0f, 100f),
            Random.Range(0f, 100f),
            Random.Range(0f, 100f)
        );

        spinAxis = Random.onUnitSphere;
        spinSpeed = Random.Range(20f, 60f);

        objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
        {
            objMaterial = objRenderer.material;
            objMaterial.EnableKeyword("_EMISSION");
        }
    }

    void Update()
    {
        float time = Time.time * speed;

        // Smooth XZ drifting, fixed Y
        float offsetX = Mathf.Sin(time + randomOffset.x) * range;
        float offsetZ = Mathf.Cos(time + randomOffset.z) * range;

        transform.position = new Vector3(
            startPos.x + offsetX,
            fixedY,
            startPos.z + offsetZ
        );

        // Continuous spin
        transform.Rotate(spinAxis, spinSpeed * Time.deltaTime);

        // Animate glow color (lerping without affecting intensity)
        if (objMaterial != null)
        {
            float t = (Mathf.Sin(Time.time * glowSpeed) + 1f) * 0.5f;  // t = 0 → 1
            Color lerpedGlow = Color.Lerp(glowColorA, glowColorB, t) * intensity;
            objMaterial.SetColor("_EmissionColor", lerpedGlow);
        }
    }
}
