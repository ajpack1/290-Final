using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReset : MonoBehaviour
{

    public GameObject currObj;
    public GameObject groundObj;
    // Rigidbody rb;

    private Vector3 startPos;
    private Vector3 floorPos;
    // Start is called before the first frame update
    void Start()
    {
    //    rb = currObj.GetComponent<Rigidbody>();
    // When object is created, store its position 
    startPos = currObj.transform.position;
    floorPos = groundObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       // If the current object's y position is lower than the identified ground, reset it to its start position 
        if (currObj.transform.position.y < groundObj.transform.position.y){
            currObj.transform.position = startPos;
        }
    }
}
