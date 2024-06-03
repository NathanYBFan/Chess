using UnityEngine;

[CreateAssetMenu(fileName = "Pieces", menuName = "ChessPieces/Pieces", order = 1)]
public sealed class Piece : ScriptableObject
{
    public PieceNames PieceName;
    // Move pattern
    public Sprite PieceIcon;
    public Players PlayerAssigned;
    public PieceMovement PieceMovement;

    public void Death(Vector2Int currentLocation)
    {
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PieceName == PieceNames.King)
            GameManager._Instance.EndGame();

        Destroy(GameManager._Instance.BoardScript.GetObjectOnTile(currentLocation));
    }
}
