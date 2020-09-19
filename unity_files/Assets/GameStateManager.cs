using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update

    private bool gameOver = false;  
    private string whoseTurn = "O";

    private string[,,] moveGrid;

    private GameObject lastPieceObjectPlayed;
    private ARButtonPlayPiece lastButtonPressed;

    private Vector3Int lastPieceCoordPlayed;

    public Text messageText;

    void Start()
    {
        moveGrid = new string[3, 3, 3] {   
            { {"", "", ""}, {"", "", ""}, {"", "", ""} },
            { {"", "", ""}, {"", "", ""}, {"", "", ""} },
            { {"", "", ""}, {"", "", ""}, {"", "", ""} },
        };
    }

    void Update()
    {
        if (gameOver) {
            messageText.text = whoseTurn + " wins!";
        }
    }

    public void UpdateGameState(Vector2Int boardPosition, GameObject lastPieceObjectPlayed, ARButtonPlayPiece lastButtonPressed)
    {
        this.lastPieceObjectPlayed = lastPieceObjectPlayed;
        this.lastButtonPressed = lastButtonPressed;
        
        //place the piece into the right slot based on the given boardPosition
        //logiclogiclogic

        for (int i=0; i < 3; i++) {
            string temp = moveGrid[boardPosition.x, boardPosition.y, i];
            if (temp == "") {
                moveGrid[boardPosition.x, boardPosition.y, i] = whoseTurn;
                lastPieceCoordPlayed = new Vector3Int(boardPosition.x, boardPosition.y, i);
                break;
            }
        }


        
        // check if there's a win or not - there are 49 lines to check...
        // set 1 - a fixed x and a fixed y, but different z
        // set 2 - fixed y and fixed z, but different x
        // set 3 - fixed x and fixed z, but different y
        string piece1;
        string piece2;
        string piece3;
        for (int fixed_1 = 0; fixed_1 < 3; fixed_1++) {
            for (int fixed_2 = 0; fixed_2 < 3; fixed_2++) {

                piece1 = moveGrid[fixed_1, fixed_2, 0];
                piece2 = moveGrid[fixed_1, fixed_2, 1];
                piece3 = moveGrid[fixed_1, fixed_2, 2];
                if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
                    gameOver = true;
                }

                piece1 = moveGrid[fixed_1, 0, fixed_2];
                piece2 = moveGrid[fixed_1, 1, fixed_2];
                piece3 = moveGrid[fixed_1, 2, fixed_2];
                if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
                    gameOver = true;
                }

                piece1 = moveGrid[0, fixed_1, fixed_2];
                piece2 = moveGrid[1, fixed_1, fixed_2];
                piece3 = moveGrid[2, fixed_1, fixed_2];
                if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
                    gameOver = true;
                }                
            }
        }

        // set 4 - fixed x, z and y form diagonals
        // set 5 - fixed y, x and z form diagonals
        // set 6 - fixed z, y and x form diagonals
        for (int fixed_1 = 0; fixed_1 < 3; fixed_1++) {
            piece1 = moveGrid[fixed_1, 0, 0];
            piece2 = moveGrid[fixed_1, 1, 1];
            piece3 = moveGrid[fixed_1, 2, 2];
            if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
                gameOver = true;
            }

            piece1 = moveGrid[fixed_1, 0, 2];
            piece2 = moveGrid[fixed_1, 1, 1];
            piece3 = moveGrid[fixed_1, 2, 0];
            if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
                gameOver = true;
            }

            piece1 = moveGrid[0, fixed_1, 0];
            piece2 = moveGrid[1, fixed_1, 1];
            piece3 = moveGrid[2, fixed_1, 2];
            if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
                gameOver = true;
            }

            piece1 = moveGrid[0, fixed_1, 2];
            piece2 = moveGrid[1, fixed_1, 1];
            piece3 = moveGrid[2, fixed_1, 0];
            if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
                gameOver = true;
            }

            piece1 = moveGrid[0, 0, fixed_1];
            piece2 = moveGrid[1, 1, fixed_1];
            piece3 = moveGrid[2, 2, fixed_1];
            if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
                gameOver = true;
            }

            piece1 = moveGrid[0, 2, fixed_1];
            piece2 = moveGrid[1, 1, fixed_1];
            piece3 = moveGrid[2, 0, fixed_1];
            if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
                gameOver = true;
            }
        }

        // set 7 - manually check 4 options
        piece1 = moveGrid[0, 0, 0];
        piece2 = moveGrid[1, 1, 1];
        piece3 = moveGrid[2, 2, 2];
        if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
            gameOver = true;
        }

        piece1 = moveGrid[2, 0, 0];
        piece2 = moveGrid[1, 1, 1];
        piece3 = moveGrid[0, 2, 2];
        if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
            gameOver = true;
        }

        piece1 = moveGrid[2, 0, 2];
        piece2 = moveGrid[1, 1, 1];
        piece3 = moveGrid[0, 2, 0];
        if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
            gameOver = true;
        }

        piece1 = moveGrid[0, 0, 2];
        piece2 = moveGrid[1, 1, 1];
        piece3 = moveGrid[2, 2, 0];
        if (piece1 != "" && piece1.Equals(piece2) && piece2.Equals(piece3)) {
            gameOver = true;
        }

        // called whenever someone makes a move;
        // flip whoseTurn around
        if (!gameOver) {
            if (whoseTurn == "O") {
                whoseTurn = "X";
            } else {
                whoseTurn = "O";
            }
        }
        
    }

    public string WhoseTurn()
    {
        return whoseTurn;
    }

    public bool GameOver() {
        return gameOver;
    }

    public void UndoPrevious() {
        if (!gameOver) {
            Destroy(lastPieceObjectPlayed);
            if (whoseTurn == "O") {
                whoseTurn = "X";
            } else {
                whoseTurn = "O";
            }
        }
        //undo the move in the moveGrid
        moveGrid[lastPieceCoordPlayed.x, lastPieceCoordPlayed.y, lastPieceCoordPlayed.z] = "";

        //get associated button and rollback its move
        this.lastButtonPressed.UndoTimesPressed();
        this.lastButtonPressed.Reactivate();

    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
