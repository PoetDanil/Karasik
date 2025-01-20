using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _musicClip;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_musicClip != null)
        {
            _audioSource.clip = _musicClip;
            _audioSource.Play();
        }
        else
        {
         
        }
    }
}
