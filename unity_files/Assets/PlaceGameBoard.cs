using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// These allow us to use the ARFoundation API.
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class PlaceGameBoard : MonoBehaviour
{
    // Public variables can be set from the unity UI.
    // We will set this to our Game Board object.
    
    
    public GameObject gameBoard;

    public ParentObject parentObject;
    
    public Text messageText;

    // These will store references to our other components.
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    // This will indicate whether the game board is set.
    private bool placed = false;
    
    private float waitTime = -1f;
    


    // Start is called before the first frame update.
    void Start()
    {
        // GetComponent allows us to reference other parts of this game object.
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        gameBoard.name = "GameBoard";
    }

    // Update is called once per frame.
    void Update()
    {
        
        if (!placed && waitTime < 0)
        {
            
            if (Input.touchCount > 0)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;
            
                // Raycast will return a list of all planes intersected by the
                // ray as well as the intersection point.
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(
                    touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    
                    // The list is sorted by distance so to get the location
                    // of the closest intersection we simply reference hits[0].
                    var hitPose = hits[0].pose;
                    // Now we will activate our game board and place it at the
                    // chosen location.
                    parentObject.transform.position = hitPose.position;
                    gameBoard.SetActive(true);
                    gameBoard.transform.localPosition = new Vector3(0f, 0f, 0f);
                    placed = true;
                    
                    planeManager.detectionMode = PlaneDetectionMode.None;

                }
            }
        }
        else
        {
            // The plane manager will set newly detected planes to active by 
            // default so we will continue to disable these.
            //planeManager.SetTrackablesActive(false); //For older versions of AR foundation
            planeManager.detectionMode = PlaneDetectionMode.None;
            waitTime -= Time.deltaTime;
        }
    }

    // If the user places the game board at an undesirable location we 
    // would like to allow the user to move the game board to a new location.
    public void AllowMoveGameBoard()
    {
        waitTime = 0.3f;
        placed = false;
        //planeManager.SetTrackablesActive(true);
        planeManager.detectionMode = PlaneDetectionMode.Horizontal;
        
    }

    // Lastly we will later need to allow other components to check whether the
    // game board has been places so we will add an accessor to this.
    public bool Placed()
    {
        return placed;
    }
}