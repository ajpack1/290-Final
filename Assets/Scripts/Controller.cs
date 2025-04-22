using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.PoseDetection;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
    // reference to the ground
    public GameObject ground;
    // original start position
    private Vector3 startPos;
    
    // HAND TRACKING REFERENCES / VARIABLES 
    // movement speed 
    public float speed;
    // A private variable to hold the actual script - it is of the type of the script
    private ShapeRecognizerActiveState script;
    // Gain acccess to the hand pose object we want to trigger our action 
    public GameObject handPose;

    // gravity and jump settings 
    public float gravity = -9f;
    public float jumpForce = 5f;
    // Tracks vertical velocity for gravity & jumping
    private float verticalVelocity = 0f;


    // Start is called before the first frame update
    void Start()
    {
        // This gives us access to the script who detects if the hand position is detected or not - we get the componenet from the handpose - remember we put these scripts on
        // objects to treat them like easy to use building blocks
        script = handPose.GetComponent<ShapeRecognizerActiveState>();

        // accessing the character controller rigid body so we can modify its position, along with the player
        characterController = OVRRig.GetComponent<CharacterController>();

        // saving intial start position
        startPos = characterController.transform.position;
    }

    // Update is called once per frame 
    void Update()
    {
        // Now that we have access to the object, we can check its "Active" method which checks if the hand position is detected - if it is, it moves the player
        if (script.Active)
        {
            doSomething();
        }
        
        
        if (characterController.transform.position.y < ground.transform.position.y - 0.5f) {
                characterController.transform.position = startPos;
            }
    }

   
    // we are  going to access the CenterEyeAnchor vector3 position
    // to determine where the headset is facing. this way, we can track active input on the player's direction, and 
    // customize movement so that the player moves in the direction they are facing - zoya
    public void doSomething()
    {
        
        // instead of accessing the headset to transform position , we are accessing the forward
        // view of the headset, to determine the direction we should be moving in based on the 
        // direction the player is facing
        Vector3 moveDirection = centerEyeAnchor.forward;
        
        // zero out vertical movement so we keep movement on XZ axis 
        moveDirection.y = 0;
        moveDirection.Normalize();
        Vector3 horizontalMovement = moveDirection * speed;

        // gravity and jump logic
        if (characterController.isGrounded) {
            // when grounded, reset vertical
            verticalVelocity = 0f;

            // when implementing jump trigger place here
        } else {
            // apply gravity 
            verticalVelocity += gravity * Time.deltaTime;

            // check if below the ground
            
        }

        Vector3 finalMovement = horizontalMovement;
        finalMovement.y = verticalVelocity;

        characterController.Move(finalMovement * Time.deltaTime);

       
    }
}


// In addition to this, we made this work by making our custom object with this script on it - you pass in the hand pose object into its inspector fields - from there, 
// you go back to the hand pose object, and add the object into what is triggered on runtime - then you can choose the public method we wrote, and it will run when the pose
// is detected