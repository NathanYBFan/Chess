using System.Collections.Generic;
using UnityEngine;

public class RookMovement : PieceMovement
{
    public override void HighlightAvailableLocations(Vector2Int currentLocation, bool facingUp)
    {
        List<Vector2Int> availablePositions = new List<Vector2Int>();

        // Check Left
        for (var x = 1; x < GameManager._Instance.BoardScript.GetNumberOfColumns(); x++)
        {
            // Position to check
            var temp = currentLocation;
            temp += new Vector2Int(-x, 0);

            if (!IsValidLocation(temp)) break;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            availablePositions.Add(temp);

            // Add position
            availablePositions.Add(temp);

            // If tile has piece and its unfriendly, its already added so break now
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }
        // Check Right
        for (int x = 1; x < GameManager._Instance.BoardScript.GetNumberOfColumns(); x++)
        {
            // Position to check
            var temp = currentLocation;
            temp += new Vector2Int(x, 0);

            if (!IsValidLocation(temp)) break;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            availablePositions.Add(temp);

            // Add position
            availablePositions.Add(temp);

            // If tile has piece and its unfriendly, its already added so break now
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }
        // Check Down
        for (int y = 1; y < GameManager._Instance.BoardScript.GetNumberOfRows(); y++)
        {
            // Position to check
            var temp = currentLocation;
            temp += new Vector2Int(0, y);

            if (!IsValidLocation(temp)) break;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            availablePositions.Add(temp);

            // Add position
            availablePositions.Add(temp);

            // If tile has piece and its unfriendly, its already added so break now
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }
        // Check Up
        for (int y = 1; y < GameManager._Instance.BoardScript.GetNumberOfRows(); y++)
        {
            // Position to check
            var temp = currentLocation;
            temp += new Vector2Int(0, -y);

            if (!IsValidLocation(temp)) break;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            availablePositions.Add(temp);

            // Add position
            availablePositions.Add(temp);

            // If tile has piece and its unfriendly, its already added so break now
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }

        // Check if newPos is valid
        foreach (var position in availablePositions)
            GameManager._Instance.BoardScript.GetTileFromPosition(position).HighlightTile();
    }
    public override void MoveAddons(Vector2Int moveToLocation) { }
    public override void PostMoveAddons() { GameManager._Instance.NextTurn(); }
}
