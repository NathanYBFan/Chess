using UnityEngine;

public class PreviousMoveManager : MonoBehaviour
{
    public static PreviousMoveManager _Instance;

    [SerializeField]
    private Recorder recorder;

    public Recorder Recorder => recorder;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
            Destroy(gameObject);
        else
            _Instance = this;
    }

    public void AddMove(PieceNames piece, Players player, Vector2Int currentLocation, Vector2Int moveToLocation)
    {
        Recorder.RecordReplayFrame(new ReplayData(piece, player, currentLocation, moveToLocation));
    }
}
