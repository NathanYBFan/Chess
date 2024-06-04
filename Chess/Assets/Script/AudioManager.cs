using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _Instance;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
            Destroy(gameObject);
        else if (_Instance == null)
            _Instance = this;
    }

    public void PlaySoundFX(int soundFXToPlay)
    {
        audioSource.PlayOneShot(audioClips[soundFXToPlay]);
    }

}
