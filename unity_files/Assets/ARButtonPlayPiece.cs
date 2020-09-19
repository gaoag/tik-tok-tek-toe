using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARButtonPlayPiece : MonoBehaviour, OnTouch3D
{
    public Vector2Int boardPosition;
    public GameObject xPiece;
    public GameObject oPiece;

    private Collider buttonCollider; 
    private float debounceTime = 1.0f;
    private float remainingTime;

    private int timesPressed;
    public Text messageText;
    
    void Start() {
        buttonCollider = GetComponent<Collider>();
        
        remainingTime = 0;
    }

    void Update() {
        if (timesPressed < 3) {
            if (remainingTime > 0) {
                remainingTime -= Time.deltaTime;
            } else {
                buttonCollider.enabled = true;
            }
        } else {
            // make the button DISAPPEAR
            this.gameObject.SetActive(false);
        }
        
    }
    public void OnTouch(GameStateManager gameStateManager)
    {
        
        if (timesPressed < 3 && remainingTime <= 0) {
            //immediately turn off the collider
            buttonCollider.enabled = false;
            timesPressed += 1;

            GameObject pieceToPlay;

            if (gameStateManager.WhoseTurn() == "X") {
                pieceToPlay = xPiece;
            } else {
                pieceToPlay = oPiece;
            }
          
            GameObject pieceObjectPlayed = Instantiate(pieceToPlay);
            pieceObjectPlayed.transform.parent = GameObject.Find("ParentObject").transform;
            pieceObjectPlayed.transform.localScale   = new Vector3(0.165f, 0.18f, 0.165f);
            pieceObjectPlayed.transform.position = this.gameObject.transform.position;
            pieceObjectPlayed.transform.localRotation = Quaternion.identity;

            //pass the coordinates of this new piece to the gamemanager; the gamemanager will decide what to do next
            gameStateManager.UpdateGameState(boardPosition, pieceObjectPlayed, this);
            messageText.text = "It is " + gameStateManager.WhoseTurn() + "'s turn.";
            //set the remaining time untilt his is activatable again.
            remainingTime = debounceTime;

        }
        
        
    }

    public void UndoTimesPressed() {
        timesPressed -= 1;
    }

    public void Reactivate() {
        this.gameObject.SetActive(true);
    }
}