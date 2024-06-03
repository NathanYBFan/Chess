using UnityEngine;

public abstract class PieceMovement : MonoBehaviour
{
    [SerializeField]
    private PieceNames pieceName;

    [SerializeField]
    private Vector2Int currentLocation;

    public PieceNames PieceName => pieceName;
    public Vector2Int CurrentLocation { get { return currentLocation; } set { currentLocation = value; } }

    public abstract void HighlightAvailableLocations(Vector2Int currentLocation, bool facingUp);

    public abstract void MoveAddons();
    public void MovePiece(Vector2Int moveToLocation)
    {
        // Uncheck has piece bool from current tile
        GameManager._Instance.BoardScript.GetTileFromPosition(currentLocation).HasPiece = false;

        // Assign self to new tile
        GameManager._Instance.BoardScript.MovePiece(GameManager._Instance.SelectedPiece, moveToLocation);
        MoveAddons();
    }
    
    public void Death()
    {
        Destroy(GameManager._Instance.BoardScript.GetObjectOnTile(currentLocation));
    }

    public bool IsValidLocation(Vector2Int position)
    {
        return !(position.x < 0 || position.x > (GameManager._Instance.BoardScript.GetNumberOfColumns() - 1)
            || position.y < 0 || position.y > (GameManager._Instance.BoardScript.GetNumberOfRows() - 1));
    }
}
