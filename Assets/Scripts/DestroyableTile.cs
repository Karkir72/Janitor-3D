using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class DestroyableTile : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyEffect;
    [SerializeField] private bool _randomRotate;
    [SerializeField] private AudioClip[] _destroySounds;

    public event UnityAction<DestroyableTile> TileDestroyed;

    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_randomRotate)
        {
            float[] rotations = { 0, 90, 180, 270 };

            Quaternion randomRotate = Quaternion.Euler(
                rotations[Random.Range(0, rotations.Length)],
                rotations[Random.Range(0, rotations.Length)],
                rotations[Random.Range(0, rotations.Length)]
                );

            transform.rotation = randomRotate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _audioSource.PlayOneShot(_destroySounds[UnityEngine.Random.Range(0, _destroySounds.Length)]);
            TileDestroyed?.Invoke(this);
            _animator.SetTrigger(AnimatorSnowTile.Params.Destroy);
            Instantiate(_destroyEffect, transform.position, Quaternion.Euler(Vector3.left));
        }
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}

public static class AnimatorSnowTile
{
    public static class Params
    { 
        public const string Destroy = nameof(Destroy); 
    }

    public static class States
    {
        public const string DestroySnow = nameof(DestroySnow);
    }
}