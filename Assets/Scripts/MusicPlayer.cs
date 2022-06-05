using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float _defaltVolume = 0.5f;

    static MusicPlayer instance = null;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            if (PlayerPrefs.HasKey("Volume"))
            {
                _audioSource.volume = PlayerPrefs.GetFloat("Volume");
            }
            else
            {
                _audioSource.volume = _defaltVolume;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
