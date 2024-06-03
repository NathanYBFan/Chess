using UnityEngine;
using UnityEngine.UI;

public sealed class Board : MonoBehaviour
{
    [SerializeField]
    private Rows[] rows;

    [SerializeField]
    private Color tileAColor;

    [SerializeField]
    private Color tileBColor;

    // Getters & Setters
    public int GetNumberOfRows() => rows.Length;
    public int GetNumberOfColumns() => rows[0].Tiles.Length;
    public Rows GetRow(int rowToGet) => rows[rowToGet];

    private void Start()
    {
        for (var y = 0; y < rows.Length; y++)
        {
            for (var x = 0; x < rows[0].Tiles.Length; x++)
            {
                Color colorToSet = (x + y) % 2 == 0 ? tileAColor : tileBColor;
                rows[y].Tiles[x].GetComponent<Image>().color = colorToSet;
                rows[y].Tiles[x].GetComponent<Tile>().TileLocation = new Vector2Int(x, y);
            }
        }
    }

    public void PlacePiece(GameObject piece, Piece pieceID, Vector2Int location)
    {
        // Set saved location
        pieceID.PieceMovement.CurrentLocation = location;

        // Assign piece data
        piece.GetComponent<defaultPiece>().PieceAssigned = pieceID;
        // Reset transform parent
        piece.transform.SetParent(rows[location.x].Tiles[location.y].transform, false);

        // Set tile data
        Tile tileData = rows[location.x].Tiles[location.y].GetComponent<Tile>();
        tileData.HasPiece = true;

        // Reset local position to 0
        piece.transform.localPosition = Vector3.zero;
    }

    public void MovePiece(GameObject piece, Vector2Int newLocation)
    {
        // Set saved location
        piece.GetComponent<defaultPiece>().PieceAssigned.PieceMovement.CurrentLocation = newLocation;

        // Reset transform parent
        piece.transform.SetParent(rows[newLocation.y].Tiles[newLocation.x].transform, false);

        // Set new tile data
        GameManager._Instance.BoardScript.GetTileFromPosition(newLocation).HasPiece = true;

        // Reset local position to 0
        piece.transform.localPosition = Vector3.zero;

        GameManager._Instance.DeselectAllTiles();
        GameManager._Instance.NextTurn();
    }

    public Tile GetTileFromPosition(Vector2Int positionToCheck)
    {
        //Debug.Log(rows[positionToCheck.y].Tiles[positionToCheck.x].name + " in row: " + positionToCheck.y + " at: " + positionToCheck);
        if (positionToCheck.x > GetNumberOfColumns() || positionToCheck.x < 0 || positionToCheck.y > GetNumberOfRows() || positionToCheck.y < 0)
            return null;

        return rows[positionToCheck.y].Tiles[positionToCheck.x].GetComponent<Tile>();
    }

    public Piece GetPieceOnTile(Vector2Int positionToCheck)
    {
        if (GetTileFromPosition(positionToCheck).transform.childCount > 1)
            return GetTileFromPosition(positionToCheck).transform.GetChild(1).GetComponent<defaultPiece>().PieceAssigned;
        return null;
    }

    public GameObject GetObjectOnTile(Vector2Int positionToCheck)
    {
        if (GetTileFromPosition(positionToCheck).transform.childCount > 1)
            return GetTileFromPosition(positionToCheck).transform.GetChild(1).gameObject;
        return null;
    }
}
