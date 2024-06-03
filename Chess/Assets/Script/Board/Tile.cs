using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject glowHighlightObject;

    // Private variables
    [SerializeField]
    private Vector2Int tileLocation;

    [SerializeField]
    private bool hasPiece;

    // Setters & Getters
    public Vector2Int TileLocation { get { return tileLocation; } set { tileLocation = value; } }
    public bool HasPiece { get { return hasPiece; } set { hasPiece = value; } }

    public void TileSelected()
    {
        GameManager._Instance.DeselectAllTiles();
        GameManager._Instance.SelectedPiece = null;

        if (!hasPiece) return;
        if (GameManager._Instance.PlayerTurn != GameManager._Instance.BoardScript.GetPieceOnTile(TileLocation).PlayerAssigned) return;

        GameManager._Instance.SelectedPiece = gameObject.transform.GetChild(1).gameObject;
        
        // Set current location to selected tile
        GameManager._Instance.SelectedPiece.GetComponent<defaultPiece>().PieceAssigned.PieceMovement.CurrentLocation = tileLocation;

        GameManager._Instance.BoardScript.GetPieceOnTile(TileLocation).PieceMovement
            .HighlightAvailableLocations(TileLocation, GameManager._Instance.BoardScript.GetPieceOnTile(TileLocation).PlayerAssigned == Players.PlayerA);

        // glowHighlightObject.SetActive(true);
        GameManager._Instance.SelectedTiles.Add(this);
    }

    public void HighlightTile()
    {
        glowHighlightObject.SetActive(true);
        GameManager._Instance.SelectedTiles.Add(this);
    }

    public void DeselectTile()
    {
        glowHighlightObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager._Instance.CanSelect) return;
        // Double click on same piece
        if (hasPiece && gameObject.transform.GetChild(1).gameObject == GameManager._Instance.SelectedPiece)
        {
            GameManager._Instance.DeselectAllTiles();
        }
        // glow is active
        else if (glowHighlightObject.activeInHierarchy)
        {
            if (GameManager._Instance.SelectedPiece != null)
            {
                if (hasPiece)
                    GameManager._Instance.BoardScript.GetPieceOnTile(tileLocation).Death(tileLocation);

                GameManager._Instance.SelectedPiece.GetComponent<defaultPiece>().PieceAssigned.PieceMovement.MovePiece(TileLocation);
            }
        }
        else
        {
            TileSelected();
        }
    }
}
