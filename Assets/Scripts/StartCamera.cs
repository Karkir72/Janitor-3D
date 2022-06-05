using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class StartCamera : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _startPositionOffset;
    [SerializeField] private Vector3 _startRotationOffset;
    [SerializeField] private AudioClip _audioClip;

    private AudioSource _audioSource;
    private Vector3 _targetPosition;
    private Vector3 _targetRotation;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_audioClip);

        _targetPosition = transform.position;
        _targetRotation = transform.rotation.eulerAngles;
        transform.position = transform.position + _startPositionOffset;
        transform.rotation = Quaternion.Euler(_targetRotation + _startRotationOffset);

        transform
            .DOMove(_targetPosition, _duration)
            .SetEase(Ease.OutSine);
        transform
            .DORotate(_targetRotation, _duration)
            .SetEase(Ease.OutSine);
    }
}
