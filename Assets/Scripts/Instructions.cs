using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A script that takes instruction text from off screen, and moves it to where the user is when they pass a certain point 
public class Instructions : MonoBehaviour
{
// The game object we want to trigger our instructions when our position matches up 
    public GameObject objToTrigger;

    // Vector3 to hold the position of the text 
    private Vector3 textPos;
    
    // Reference to the camera rig 
    public GameObject OVRRig;

// Get reference to the text we are trying to move 
    public GameObject textPrefab;

    private Vector3 currPlayerPos;
    private Vector3 posToKeepText; 

    private int preventPosUpdate = 0; 

    // public Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Player Pos updates each time, but each new update will not always be used - used to just make sure the vector3 gets initialized 
        currPlayerPos = OVRRig.transform.position;
        // If statement will run once, set the position of the text, change the int lock, and will never run again 
        if (currPlayerPos.z >= objToTrigger.transform.position.z && currPlayerPos.z <= objToTrigger.transform.position.z + 5f && preventPosUpdate == 0){
            textPos.z = OVRRig.transform.position.z + 6f; // Make the text appear a bit in front of the user
            textPos.x = OVRRig.transform.position.x + 6f; // Move the text a bit to the right too 
            textPrefab.transform.position = textPos;
            posToKeepText = textPrefab.transform.position; // Lock the position of where the text is to remain - locked in as the current position when this if statement is triggered
            preventPosUpdate = 1; // Boolean lock enables - prevents pos position from updating - all future cases will hit the below, and will use posToKeepText from now on 
            
            // Once boolean lock is enabled, do not use the player position for the text anymore - keep the text at the fixed position 
        // } else if(currPlayerPos.z >= objToTrigger.transform.position.z && currPlayerPos.z <= objToTrigger.transform.position.z + 5f && preventPosUpdate == 1){
        //     textPrefab.transform.position = posToKeepText; // Position has been set - from this point on, keep the text appearing at the same place 
        // }
    }
}
}