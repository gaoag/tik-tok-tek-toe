using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARButtonManager : MonoBehaviour
{
    private Camera arCamera;
    private PlaceGameBoard placeGameBoard;

    private GameStateManager gameStateManager;

    private float debounceTime = 1.0f;
    private float remainingTime;

    void Start()
    {
        // Here we will grab the camera from the AR Session Origin.
        // This camera acts like any other camera in Unity.
        arCamera = GetComponent<ARSessionOrigin>().camera;
        // We will also need the PlaceGameBoard script to know if
        // the game board exists or not.
        placeGameBoard = GetComponent<PlaceGameBoard>();

        // We will also need the GameStateManager to know what the state of the game is (who is playing, whether anybody has won)
        gameStateManager = GetComponent<GameStateManager>();
    }

    void Update()
    {
        if (remainingTime <= 0 && placeGameBoard.Placed() && Input.touchCount > 0 && !gameStateManager.GameOver())
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            // Convert the 2d screen point into a ray.
            Ray ray = arCamera.ScreenPointToRay(touchPosition);
            // Check if this hits an object within 100m of the user.
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,100))
            {

                // Check that the object is interactable.
                if(hit.transform.tag=="Interactable")
                    // Call the OnTouch function.
                    // Note the use of OnTouch3D here lets us
                    // call any class inheriting from OnTouch3D.

                    hit.transform.GetComponent<OnTouch3D>().OnTouch(gameStateManager);
                remainingTime = debounceTime;
            }
            
        } else {
            remainingTime -= Time.deltaTime;
        }
    }
}