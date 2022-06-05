using DG.Tweening;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SwipeDetection))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SwipeDetection _swipeDetection;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isMoving;

    private void Awake()
    {
        _swipeDetection = GetComponent<SwipeDetection>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _swipeDetection.SwipeEvent += OnSwipe;
    }

    private void OnDisable()
    {
        _swipeDetection.SwipeEvent -= OnSwipe;
    }

    private void OnSwipe(Vector2 direction)
    {
        if (_isMoving == false)
        {
            Vector3 targetPosition = transform.position;

            if (direction == Vector2.up)
            {
                targetPosition.x = FindClosestWall(Vector3.left).transform.position.x + 1f;

                if (targetPosition != transform.position)
                {
                    _animator.Play(AnimatorPlayer.States.RunBack);
                    _spriteRenderer.flipX = false;
                    Move(targetPosition);
                }
            }
            else if (direction == Vector2.down)
            {
                targetPosition.x = FindClosestWall(-Vector3.left).transform.position.x - 1f;

                if (targetPosition != transform.position)
                {
                    _animator.Play(AnimatorPlayer.States.RunForward);
                    _spriteRenderer.flipX = true;
                    Move(targetPosition);
                }
            }
            else if (direction == Vector2.left)
            {
                targetPosition.z = FindClosestWall(-Vector3.forward).transform.position.z + 1f;

                if (targetPosition != transform.position)
                {
                    _animator.Play(AnimatorPlayer.States.RunBack);
                    _spriteRenderer.flipX = true;
                    Move(targetPosition);
                }
            }
            else if (direction == Vector2.right)
            {
                targetPosition.z = FindClosestWall(Vector3.forward).transform.position.z - 1f;

                if (targetPosition != transform.position)
                {
                    _animator.Play(AnimatorPlayer.States.RunForward);
                    _spriteRenderer.flipX = false;
                    Move(targetPosition);
                }
            }
        }
    }

    private Wall FindClosestWall(Vector3 vector)
    {
        Wall closestWall = null;

        Vector3 startRayPosition = transform.position;
        startRayPosition.y -= 0.5f;

        RaycastHit[] hits = Physics.RaycastAll(startRayPosition, vector).OrderBy(hit => hit.distance).ToArray();

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.TryGetComponent<Wall>(out Wall wall))
            {
                return wall;
            }
        }

        return closestWall;
    }

    private void Move(Vector3 targetPosition)
    {
        float delta = Vector3.Distance(transform.position, targetPosition);
        _isMoving = true;
        transform
            .DOMove(targetPosition, delta / _speed)
            .SetEase(Ease.OutSine)
            .OnComplete(StopMove);
    }

    private void StopMove()
    {
        _isMoving = false;
        _animator.Play(AnimatorPlayer.States.Idle);
        _spriteRenderer.flipX = false;
    }

    public void Win(Vector3 winPosition)
    {
        float winMovementSpeed = 0.2f;

        DOTween.Clear();
        transform
            .DOMove(winPosition, winMovementSpeed)
            .SetEase(Ease.OutSine);

        _isMoving = true;
        _animator.Play(AnimatorPlayer.States.Jump);
        _spriteRenderer.flipX = false;
        Handheld.Vibrate();
    }
}
