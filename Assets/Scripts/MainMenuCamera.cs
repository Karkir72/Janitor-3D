using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] private GameObject _levelsObject;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;

    private LevelBtn[] _levels;
    private Vector3 _startPosition;
    private Camera _camera;
    private bool _canMove = false;

    private void Start()
    {
        _levels = _levelsObject.GetComponentsInChildren<LevelBtn>();
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (_canMove == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                float newPosition = _camera.ScreenToViewportPoint(Input.mousePosition).y - _startPosition.y;
                float movementFactor = 20f;

                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.Clamp(
                        transform.position.y - newPosition * movementFactor,
                        _levels[0].transform.position.y,
                        _levels[_levels.Length - 1].transform.position.y),
                    transform.position.z);
            }
        }
    }

    public void MoveToCurrentLevel()
    {
        Vector3 targetPosition = transform.position;

        if (PlayerPrefs.HasKey(Preferences.LastLevel) == false)
        {
            targetPosition.y = _levels[0].transform.position.y;
        }
        else if (PlayerPrefs.GetInt(Preferences.LastLevel) < _levels.Length)
        {
            targetPosition.y = _levels[PlayerPrefs.GetInt(Preferences.LastLevel)].transform.position.y;            
        }
        else
        {
            targetPosition.y = _levels[PlayerPrefs.GetInt(Preferences.LastLevel) - 1].transform.position.y;
        }

        transform.DOMove(targetPosition, _duration).SetEase(_animationCurve);
        _canMove = true;
    }

    public void MoveToSettings()
    {
        Vector3 targetPosition = transform.position;

        targetPosition.y = _settingsPanel.transform.position.y;
        transform.DOMove(targetPosition, _duration).SetEase(_animationCurve);
    }

    public void MoveToStart()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.y = 0f;

        transform.DOMove(targetPosition, _duration).SetEase(_animationCurve);
        _canMove = false;
    }
}
