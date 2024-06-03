using UnityEngine;

[CreateAssetMenu(fileName = "Pieces", menuName = "ChessPieces/Pieces", order = 1)]
public sealed class Piece : ScriptableObject
{
    public PieceNames PieceName;
    // Move pattern
    public Sprite PieceIcon;
    public Players PlayerAssigned;
    public PieceMovement PieceMovement;
}
