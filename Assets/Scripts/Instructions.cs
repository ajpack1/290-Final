using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
// The game object we want to trigger our instructions when our position matches up 
    public GameObject objToTrigger;
    private Vector3 objTriggerPos;
    public GameObject OVRRig;

    public GameObject textPrefab;

    public Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        objTriggerPos = objToTrigger.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPlayerPos = OVRRig.transform.position; 
        spawnPos = currPlayerPos;
        // spawnPos.x
        if (currPlayerPos.z >= objTriggerPos.z && currPlayerPos.z <= objTriggerPos.z +  5f){
            // Spawn text here 
        }
    }
}
