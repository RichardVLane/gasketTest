using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class InstantiateOnTouch : MonoBehaviour
{
    public Camera firstPersonCamera;
    public GameObject placeObject;

   
    // Update is called once per frame
    void Update()
    {
        // Get the touch position from the screen to see if we have at least one touch event currently active
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // Now that we know that we have an active touch point, do a raycast to see if it hits
        // a plane where we can instantiate the object on.
        


    }
}
