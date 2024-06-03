using UnityEngine;

public class KingMovement : PieceMovement
{
    public override void HighlightAvailableLocations(Vector2Int currentLocation, bool facingUp)
    {
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

    public override void MoveAddons() { }
}
