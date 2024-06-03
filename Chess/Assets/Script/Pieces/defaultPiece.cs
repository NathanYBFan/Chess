using UnityEngine;
using UnityEngine.UI;

public class defaultPiece : MonoBehaviour
{
    [SerializeField]
    private Piece pieceAssigned;

    [SerializeField]
    private Image pieceIcon;

    public Piece PieceAssigned
    {
        get { return pieceAssigned; }
        set
        {
            pieceAssigned = value;
            pieceIcon.sprite = pieceAssigned.PieceIcon;
        }
    }
}
