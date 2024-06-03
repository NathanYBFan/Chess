using System.Collections.Generic;
using UnityEngine;

public class RookMovement : PieceMovement
{
    public override void HighlightAvailableLocations(Vector2Int currentLocation, bool facingUp)
    {
        List<Vector2Int> newPositions = new List<Vector2Int>();

        // Check Left
        for (var x = currentLocation.x - 1; x > -1; x--)
        {
            // Position to check
            Vector2Int positionToCheck = new Vector2Int(x, currentLocation.y);

            // Tile data to check against
            Tile tileScript = GameManager._Instance.BoardScript.GetTileFromPosition(positionToCheck).GetComponent<Tile>();
            
            // If tile has piece and its a friendly, break
            if (tileScript.HasPiece && GameManager._Instance.BoardScript.GetPieceOnTile(positionToCheck).PlayerAssigned 
                == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;

            // Add position
            newPositions.Add(tileScript.TileLocation);

            // If tile has piece and its unfriendly, its already added so break now
            if (tileScript.HasPiece && GameManager._Instance.BoardScript.GetPieceOnTile(positionToCheck).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }
        // Check Right
        for (int x = currentLocation.x + 1; x < GameManager._Instance.BoardScript.GetNumberOfColumns(); x++)
        {
            Vector2Int positionToCheck = new Vector2Int(x, currentLocation.y);
            Tile tileScript = GameManager._Instance.BoardScript.GetTileFromPosition(positionToCheck).GetComponent<Tile>();
            if (tileScript.HasPiece && GameManager._Instance.BoardScript.GetPieceOnTile(positionToCheck).PlayerAssigned
                == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            newPositions.Add(tileScript.TileLocation);

            if (tileScript.HasPiece && GameManager._Instance.BoardScript.GetPieceOnTile(positionToCheck).PlayerAssigned
                != GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
        }
        // Check Down
        for (int y = currentLocation.y + 1; y < GameManager._Instance.BoardScript.GetNumberOfRows(); y++)
        {
            Vector2Int positionToCheck = new Vector2Int(currentLocation.x, y);
            Tile tileScript = GameManager._Instance.BoardScript.GetTileFromPosition(positionToCheck).GetComponent<Tile>();
            if (tileScript.HasPiece && GameManager._Instance.BoardScript.GetPieceOnTile(positionToCheck).PlayerAssigned
                == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            newPositions.Add(tileScript.TileLocation);
        }
        // Check Up
        for (int y = currentLocation.y - 1; y > -1; y--)
        {
            Vector2Int positionToCheck = new Vector2Int(currentLocation.x, y);
            Tile tileScript = GameManager._Instance.BoardScript.GetTileFromPosition(positionToCheck).GetComponent<Tile>();
            if (tileScript.HasPiece && GameManager._Instance.BoardScript.GetPieceOnTile(positionToCheck).PlayerAssigned
                == GameManager._Instance.BoardScript.GetPieceOnTile(currentLocation).PlayerAssigned) break;
            newPositions.Add(tileScript.TileLocation);
        }

        // Check if newPos is valid
        foreach (var position in newPositions)
        {
            if (!IsValidLocation(position)) continue;
            GameManager._Instance.BoardScript.GetTileFromPosition(position).HighlightTile();
        }
    }

    public override void MoveAddons() { }
}
