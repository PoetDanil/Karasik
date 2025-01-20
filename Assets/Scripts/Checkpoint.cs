using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Checkpoint : MonoBehaviour
{
    public static Action<Transform> OnCheckpointReached;

    [SerializeField] private AudioClip _musicClip;
    private AudioSource _audioSource;
    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_musicClip != null)
        {
            _audioSource.clip = _musicClip;
            _audioSource.Play();
        }
        else
        {
         
        }

        OnCheckpointReached?.Invoke(transform);
        _collider.enabled = false;
    }
}
