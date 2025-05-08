using UnityEngine;
using UnityEngine.Pool;

public class FloatingStarSpawner : MonoBehaviour
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
        spawnPos.y = 50f; // Set a fixed Y level if desired
        shape.transform.position = spawnPos;
        shape.transform.rotation = Random.rotation;

        if (shape.TryGetComponent(out FloatingStarMotion motion))
        {
            motion.Init(
                floatSpeed: 0.5f,
                floatRange: 1f,
                yLevel: spawnPos.y
            );
        }
    }


    GameObject CreatePooledObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        if (!obj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb = obj.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;

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
