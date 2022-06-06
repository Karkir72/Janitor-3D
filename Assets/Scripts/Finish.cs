using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _gift;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private PlayerMove _playerMove;

    private AudioSource _audioSource;
    private DestroyableTile[] _tiles;
    private List<DestroyableTile> _tempTiles;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _tiles = GetComponentsInChildren<DestroyableTile>();
        _tempTiles = new List<DestroyableTile>();

        foreach (DestroyableTile tile in _tiles)
        {
            _tempTiles.Add(tile);
        }
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
        if (_tempTiles.Count == 1)
        {
            Vector3 winPosition = new Vector3(_tempTiles[0].transform.position.x, _playerMove.transform.position.y, _tempTiles[0].transform.position.z);
            _playerMove.Win(winPosition);
            Instantiate(_gift, _tempTiles[0].transform.position, Quaternion.identity);
            _audioSource.PlayOneShot(_winSound);
            _winScreen.SetActive(true);
            PlayerPrefs.SetInt(Preferences.LastLevel, SceneManager.GetActiveScene().buildIndex);
        }

        _tempTiles.Remove(tile);
    }
}
