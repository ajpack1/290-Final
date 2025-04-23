using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelete : MonoBehaviour
{

// A variable to store reference to the array of objects from ObjectCreate script 
    private ObjectCreate objScript;

    // This variable will hold the list from the other class 
    private List<GameObject> arrOfObj;
// Reference to the hand position which has this script we need on it
    public GameObject handPos;
    // Start is called before the first frame update
    void Start()
    {
        objScript = handPos.GetComponent<ObjectCreate>();
        arrOfObj = objScript.objArr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
