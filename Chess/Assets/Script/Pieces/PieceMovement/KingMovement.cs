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
    }

    private void CanCastle(Vector2Int currentLocation, bool facingUp)
    {
        if (facingUp) // white
        {
            if (currentLocation != new Vector2Int(3, 7)) return; // If not in starting location
            CheckLeft(currentLocation, PieceNames.RookA);
            CheckRight(currentLocation, PieceNames.RookB);
        }
        else // black
        {
            if (currentLocation != new Vector2Int(4, 0)) return; // If not in starting location
            Debug.Log("In correct pos");
            CheckLeftTop(currentLocation, PieceNames.RookA);
            CheckRightTop(currentLocation, PieceNames.RookB);
        }
    }

    private void CheckLeft(Vector2Int currentLocation, PieceNames pieceToCheck)
    {
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-1, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-2, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-3, 0)) == null) return;

        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-3, 0)).PlayerAssigned != GameManager._Instance.PlayerTurn) return;

        if (!PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(pieceToCheck, GameManager._Instance.PlayerTurn) && !PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(PieceNames.King, GameManager._Instance.PlayerTurn))
        {
            // Is rook on right team
            if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-3, 0)).PieceName == pieceToCheck)
            {
                var temp = currentLocation;
                temp += new Vector2Int(-2, 0);
                GameManager._Instance.BoardScript.GetTileFromPosition(temp).HighlightTile();
            }
        }
    }

    private void CheckRight(Vector2Int currentLocation, PieceNames pieceToCheck)
    {
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(1, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(2, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(3, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(4, 0)) == null) return;

        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(4, 0)).PlayerAssigned != GameManager._Instance.PlayerTurn) return;

        if (!PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(pieceToCheck, GameManager._Instance.PlayerTurn) && !PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(PieceNames.King, GameManager._Instance.PlayerTurn))
        {
            // Is rook on right team
            if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(4, 0)).PieceName == pieceToCheck)
            {
                var temp = currentLocation;
                temp += new Vector2Int(3, 0);
                GameManager._Instance.BoardScript.GetTileFromPosition(temp).HighlightTile();
            }
        }
    }

    private void CheckLeftTop(Vector2Int currentLocation, PieceNames pieceToCheck)
    {
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-1, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-2, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-3, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-4, 0)) == null) return;

        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-4, 0)).PlayerAssigned != GameManager._Instance.PlayerTurn) return;

        if (!PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(pieceToCheck, GameManager._Instance.PlayerTurn) && !PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(PieceNames.King, GameManager._Instance.PlayerTurn))
        {
            // Is rook on right team
            if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(-4, 0)).PieceName == pieceToCheck)
            {
                var temp = currentLocation;
                temp += new Vector2Int(-3, 0);
                GameManager._Instance.BoardScript.GetTileFromPosition(temp).HighlightTile();
            }
        }
    }

    private void CheckRightTop(Vector2Int currentLocation, PieceNames pieceToCheck)
    {
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(1, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(2, 0)) != null) return;
        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(3, 0)) == null) return;

        if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(3, 0)).PlayerAssigned != GameManager._Instance.PlayerTurn) return;

        if (!PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(pieceToCheck, GameManager._Instance.PlayerTurn) && !PreviousMoveManager._Instance.Recorder.ContainsMoveFromPiece(PieceNames.King, GameManager._Instance.PlayerTurn))
        {
            // Is rook on right team
            if (GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation + new Vector2Int(3, 0)).PieceName == pieceToCheck)
            {
                var temp = currentLocation;
                temp += new Vector2Int(2, 0);
                GameManager._Instance.BoardScript.GetTileFromPosition(temp).HighlightTile();
            }
        }
    }

    public override void MoveAddons(Vector2Int moveToLocation)
    {
        if (Mathf.Abs(moveToLocation.x - CurrentLocation.x) <= 1) return;
        // Castle was done
        if (moveToLocation == new Vector2Int(1, 7))
            GameManager._Instance.BoardScript.MovePieceWithoutDeselect(GameManager._Instance.BoardScript.GetObjectOnTile(new Vector2Int(0, 7)), new Vector2Int(2, 7));
        else if (moveToLocation == new Vector2Int(6, 7))
            GameManager._Instance.BoardScript.MovePieceWithoutDeselect(GameManager._Instance.BoardScript.GetObjectOnTile(new Vector2Int(7, 7)), new Vector2Int(5, 7));
        else if (moveToLocation == new Vector2Int(1, 0))
            GameManager._Instance.BoardScript.MovePieceWithoutDeselect(GameManager._Instance.BoardScript.GetObjectOnTile(new Vector2Int(0, 0)), new Vector2Int(2, 0));
        else if (moveToLocation == new Vector2Int(6, 0))
            GameManager._Instance.BoardScript.MovePieceWithoutDeselect(GameManager._Instance.BoardScript.GetObjectOnTile(new Vector2Int(7, 0)), new Vector2Int(5, 0));
    }
    public override void PostMoveAddons() { GameManager._Instance.NextTurn(); }
}
