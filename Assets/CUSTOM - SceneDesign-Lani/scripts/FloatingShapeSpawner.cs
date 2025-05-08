using UnityEngine;

public class FloatingShapeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject cylinderPrefab;

    public int spawnCount = 10;
    public float spawnRadius = 10f;
    public float floatSpeed = 0.5f;
    public float floatRange = 1f;
    public float fixedY = 1f;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnFloatingObject(cubePrefab);
            SpawnFloatingObject(cylinderPrefab);
        }
    }

    void SpawnFloatingObject(GameObject prefab)
    {
        Vector3 spawnPos = transform.position + new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            0f,
            Random.Range(-spawnRadius, spawnRadius)
        );
        spawnPos.y = fixedY;

        GameObject obj = Instantiate(prefab, spawnPos, Random.rotation);
        obj.AddComponent<FloatingMotion>().Init(floatSpeed, floatRange, fixedY);
    }
}
