using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleTextBox;

    [SerializeField]
    private TextMeshProUGUI statTextBox;

    private void OnEnable()
    {
        titleTextBox.text = GameManager._Instance.PlayerTurn == Players.PlayerA ? "White Wins!": "Black Wins!";
        statTextBox.text = "Total moves this game: " + PreviousMoveManager._Instance.Recorder.recordingQueue.Count;
        AudioManager._Instance.PlaySoundFX(3);
    }

    public void ContinueButtonPressed()
    {
        GameManager._Instance.ResetAll();
        GameManager._Instance.DeactivateWinScreen();
        AudioManager._Instance.PlaySoundFX(1);
    }
}
