using UnityEngine;

public sealed class ReplayData
{
    private PieceNames piece;
    private Vector2Int currentLocation;
    private Vector2Int moveToLocation;
    private Players player;

    public PieceNames Piece => piece;
    public Vector2Int CurrentLocation => currentLocation;
    public Vector2Int MoveToLocation => moveToLocation;
    public Players Player => player;

    public ReplayData(PieceNames pieceName, Players playerAssigned, Vector2Int currentPos, Vector2Int moveToPos)
    {
        piece = pieceName;
        player = playerAssigned;
        currentLocation = currentPos;
        moveToLocation = moveToPos;
    }
}
