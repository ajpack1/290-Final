using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReset : MonoBehaviour
{

    public GameObject currObj;
    public GameObject groundObj;

    // public GameObject OVRrig;

    private Rigidbody rb;

    private Vector3 velocityNone = new Vector3 (0f, 0f, 0f);

// private GameObject OVRrig;
    // private Controller script;
    // private Vector3 OVRPosActive;
    // Rigidbody rb;

    private Vector3 startPos;
    // private Vector3 floorPos;
    // Start is called before the first frame update
    void Start()
    {
    //    rb = currObj.GetComponent<Rigidbody>();
    // When object is created, store its position 
    startPos = currObj.transform.position;
    // // floorPos = groundObj.transform.position;
    // rb = currObj.GetComponent<Rigidbody>();

    // OVRrig = GameObject.Find("OVRCameraRig");
    }

    // Update is called once per frame
    void Update()
    {
        // We need to create a variable with the updated player position / ball position each time update is called - this is so objects and the player know where they are relative to each other
        // Vector3 currPlayerPos = OVRrig.transform.position;
        // Vector3 currObjPos = currObj.transform.position;
        // // Observes the current object's distance away from the player on all axis
        // float objXDiff = currObjPos.x - currPlayerPos.x;
        // float objZDiff = currObjPos.z - currPlayerPos.z;
        // float objYDiff = currObjPos.y - currPlayerPos.y;
       // If the current object's y position is lower than the identified ground, reset it to its start position  - if the object is too far from the player reset it too, whether triggered because of being too far from the x y or z axis
        if (currObj.transform.position.y < groundObj.transform.position.y){
        // || objXDiff > 5f || objYDiff > 5f || objZDiff > 5f){
        //    Debug.Log("ZDiff:" + objZDiff);
        //    Debug.Log("PlayerPosZ:" + currPlayerPos);
        //    Debug.Log("ObjPosZ:" + currObjPos.z);
       
        //    resetObj(currPlayerPos);
        resetObj();
            rb.velocity = velocityNone;
        }
    }

// Reset the object to where the user currently is 
    void resetObj(){
        currObj.transform.position = startPos;
    }
}
