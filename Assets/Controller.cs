using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.PoseDetection;
using UnityEngine;

// This is a script to move the character forwards when a pose is given 
public class Controller : MonoBehaviour
{  
    // PLAYER REFERENCES
    // reference to the whole player object 
    public GameObject OVRRig;
    // reference to the character controller rigid body on the OVR Rig
    private CharacterController characterController;
      // reference to the CenterEyeAnchor, to access the headsets forward eye direction. 
    public Transform centerEyeAnchor;
    
    
    // HAND TRACKING REFERENCES / VARIABLES 
    // movement speed 
    public float speed;
    // A private variable to hold the actual script - it is of the type of the script
    private ShapeRecognizerActiveState script;
    // Gain acccess to the hand pose object we want to trigger our action 
    public GameObject handPose;
    
    // OLD IMPLEMENTATION VARIABLES BELOW:
    // Public access to the game object OVR setup - we will use this to move the player based on a gesture
        // public GameObject headset;
    // Vector3 to move our position 
        // private Vector3 movePos;


    // Start is called before the first frame update
    void Start()
    {
        // This gives us access to the script who detects if the hand position is detected or not - we get the componenet from the handpose - remember we put these scripts on
        // objects to treat them like easy to use building blocks
        script = handPose.GetComponent<ShapeRecognizerActiveState>();

        // accessing the character controller rigid body so we can modify its position, along with the player
        characterController = OVRRig.GetComponent<CharacterController>();
    }

    // Update is called once per frame 
    void Update()
    {
        // Now that we have access to the object, we can check its "Active" method which checks if the hand position is detected - if it is, it moves the player
        if (script.Active)
        {
            doSomething();
        }

    }

    // This is a public method we put onto a gameobject - doing it this way makes things simpler
    // It is like lego building blocks - we put this script onto an object, and then that object can be accessed
    // which is actually our script to do something - we are then able to tell unity to run this script when
    // it sees a hand gesture run - this also allows for the ease of script access,
    // where one script can access another - ari

    // that was the original implementation, now we are instead going to access the CenterEyeAnchor vector3 position
    // to determine where the headset is facing. this way, we can track active input on the player's direction, and 
    // customize movement so that the player moves in the direction they are facing - zoya
    public void doSomething()
    {
        // OLD IMPLEMENTATION 
        // movePos = headset.transform.position;
        // movePos.z = movePos.z + speed;
        // headset.transform.position = movePos;

        // instead of accessing the headset to transform position , we are accessing the forward
        // view of the headset, to determine the direction we should be moving in based on the 
        // direction the player is facing
        Vector3 moveDirection = centerEyeAnchor.forward;
        moveDirection.y = 0;

        // // we are removing any vertical components for now, so our movement remains horizontal, and height 
        // // is only affected by the rigid body interaction. 
        // // because of this, we also have to normalize the vectors, so they remain consistent on the plane
        // // despite us changing the y position. 
        // moveDirection.y = 0;
        // moveDirection.Normalize();

        // now we move the characterController's rigid body, rather than the headset itself, so the rigid body
        // may interact with other objects in the scene during its movement. Since we are pulling a reference
        // we have to do a null check first. 
        if (characterController != null) {
            characterController.Move(moveDirection * speed * Time.deltaTime);
        } else {
            // directly modifying the position if the character controller isn't found. Also a good implementation
            // just on its own, but ignores collisions and therefore inadequate for this project. 
            OVRRig.transform.position += moveDirection * speed * Time.deltaTime;
        }
    }
}


// In addition to this, we made this work by making our custom object with this script on it - you pass in the hand pose object into its inspector fields - from there, 
// you go back to the hand pose object, and add the object into what is triggered on runtime - then you can choose the public method we wrote, and it will run when the pose
// is detected