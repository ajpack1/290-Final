using UnityEngine;

public class GlowAndColor : MonoBehaviour
{
    private Renderer rend;
    private float time;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogWarning("No Renderer found on object for glow!");
        }

        // Set initial color
        Color baseColor = Color.gray; // or white, blue, etc.
        rend.material.color = baseColor;

        // Enable emission
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", baseColor * 0.5f);
    }

    void Update()
    {
        if (rend == null) return;

        time += Time.deltaTime;
        float pulse = 0.5f + 0.5f * Mathf.Sin(time * 2f); // oscillates 0 to 1

        Color glowColor = Color.white; // change to any color you like

        // Pulse the emission
        rend.material.SetColor("_EmissionColor", glowColor * pulse);
    }
}
