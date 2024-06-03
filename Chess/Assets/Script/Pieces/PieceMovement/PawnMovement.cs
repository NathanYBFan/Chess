using System.Collections.Generic;
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
            if (IsValidLocation(position) && GameManager._Instance.BoardScript.GetPieceOnTile(position) == null)
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
        // Change when moved to end of board

        // Check if newPos is valid
        foreach (Vector2Int position in availablePositions)
        {
            GameManager._Instance.BoardScript.GetTileFromPosition(position).HighlightTile();
        }
    }

    public override void MoveAddons() { }

    private bool PawnCanMove(Vector2Int position, Vector2Int currentLocation)
    {
        // If both are from the same player (eg. white & white)
        return (GameManager._Instance.BoardScript.GetPieceOnTile(position) != null && GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation) != null && GameManager._Instance.BoardScript.GetPieceOnTile(position).PlayerAssigned != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned);
    }
}
