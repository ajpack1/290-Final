using UnityEngine;

public class FloatingShapeSpawner : MonoBehaviour {
    public GameObject cubePrefab;
    public GameObject cylinderPrefab;

    public int spawnCount = 30;
    public float spawnRadius = 10f;

    void Start() {
        for (int i = 0; i < spawnCount; i++) {
            SpawnShape(cubePrefab);
            SpawnShape(cylinderPrefab);
            
        }
    }

    void SpawnShape(GameObject prefab) {
        Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
        GameObject shape = Instantiate(prefab, spawnPos, Random.rotation);
        shape.AddComponent<Rigidbody>().useGravity = false;
        shape.AddComponent<FloatingMotion>();
        
      
    }


}

