using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.Gestures;
using UnityEngine.XR.Hands.Samples.Gestures.DebugTools;
// Code/Info from GPT Utilized to help breakdown the process of what is occuring - comments provided below to explain what is happening/clarify 
public class HandGestureDetector : MonoBehaviour
{
// The shape we want to be detected
    public XRHandShape shape;

    // The threshold it will take for the hand gesture to be detected / the level of accuracy of the gesture 
    public float threshold = 1f;

// Allows us to check which hand we are working with / boolean to determine if using left or right hand
    public bool lHand = false; 

    private XRHandSubsystem handSubsystem;
    // Start is called before the first frame update
    void Start()
    {
        // Var is a high level variable type - represents variables that can be passed around to different types 
        // subSystem is the variable name
        // The subsystem is the system that open XR is using to track - we are storing a reference to this system here
        var subSystem = new System.Collections.Generic.List<XRHandSubsystem>();
        
        // Taking the subsystem open xr is using, such that open xr will use this specific form of gesture tracking
        SubsystemManager.GetInstances(subSystem);

// var subSystem holds a reference to the list of xr systems that can be used - if populated/hand systems show up, we set the handsystem we will be using equal to 
// subSystem[0], which is the one we want - this will be the first, main subsystem/default one that will be initialized, so we can instantly grab it and start using it 
        if (subSystem.Count > 0) {
            handSubsystem = subSystem[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        // First check if the subsystem is ready for detection - if it is, then check if the hand shape has been detected
        if (handSubsystem == null || shape == null){
            return;
        }
        
        // Detecting which hand is being used at any point in time by the script / which hand is accessing the script / we are setting the hand being used to which hand is active 
        // Accessing subset hand based on true/false value (the object/hand we attatch the script to )
        // Ternary operator used for simplicity 
        XRHand hand = lHand ? handSubsystem.leftHand : handSubsystem.rightHand;

        // Now that we have the current hand in use, we want to check if this hand is active/moving, or not 
        // If the hand is not active/being used, no work to do and return 
        if (!hand.isTracked){
            return;
        }

        // Now that we know the hand is active, we want to check if it meets the requirements for triggering the action / we can check the threshold we set earlier 
        // float thresholdCheck = shape.GetSimilarity(hand);

    }
}
