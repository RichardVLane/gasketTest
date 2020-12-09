using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacePrefabOnPlane : MonoBehaviour
{

    //[RequireComponent(typeof(ARRaycastManager))]
    // Start is called before the first frame update
    [SerializeField]
    [Tooltip("Instantiates a prefab on the plane at the touch location")]
    
    public GameObject myplacedPrefab;
    ARRaycastManager m_RaycastManager;
    private ARRaycastManager rayCastmgr;
    private List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    //private GameObject spawnedObject;

    // The prefab that will be instantiated on touching the screen
    public GameObject myPlacedPrefab
    {
        get { return myplacedPrefab; }
        set { myplacedPrefab = value; }
    }

    // The object instantiated as a result of a successful raycast intersection with the plane

    //TODO: This intersection point will be the second point to calculate the distance of the raycast - other being its origin

    public GameObject spawnedObject
    {
        get;
        private set;
    }

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        //rayCastmgr = FindObjectOfType<ARRaycastManager>();

    }

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(myPlacedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.position = hitPose.position;
            }
        }        
    }
}
