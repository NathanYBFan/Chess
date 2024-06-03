using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    [SerializeField]
    private Players playerTurn;

    [SerializeField]
    private Piece[] listOfWhitePieces;

    [SerializeField]
    private Piece[] listOfBlackPieces;

    [Header("Board & Pieces")]
    [SerializeField]
    private Board boardScript;

    [SerializeField]
    private GameObject defaultPiecePrefab;

    [SerializeField]
    private List<Tile> selectedTiles;

    [SerializeField]
    private GameObject selectedPiece;

    // Getters & Setters
    public Players PlayerTurn => playerTurn;
    public Board BoardScript => boardScript;
    public List<Tile> SelectedTiles { get { return selectedTiles; } set { selectedTiles = value; } }
    public GameObject SelectedPiece { get { return selectedPiece; } set { selectedPiece = value; } }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
            Destroy(this.gameObject);

        else if (_Instance == null)
            _Instance = this;
    }

    private void Start()
    {
        playerTurn = Players.PlayerA;
        SetupBoard();
    }

    private void SetupBoard()
    {
        // Pawns
        for (var i = 0; i < 8; i++)
        {
            GameObject white_Pawn = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(white_Pawn, listOfWhitePieces[0], new Vector2Int(6, i));

            GameObject black_Pawn = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(black_Pawn, listOfBlackPieces[0], new Vector2Int(1, i));
        }

        // Rooks/Knights/Bishops
        for (var i = 0; i < 2; i++)
        {
            // White
            GameObject white_Rook = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(white_Rook, listOfWhitePieces[1], new Vector2Int(7, i * 7));

            //GameObject white_Knight = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            //boardScript.PlacePiece(white_Knight, listOfWhitePieces[2], new Vector2Int(7, 1 + i * 5));

            GameObject white_Bishop = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(white_Bishop, listOfWhitePieces[3], new Vector2Int(7, 2 + i * 3));

            // Black
            GameObject black_Rook = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(black_Rook, listOfBlackPieces[1], new Vector2Int(0, i * 7));

            //GameObject black_Knight = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            //boardScript.PlacePiece(black_Knight, listOfBlackPieces[2], new Vector2Int(0, 1 + i * 5));

            GameObject black_Bishop = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(black_Bishop, listOfBlackPieces[3], new Vector2Int(0, 2 + i * 3));
        }

        // Kings
        GameObject white_King = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(white_King, listOfWhitePieces[4], new Vector2Int(7, 3));

        GameObject black_King = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(black_King, listOfBlackPieces[4], new Vector2Int(0, 3));

        //// Queens
        //GameObject white_Queen = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        //boardScript.PlacePiece(white_Queen, listOfWhitePieces[5], new Vector2Int(7, 4));

        //GameObject black_Queen = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        //boardScript.PlacePiece(black_Queen, listOfBlackPieces[5], new Vector2Int(0, 4));
    }

    public void DeselectAllTiles()
    {
        foreach (var tile in selectedTiles)
            tile.DeselectTile();

        selectedTiles.Clear();
        selectedPiece = null;
    }

    public void NextTurn()
    {
        selectedPiece = null;
        // check if win
        playerTurn = playerTurn == Players.PlayerA ? Players.PlayerB : Players.PlayerA;
    }
}
