using UnityEngine;

public class KingMovement : PieceMovement
{
    public override void HighlightAvailableLocations(Vector2Int currentLocation, bool facingUp)
    {
        // If castling
        CanCastle(currentLocation, facingUp);

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                var temp = currentLocation;
                temp += new Vector2Int(-1 + x, -1 + y);

                if (!IsValidLocation(temp)) continue;
                if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) continue;
                GameManager._Instance.BoardScript.GetTileFromPosition(temp).HighlightTile();
            }
        }

        // Make sure the move wont put you in check
    }

    private void CanCastle(Vector2Int currentLocation, bool facingUp)
    {
        if (facingUp) // white
        {
            Debug.Log("Is facing up");
            if (currentLocation != new Vector2Int(3, 7)) return; // If not in starting location

            Debug.Log(currentLocation + new Vector2Int(-1, 0));
            Debug.Log(currentLocation + new Vector2Int(-2, 0));
            Debug.Log(currentLocation + new Vector2Int(-3, 0));

            if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-1, 0)) != null) return;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-2, 0)) != null) return;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-3, 0)) == null) return;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-3, 0)).PlayerAssigned != Players.PlayerA) return;

            if (!PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(PieceNames.RookA, GameManager._Instance.PlayerTurn) && !PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(PieceNames.King, GameManager._Instance.PlayerTurn))
            {
                // Is rook on right team
                if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-3, 0)).PieceName == PieceNames.RookA)
                {
                    var temp = currentLocation;
                    temp += new Vector2Int(-2, 0);
                    GameManager._Instance.BoardScript.GetTileFromPosition(temp).HighlightTile();
                }
            }
            else if (PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(PieceNames.RookB, GameManager._Instance.PlayerTurn))
            {

            }

            
        }
        else // black
        {
            Debug.Log("Is facing down");
        }
    }

    public override void MoveAddons(Vector2Int moveToLocation)
    {
        Debug.Log(Mathf.Abs(moveToLocation.x - CurrentLocation.x));
        if (Mathf.Abs(moveToLocation.x - CurrentLocation.x) <= 1) return;
        // Castle was done
        if (moveToLocation == new Vector2Int(1, 7))
            GameManager._Instance.BoardScript.MovePieceWithoutDeselect(GameManager._Instance.BoardScript.GetObjectOnTile(new Vector2Int(0, 7)), new Vector2Int(2, 7));
    }
    public override void PostMoveAddons() { GameManager._Instance.NextTurn(); }
}
