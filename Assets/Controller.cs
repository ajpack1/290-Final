using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.PoseDetection;
using UnityEngine;

// This is a script to move the character forwards when a pose is given 

public class Controller : MonoBehaviour
{
    // Public access to the game object OVR setup - we will use this to move the player based on a gesture
    public GameObject headset;
    // Gain acccess to the hand pose object we want to trigger our action 
    public GameObject handPose;

    // A private variable to hold the actual script - it is of the type of the script
    private ShapeRecognizerActiveState script;



    // Vector3 to move our position 
    private Vector3 movePos;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //This gives us access to the script who detects if the hand position is detected or not - we get the componenet from the handpose - remember we put these scripts on
        // objects to treat them like easy to use building blocks
        script = handPose.GetComponent<ShapeRecognizerActiveState>();
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
    // where one script can access another 
    public void doSomething()
    {
        movePos = headset.transform.position;
        movePos.z = movePos.z + speed;
        headset.transform.position = movePos;
    }
}


// In addition to this, we made this work by making our custom object with this script on it - you pass in the hand pose object into its inspector fields - from there, 
// you go back to the hand pose object, and add the object into what is triggered on runtime - then you can choose the public method we wrote, and it will run when the pose
// is detected