using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceChangeUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> listOfButtons = new List<GameObject>();

    private Vector2Int position;
    private Players player;

    private void OnEnable()
    {
        GameManager._Instance.CanSelect = false;
    }

    private void OnDisable()
    {
        GameManager._Instance.CanSelect = true;
    }

    public void InitializeUI(Vector2Int piecePosition, Players piecePlayer)
    {
        position = piecePosition;
        player = piecePlayer;

        foreach(var button in listOfButtons)
        {
            Image imageToChange = button.transform.GetChild(0).GetComponent<Image>();

            if (piecePlayer == Players.PlayerB)
            {
                switch (listOfButtons.IndexOf(button))
                {
                    case 0: // Queen
                        imageToChange.sprite = GameManager._Instance.ListOfWhitePieces[5].PieceIcon;
                        break; 
                    case 1: // Bishop
                        imageToChange.sprite = GameManager._Instance.ListOfWhitePieces[3].PieceIcon;
                        break;
                    case 2: // Knight
                        imageToChange.sprite = GameManager._Instance.ListOfWhitePieces[2].PieceIcon;
                        break;
                    default: // Rook
                        imageToChange.sprite = GameManager._Instance.ListOfWhitePieces[7].PieceIcon;
                        break;
                }
            }
            else
            {
                switch (listOfButtons.IndexOf(button))
                {
                    case 0: // Queen
                        imageToChange.sprite = GameManager._Instance.ListOfBlackPieces[5].PieceIcon;
                        break;
                    case 1: // Bishop
                        imageToChange.sprite = GameManager._Instance.ListOfBlackPieces[3].PieceIcon;
                        break;
                    case 2: // Knight
                        imageToChange.sprite = GameManager._Instance.ListOfBlackPieces[2].PieceIcon;
                        break;
                    default: // Rook
                        imageToChange.sprite = GameManager._Instance.ListOfBlackPieces[7].PieceIcon;
                        break;
                }
            }
        }
    }

    public void ButtonPressed(int buttonNumber)
    {
        if (player == Players.PlayerB)
        {
            switch (buttonNumber)
            {
                case 0: // Queen
                    GameManager._Instance.BoardScript.GetObjectOnTile(position).GetComponent<defaultPiece>().PieceAssigned = GameManager._Instance.ListOfWhitePieces[5];
                    break;
                case 1: // Bishop
                    GameManager._Instance.BoardScript.GetObjectOnTile(position).GetComponent<defaultPiece>().PieceAssigned = GameManager._Instance.ListOfWhitePieces[3];
                    break;
                case 2: // Knight
                    GameManager._Instance.BoardScript.GetObjectOnTile(position).GetComponent<defaultPiece>().PieceAssigned = GameManager._Instance.ListOfWhitePieces[2];
                    break;
                default: // Rook
                    GameManager._Instance.BoardScript.GetObjectOnTile(position).GetComponent<defaultPiece>().PieceAssigned = GameManager._Instance.ListOfWhitePieces[7];
                    break;
            }
        }
        else
        {
            switch (buttonNumber)
            {
                case 0: // Queen
                    GameManager._Instance.BoardScript.GetObjectOnTile(position).GetComponent<defaultPiece>().PieceAssigned = GameManager._Instance.ListOfBlackPieces[5];
                    break;
                case 1: // Bishop
                    GameManager._Instance.BoardScript.GetObjectOnTile(position).GetComponent<defaultPiece>().PieceAssigned = GameManager._Instance.ListOfBlackPieces[3];
                    break;
                case 2: // Knight
                    GameManager._Instance.BoardScript.GetObjectOnTile(position).GetComponent<defaultPiece>().PieceAssigned = GameManager._Instance.ListOfBlackPieces[2];
                    break;
                default: // Rook
                    GameManager._Instance.BoardScript.GetObjectOnTile(position).GetComponent<defaultPiece>().PieceAssigned = GameManager._Instance.ListOfBlackPieces[7];
                    break;
            }
        }
        GameManager._Instance.NextTurn();
        Destroy(this.gameObject);
    }
}
