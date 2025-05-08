using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Objfollow : MonoBehaviour
{

// Gain reference to our main character controller 
    public CharacterController cc;
    // The game object we want to follow our main object 
    public GameObject objThatFollow;

    // private Vector3 newPosForObj; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // newPosForObj = cc.transform.position;
        objThatFollow.transform.position = cc.transform.position;
    }
}
