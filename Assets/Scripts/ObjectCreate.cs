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
// Two integers to store how many objects we can spawn, and a count to keep track 
    public int objToAllow; 
    private int objCount;

    // public GameObject OVRRig;
// Reference to where the user is currently looking - will be used so shapes will generate in front of the user 
    public Transform centerEyeAnchor;
private Vector3 eyeAnchorPos; 
    // A private variable to hold the actual script - it is of the type of the script
    private ShapeRecognizerActiveState script;

// Gets reference to the ISDK game object with the script - we will spawn new objects with their components on them, and we can also put this script/object on them and update 
// ISDK values accordingly to new objects 
    // public GameObject ISDK; 

    // Vector3 to move our position 
    private Vector3 movePos;

    // Private counter variable to store the number of objects we have / what object number we have created (5 means this is the 6th object - zero index)
    private int objectIdx;

// A list to hold game objects we create - Unity forum helped to explain lists are better than array lists, as they can hold specific types, not just general object types
    private List<GameObject> objArr;

    // List to hold start positions of game objects
    private List<Vector3> objPos;

    // Vector 3 to hold the players position at any given time to determine where to place the objects 
    // private Vector3 playerPos;

    // Global boolean to help make it so one object at a time spwans 
    private int spawnOnce;
 
 // A variable to gain access to the ISDK component for hand grab interactable 
//  private HandGrabInteractable HGIP;

//  // A variable to then gain access to the specific component in the ISDK Hand Grab Interactable Script we need 
//  private PointableElement HGIP2;

 public GameObject objectToSpawn; 

 // Same thing as above but for other scripts
//  private Grabbable GB;
//  private GrabInteractable GBI;
//  private OneGrabTranslateTransformer OGT;

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
        // To draw a new object, the position must be detected, AND we must have our count at 1, so that one object can be created - we also have to have not exceeded max shapes to create
        if (script.Active && spawnOnce == 1 && objCount < objToAllow){
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

// Get the current position of the eye anchor 
eyeAnchorPos = centerEyeAnchor.position;  
        // Gain access to the current user position - set player pos to be slightly incremented forward so the object does not spawn in the user 
        // playerPos = headset.transform.position;
        // playerPos.z = playerPos.z + 2f;
        // playerPos.y = playerPos.y + 2f;

        // Add the shape/prefab to the array of objects
    objArr.Add(objectToSpawn);
    objCount++;

    
    // Add the vector3 to the position array 
    objPos.Add(new Vector3 (eyeAnchorPos.x, eyeAnchorPos.y, eyeAnchorPos.z));

    // Vector3 facing = centerEyeAnchor.forward + OVRRig.transform.position;
    // facing.y = 0;

    // Actually instantiate and draw the object here based off where the user is looking - x/z position will be increased slightly to ensure it does not spawn ontop of user
     Instantiate(objectToSpawn, new Vector3(eyeAnchorPos.x + 0.5f, eyeAnchorPos.y, eyeAnchorPos.z + 1.5f), Quaternion.identity);

// Set the newly created object's position in the space
    objArr[objectIdx].transform.position = objPos[objectIdx];
    
    // Increment the global value
    objectIdx++;
    }
}
