using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.PoseDetection;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

// This is a script to create new objects when a pose is given 

public class ObjectCreate : MonoBehaviour
{
     // Public access to the game object OVR setup - we will use this to move the player based on a gesture
    public GameObject headset;
    // Gain acccess to the hand pose object we want to trigger our action 
    public GameObject handPose;

    // A private variable to hold the actual script - it is of the type of the script
    private ShapeRecognizerActiveState script;

    // Vector3 to move our position 
    private Vector3 movePos;

    // Private counter variable to store the number of objects we have / what object number we have created (5 means this is the 6th object - zero index)
    private int objectIdx;

// A list to hold game objects we create - Unity forum helped to explain lists are better than array lists, as they can hold specific types, not just general object types
    private List<GameObject> objArr;

    // List to hold start positions of game objects
    private List<Vector3> objPos;

    // Vector 3 to hold the players position at any given time to determine where to place the objects 
    private Vector3 playerPos;

    // Global boolean to help make it so one object at a time spwans 
    private int spawnOnce;
 

    // Start is called before the first frame update
    void Start()
    {
        // Array list to hold game objects - initalize it
        objArr = new List<GameObject>();

        // Initialize the other start pos list - if an object goes too far away from the player it can reset back to its origin point
        objPos = new List<Vector3>();

        // Initialize objectIdx
        objectIdx = 0; 

        // Get the handpose detection script component 
        script = handPose.GetComponent<ShapeRecognizerActiveState>();

        spawnOnce = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // To draw a new object, the position must be detected, AND we must have our count at 1, so that one object can be created 
        if (script.Active && spawnOnce == 1){
            drawShape();
            // After the code is run, spawnOnce should be set to 0 - only when script.Active is false can this be reset
            spawnOnce = 0;
        } else if (script.Active && spawnOnce == 0) { // Do not create a shape if the position is determined but count is 0 - just return 
           return;
            
            // Once false is hit and the pose is no longer detected we can then load up another object to be spawned
        } else if (!script.Active){
            spawnOnce = 1;
        }
    }

    public void drawShape(){

        // Gain access to the current user position - set player pos to be slightly incremented forward so the object does not spawn in the user 
        playerPos = headset.transform.position;
        playerPos.z = playerPos.z + 2f;
        playerPos.y = playerPos.y + 5f;

        // Draw the shape and add it to the array 
    objArr.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));

    
    // On the new objects, it adds a rigid body and the scripts components neccessary to pick up objects 
    GameObject newObj = objArr[objectIdx];
    newObj.AddComponent<Rigidbody>();
    newObj.AddComponent<XRGrabInteractable>();
    newObj.AddComponent<XRGeneralGrabTransformer>();
    newObj.AddComponent<Grabbable>();
    newObj.AddComponent<HandGrabInteractable>();
    newObj.AddComponent<GrabInteractable>();
    newObj.AddComponent<GrabFreeTransformer>();
    
    objPos.Add(new Vector3 (playerPos.x, playerPos.y, playerPos.z));

    objArr[objectIdx].transform.position = objPos[objectIdx];
    
    // Increment the global value
    objectIdx++;
    }
}
