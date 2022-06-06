using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _destroyableTiles;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private float _sliderChangeSpeed;

    private DestroyableTile[] _tiles;
    private Coroutine _corutine;
    private int _startTilesCount;
    private int _currentTilesCount;

    private void Awake()
    {
        _tiles = _destroyableTiles.GetComponentsInChildren<DestroyableTile>();
        _startTilesCount = _tiles.Length;
        _currentTilesCount = _startTilesCount;
        _level.text = SceneManager.GetActiveScene().buildIndex.ToString();
    }

    private void OnEnable()
    {
        foreach (DestroyableTile tile in _tiles)
        {
            tile.TileDestroyed += OnTileDestroyed;
        }
    }

    private void OnDisable()
    {
        foreach (DestroyableTile tile in _tiles)
        {
            tile.TileDestroyed -= OnTileDestroyed;
        }
    }

    private void OnTileDestroyed(DestroyableTile tile)
    {
        if (_corutine != null)
            StopCoroutine(_corutine);

        _currentTilesCount--;
        _corutine = StartCoroutine(Change(_currentTilesCount));
    }

    private IEnumerator Change(float newVelue)
    {
        float endValue = Mathf.InverseLerp(0, _startTilesCount, _startTilesCount - newVelue);

        while (_slider.value != endValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, endValue, _sliderChangeSpeed);

            yield return null;
        }
    }
}
