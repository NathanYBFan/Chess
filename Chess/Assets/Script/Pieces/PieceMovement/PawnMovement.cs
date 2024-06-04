using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnMovement : PieceMovement
{
    public override void HighlightAvailableLocations(Vector2Int currentLocation, bool facingUp)
    {
        List<Vector2Int> availablePositions = new List<Vector2Int>();

        // Base move
        Vector2Int newPos = facingUp ? currentLocation + new Vector2Int(0, -1) : currentLocation + new Vector2Int(0, 1);
        if (IsValidLocation(newPos) && GameManager._Instance.BoardScript.GetPieceOnTile(newPos) == null)
            availablePositions.Add(newPos);

        // First Move
        if (currentLocation.y == 6 || currentLocation.y == 1)
        {
            Vector2Int position = facingUp ? currentLocation + new Vector2Int(0, -2) : currentLocation + new Vector2Int(0, 2);
            if (IsValidLocation(position) && GameManager._Instance.BoardScript.GetPieceOnTile(position) == null && GameManager._Instance.BoardScript.GetPieceOnTile(facingUp ? currentLocation + new Vector2Int(0, -1): currentLocation + new Vector2Int(0, 1)) == null)
                availablePositions.Add(position);
        }

        // Attack 
        Vector2Int[] attackPositions = new Vector2Int[]
        {
            facingUp ? currentLocation + new Vector2Int(1, -1) : currentLocation + new Vector2Int(1, 1),
            facingUp ? currentLocation + new Vector2Int(-1, -1) : currentLocation + new Vector2Int(-1, 1)
        };

        // Checking attack
        foreach (Vector2Int attackPosition in attackPositions)
        {
            if (!IsValidLocation(attackPosition)) continue;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(attackPosition) != null && GameManager._Instance.BoardScript.GetPieceOnTile(attackPosition).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) continue;
            else if (GameManager._Instance.BoardScript.GetPieceOnTile(attackPosition) == null) continue;
            availablePositions.Add(attackPosition);
        }

        // En passant
        var temp = currentLocation; 
        // If piece is a pawn && it was first move
        if (PreviousMoveManager._Instance.Recorder.recordingQueue.Count > 0
            && PreviousMoveManager._Instance.Recorder.recordingQueue.Last().Piece == PieceNames.Pawn
            && Mathf.Abs(PreviousMoveManager._Instance.Recorder.recordingQueue.Last().MoveToLocation.y - PreviousMoveManager._Instance.Recorder.recordingQueue.Last().CurrentLocation.y) == 2
            && (PreviousMoveManager._Instance.Recorder.recordingQueue.Last().MoveToLocation == temp - new Vector2Int(-1, 0)
            || PreviousMoveManager._Instance.Recorder.recordingQueue.Last().MoveToLocation == temp - new Vector2Int(1, 0)))
        {
            if (facingUp)
                availablePositions.Add(PreviousMoveManager._Instance.Recorder.recordingQueue.Last().MoveToLocation == temp - new Vector2Int(-1, 0) ? temp - new Vector2Int(-1, 1) : temp - new Vector2Int(1, 1));
            else
                availablePositions.Add(PreviousMoveManager._Instance.Recorder.recordingQueue.Last().MoveToLocation == temp - new Vector2Int(-1, 0) ? temp - new Vector2Int(-1, -1) : temp - new Vector2Int(1, -1));
        }

        // Check if newPos is valid
        foreach (Vector2Int position in availablePositions)
        {
            GameManager._Instance.BoardScript.GetTileFromPosition(position).HighlightTile();
        }
    }

    public override void MoveAddons(Vector2Int moveToLocation)
    {
        var temp = CurrentLocation;
        if (PreviousMoveManager._Instance.Recorder.recordingQueue.Count <= 0) return; // Has been a move before
        if (PreviousMoveManager._Instance.Recorder.recordingQueue.Last().Piece != PieceNames.Pawn) return; // last moved piece was a pawn
        if (Mathf.Abs(PreviousMoveManager._Instance.Recorder.recordingQueue.Last().MoveToLocation.y - PreviousMoveManager._Instance.Recorder.recordingQueue.Last().CurrentLocation.y) != 2) return; // Last move was a double move
        if ((PreviousMoveManager._Instance.Recorder.recordingQueue.Last().MoveToLocation != temp - new Vector2Int(-1, 0) && PreviousMoveManager._Instance.Recorder.recordingQueue.Last().MoveToLocation != temp - new Vector2Int(1, 0))) return;

        // Chose enpassant

        if (moveToLocation == temp - new Vector2Int(-1, 1) || moveToLocation == temp - new Vector2Int(-1, -1)) // Left
        {
            Debug.Log(temp - new Vector2Int(-1, 0));

            GameManager._Instance.BoardScript.GetPieceOnTile(temp - new Vector2Int(-1, 0)).Death(temp - new Vector2Int(-1, 0));
        }
        else if (moveToLocation == temp - new Vector2Int(1, 1) || moveToLocation == temp - new Vector2Int(1, -1)) // Right
        {
            Debug.Log(moveToLocation);
            Debug.Log(temp - new Vector2Int(1, 1));
            Debug.Log(temp - new Vector2Int(1, -1));
            GameManager._Instance.BoardScript.GetPieceOnTile(temp - new Vector2Int(1, 0)).Death(temp - new Vector2Int(1, 0));
        }
    }

    public override void PostMoveAddons()
    {
        // Change piece if at end of board
        if ((GameManager._Instance.BoardScript.GetPieceOnTile(CurrentLocation).PlayerAssigned == Players.PlayerA && CurrentLocation.y == 7)
            || (GameManager._Instance.BoardScript.GetPieceOnTile(CurrentLocation).PlayerAssigned == Players.PlayerB && CurrentLocation.y == 0))
            GameManager._Instance.PawnChange(CurrentLocation);
        else
            GameManager._Instance.NextTurn();
    }
}
