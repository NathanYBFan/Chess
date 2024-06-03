using System.Collections.Generic;
using UnityEngine;

public class BishopMovement : PieceMovement
{
    public override void HighlightAvailableLocations(Vector2Int currentLocation, bool facingUp)
    {
        List<Vector2Int> availablePositions = new List<Vector2Int>();
        // Check top left
        for (int y = 1; y < GameManager._Instance.BoardScript.GetNumberOfRows(); y++)
        {
            var temp = currentLocation;
            temp += new Vector2Int(-y, -y);

            if (!IsValidLocation(temp)) break;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            availablePositions.Add(temp);

            // If tile has piece and its unfriendly, its already added so break now
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }
        // Check top right
        for (int y = 1; y < GameManager._Instance.BoardScript.GetNumberOfRows(); y++)
        {
            var temp = currentLocation;
            temp += new Vector2Int(y, -y);

            if (!IsValidLocation(temp)) break;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            availablePositions.Add(temp);

            // If tile has piece and its unfriendly, its already added so break now
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }
        for (int y = 1; y < GameManager._Instance.BoardScript.GetNumberOfRows(); y++)
        {
            var temp = currentLocation;
            temp += new Vector2Int(-y, y);

            if (!IsValidLocation(temp)) break;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            availablePositions.Add(temp);

            // If tile has piece and its unfriendly, its already added so break now
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }
        // Check bottom right
        for (int y = 1; y < GameManager._Instance.BoardScript.GetNumberOfRows(); y++)
        {
            var temp = currentLocation;
            temp += new Vector2Int(y, y);

            if (!IsValidLocation(temp)) break;
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            availablePositions.Add(temp);

            // If tile has piece and its unfriendly, its already added so break now
            if (GameManager._Instance.BoardScript.GetPieceOnTile(temp) != null && GameManager._Instance.BoardScript.GetPieceOnTile(temp).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }

        foreach (var position in availablePositions)
            GameManager._Instance.BoardScript.GetTileFromPosition(position).HighlightTile();
    }
    public override void MoveAddons(Vector2Int moveToLocation) { }
    public override void PostMoveAddons() { GameManager._Instance.NextTurn(); }
}
