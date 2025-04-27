using UnityEngine;

public class FloatingMotion : MonoBehaviour {
    private Vector3 randomDir;
    private float floatSpeed;
    private float rotationSpeed;

    void Start() {
        randomDir = Random.insideUnitSphere.normalized;
        floatSpeed = Random.Range(0.2f, 0.5f);
        rotationSpeed = Random.Range(10f, 50f);
    }

    void Update() {
        transform.Translate(randomDir * floatSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }
}