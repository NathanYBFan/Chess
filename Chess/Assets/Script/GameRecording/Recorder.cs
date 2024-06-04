using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public Queue<ReplayData> recordingQueue { get; private set; }

    private void Awake()
    {
        recordingQueue = new Queue<ReplayData>();
    }

    public void RecordReplayFrame(ReplayData data)
    {
        recordingQueue.Enqueue(data);
        //Debug.Log("RecordingData: " + data.Piece + ":" + data.CurrentLocation + ":" + data.MoveToLocation + ":" + data.Player);
    }

    public bool ContainsMoveFromPiece(PieceNames pieceToCheckFor, Players playerAssigned)
    {
        ReplayData[] temp = new ReplayData[recordingQueue.Count];
        recordingQueue.CopyTo(temp, 0);

        for (int i = 0; i < recordingQueue.Count; i++)
            if (temp[i].Piece == pieceToCheckFor && temp[i].Player == playerAssigned) return true;
        return false;
    }
}
