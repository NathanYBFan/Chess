using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : PieceMovement
{
    public override void HighlightAvailableLocations(Vector2Int currentLocation, bool facingUp)
    {
        List<Vector2Int> availablePositions = new List<Vector2Int>();

        // UP
        for (var i = 0; i < 2; i++)
        {
            var temp = currentLocation;
            temp += i % 2 == 0 ? new Vector2Int(1, -2) : new Vector2Int(-1, -2);

            if (!IsValidLocation(temp)) continue;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) continue;

            // Add position
            availablePositions.Add(temp);
        }

        // Right
        for (var i = 0; i < 2; i++)
        {
            var temp = currentLocation;
            temp += i % 2 == 0 ? new Vector2Int(2, 1) : new Vector2Int(2, -1);

            if (!IsValidLocation(temp)) continue;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) continue;

            // Add position
            availablePositions.Add(temp);
        }

        // Down
        for (var i = 0; i < 2; i++)
        {
            var temp = currentLocation;
            temp += i % 2 == 0 ? new Vector2Int(1, 2) : new Vector2Int(-1, 2);

            if (!IsValidLocation(temp)) continue;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) continue;

            // Add position
            availablePositions.Add(temp);
        }

        // Left
        for (var i = 0; i < 2; i++)
        {
            var temp = currentLocation;
            temp += i % 2 == 0 ? new Vector2Int(-2, 1) : new Vector2Int(-2, -1);

            if (!IsValidLocation(temp)) continue;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) continue;

            // Add position
            availablePositions.Add(temp);
        }

        // Check if newPos is valid
        foreach (var position in availablePositions)
            GameManager._Instance.BoardScript.GetTileFromPosition(position).HighlightTile();
    }

    public override void MoveAddons() { }
}
