using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [SerializeField]
    private GameObject pieceChangeUIObject;

    [SerializeField]
    private List<GameObject> activePieces;

    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private TextMeshProUGUI playerTurnText;

    private bool canSelect = true;

    // Getters & Setters
    public Players PlayerTurn => playerTurn;
    public Board BoardScript => boardScript;
    public Piece[] ListOfBlackPieces { get { return listOfBlackPieces; } }
    public Piece[] ListOfWhitePieces { get { return listOfWhitePieces; } }
    public List<Tile> SelectedTiles { get { return selectedTiles; } set { selectedTiles = value; } }
    public GameObject SelectedPiece { get { return selectedPiece; } set { selectedPiece = value; } }
    public bool CanSelect { get { return canSelect; } set { canSelect = value; } }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
            Destroy(this.gameObject);

        else if (_Instance == null)
            _Instance = this;

        activePieces = new List<GameObject>();
    }

    private void Start()
    {
        playerTurn = Players.PlayerA;
        playerTurnText.text = "White's turn";
        SetupBoard();
        canSelect = true;
    }

    private void SetupBoard()
    {
        // Pawns
        for (var i = 0; i < 8; i++)
        {
            GameObject white_Pawn = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(white_Pawn, listOfWhitePieces[0], new Vector2Int(i, 6));

            GameObject black_Pawn = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(black_Pawn, listOfBlackPieces[0], new Vector2Int(i, 1));

            activePieces.Add(white_Pawn);
            activePieces.Add(black_Pawn);
        }

        // Rooks/Knights/Bishops
        for (var i = 0; i < 2; i++)
        {
            // White
            GameObject white_Knight = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(white_Knight, listOfWhitePieces[2], new Vector2Int(1 + i * 5, 7));

            GameObject white_Bishop = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(white_Bishop, listOfWhitePieces[3], new Vector2Int(2 + i * 3, 7));

            // Black
            GameObject black_Knight = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(black_Knight, listOfBlackPieces[2], new Vector2Int(1 + i * 5, 0));

            GameObject black_Bishop = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
            boardScript.PlacePiece(black_Bishop, listOfBlackPieces[3], new Vector2Int(2 + i * 3, 0));

            activePieces.Add(white_Knight);
            activePieces.Add(white_Bishop);

            activePieces.Add(black_Knight);
            activePieces.Add(black_Bishop);
        }

        // Rooks
        GameObject white_RookA = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(white_RookA, listOfWhitePieces[1], new Vector2Int(0, 7));

        GameObject white_RookB = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(white_RookB, listOfWhitePieces[6], new Vector2Int(7, 7));

        GameObject black_RookA = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(black_RookA, listOfBlackPieces[1], new Vector2Int(0, 0));
        
        GameObject black_RookB = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(black_RookB, listOfBlackPieces[6], new Vector2Int(7, 0));

        // Kings
        GameObject white_King = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(white_King, listOfWhitePieces[4], new Vector2Int(4, 7));

        GameObject black_King = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(black_King, listOfBlackPieces[4], new Vector2Int(3, 0));

        // Queens
        GameObject white_Queen = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(white_Queen, listOfWhitePieces[5], new Vector2Int(3, 7));

        GameObject black_Queen = Instantiate(defaultPiecePrefab, boardScript.gameObject.transform);
        boardScript.PlacePiece(black_Queen, listOfBlackPieces[5], new Vector2Int(4, 0));
        
        activePieces.Add(white_RookA);
        activePieces.Add(white_RookB);
        activePieces.Add(black_RookA);
        activePieces.Add(black_RookB);

        activePieces.Add(white_King);
        activePieces.Add(black_King);
        activePieces.Add(white_Queen);
        activePieces.Add(black_Queen);
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
        playerTurn = (playerTurn == Players.PlayerA) ? Players.PlayerB : Players.PlayerA;

        playerTurnText.text = playerTurn == Players.PlayerA ? "White's Turn" : "Black's Turn";
    }

    public void PawnChange(Vector2Int pawnToChangeLocation)
    {
        if (winScreen.activeSelf) return;
        GameObject temp = Instantiate(pieceChangeUIObject, boardScript.gameObject.transform.position, Quaternion.identity);
        var tileLocation = boardScript.GetTileFromPosition(pawnToChangeLocation).gameObject.transform.position;

        temp.transform.SetParent(BoardScript.gameObject.transform);

        temp.transform.position = pawnToChangeLocation.y == 0 ?  new Vector3(tileLocation.x, tileLocation.y): new Vector3 (tileLocation.x, boardScript.GetTileFromPosition((pawnToChangeLocation - new Vector2Int(0, 7))).gameObject.transform.position.y);

        temp.GetComponent<PieceChangeUI>().InitializeUI(pawnToChangeLocation, boardScript.GetPieceOnTile(pawnToChangeLocation).PlayerAssigned);
    }

    public void ResetAll()
    {
        playerTurn = Players.PlayerA;
        playerTurnText.text = playerTurn == Players.PlayerA ? "White's Turn" : "Black's Turn";

        AudioManager._Instance.PlaySoundFX(1);
        // Destroy all pieces
        for (int i = 0; i < activePieces.Count; i++)
            Destroy(activePieces[i]);
        // Clear list
        activePieces.Clear();

        for (int y = 0; y < boardScript.GetNumberOfRows(); y++)
            for (int x = 0; x < boardScript.GetNumberOfColumns(); x++)
                boardScript.GetTileFromPosition(new Vector2Int(x, y)).HasPiece = false;

        // Clear all previous moves
        PreviousMoveManager._Instance.Recorder.recordingQueue.Clear();

        // Reset board
        SetupBoard();
    }

    public void EndGame()
    {
        winScreen.SetActive(true);
    }

    public void DeactivateWinScreen()
    {
        winScreen.SetActive(false);
    }
}
