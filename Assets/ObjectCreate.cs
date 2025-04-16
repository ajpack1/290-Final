using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.Editor;
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

// Gets reference to the ISDK game object with the script - we will spawn new objects with their components on them, and we can also put this script/object on them and update 
// ISDK values accordingly to new objects 
    public GameObject ISDK; 

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
 
 // A variable to gain access to the ISDK component for hand grab interactable 
 private HandGrabInteractable HGIP;

 // A variable to then gain access to the specific component in the ISDK Hand Grab Interactable Script we need 
 private PointableElement HGIP2;

 // Same thing as above but for other scripts
 private Grabbable GB;
 private GrabInteractable GBI;
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

// Get the hand grab interactable from the ISDK object - this object contains what scripts we need for interaction in the spawned objects
        HGIP = ISDK.GetComponent<HandGrabInteractable>();

        // Gain the script we need - also used by grab interactable 
        // HGIP2 = HGIP.GetComponent<PointableElement>();

// Get the scripts needed 
        GB = ISDK.GetComponent<Grabbable>();

        GBI = ISDK.GetComponent<GrabInteractable>();
        // OGT = GB.GetComponent<OneGrabTranslateTransformer>();

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
    Rigidbody rb = newObj.AddComponent<Rigidbody>();
    newObj.AddComponent<XRGrabInteractable>();
    newObj.AddComponent<XRGeneralGrabTransformer>();
    // newObj.AddComponent<Grabbable>();
    // newObj.AddComponent<HandGrabInteractable>();
    // newObj.AddComponent<GrabInteractable>();
    // newObj.AddComponent<GrabFreeTransformer>();
GB.InjectOptionalRigidbody(rb);
HGIP.InjectRigidbody(rb);
GBI.InjectRigidbody(rb);

    
// Gives these scripts the rigidbodies they need 
    // newObj.GetComponent<Grabbable>().InjectOptionalRigidbody(newObj.GetComponent<Rigidbody>());
    // newObj.GetComponent<HandGrabInteractable>().InjectRigidbody(newObj.GetComponent<Rigidbody>());
    // newObj.GetComponent<GrabInteractable>().InjectRigidbody(newObj.GetComponent<Rigidbody>());
   
   // Apply the components we wanted from ISDK to the current scripts we need these ISDK components in 
    // newObj.GetComponent<HandGrabInteractable>().InjectOptionalPointableElement(HGIP2);

   
    // newObj.GetComponent<Grabbable>().InjectOptionalOneGrabTransformer(OGT);
    // newObj.GetComponent<GrabInteractable>().InjectOptionalPointableElement(HGIP2);
    ISDK.GetComponent<Grabbable>().InjectOptionalTargetTransform(objArr[objectIdx].transform);
       // Maybe get a reference to the istk game object we are using / get access to that script or something and figure out how to put the cript in the component inspector spot -although we may not be using one here - lets get maybe the one we have on the interactsphere,
       // an just change its fields/rigidbodies/references here? Also figure out how we can not only pick up, but push and resize these objects! Also does
       // this script need to be under the handpose game object and scripts, or can it stand on its own since we have boolean trigger in update/how does audio do it without
       // it constantly playing (equivalent to when my motion just occured regardless of thumbs up when it was under the handpose and its script? *****************

    
    // Add the vector3 to the position array 
    objPos.Add(new Vector3 (playerPos.x, playerPos.y, playerPos.z));

// Set the newly created object's position 
    objArr[objectIdx].transform.position = objPos[objectIdx];
    
    // Increment the global value
    objectIdx++;
    }
}
