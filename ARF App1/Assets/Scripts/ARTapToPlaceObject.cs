using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;

    private Camera myCamera;
    //private ARSessionOrigin arOrigin;
    private ARRaycastManager rayCastmgr;

    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private bool objectPlaced = false;


    // Start is called before the first frame update
    void Start()
    {
        //arOrigin = FindObjectOfType<ARSessionOrigin>();
        // Use raycast manager to place the reticle from the raycast manager on the AR Session Origin 

        // TODO: Obtain information (the coordinates in world space) for the reticle and its intersection with the plane
        // so that this becomes a value to calculate the distance to the object/gasket/shape.


        rayCastmgr = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objectPlaced == false)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
                objectPlaced = true;
                Destroy(placementIndicator);
            }
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            Debug.Log("pose is valid");
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
            Debug.Log("Pose is not valid");
        }
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementPose()
    {
        var screenCentre = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        
        rayCastmgr.Raycast(screenCentre, hits, TrackableType.Planes);
        calcDistanceToPoint();

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
      
    }

    private void calcDistanceToPoint()
    {
        //TODO: 
    }
}
