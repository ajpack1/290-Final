using UnityEngine;
using UnityEngine.Pool;

public class FloatingStarSpawn : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject cylinderPrefab;
    public int spawnCount = 10;
    public float spawnRadius = 5f;

    private ObjectPool<GameObject> cubePool;
    private ObjectPool<GameObject> cylinderPool;

    void Start()
    {
        // Create cube pool
        cubePool = new ObjectPool<GameObject>(
            () => CreatePooledObject(cubePrefab),
            obj => obj.SetActive(true),
            obj => obj.SetActive(false),
            null,
            false, 50
        );
        // Create cylinder pool
        cylinderPool = new ObjectPool<GameObject>(
            () => CreatePooledObject(cylinderPrefab),
            obj => obj.SetActive(true),
            obj => obj.SetActive(false),
            null,
            false, 50
        );

        // Spawn shapes using pools
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnShape(cubePool);
            SpawnShape(cylinderPool);
        }
    }

    void SpawnShape(ObjectPool<GameObject> pool)
    {
        GameObject shape = pool.Get();

        Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
        shape.transform.position = spawnPos;
        shape.transform.rotation = Random.rotation;

        // Reset floating position
        if (shape.TryGetComponent(out FloatingStarMotion motion))
        {
            motion.enabled = false;
            motion.enabled = true; // Reset floating motion
        }
    }

    GameObject CreatePooledObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);

        if (!obj.GetComponent<FloatingStarMotion>())
        {
            obj.AddComponent<FloatingStarMotion>();
        }
        if (!obj.GetComponent<GlowAndColor>())
        {
            obj.AddComponent<GlowAndColor>();
        }
        return obj;
    }
}