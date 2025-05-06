using UnityEngine;

public class FloatingShapeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject cylinderPrefab;

    public int spawnCount = 10;         // Reduced for testing
    public float spawnRadius = 5f;      // Smaller radius

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnShape(cubePrefab);
            SpawnShape(cylinderPrefab);
        }
    }

    void SpawnShape(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefab is missing!");
            return;
        }

        Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
        GameObject shape = Instantiate(prefab, spawnPos, Random.rotation);

        Rigidbody rb = shape.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = shape.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;

        shape.AddComponent<FloatingMotion>();
    }
}
