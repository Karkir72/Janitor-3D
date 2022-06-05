using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsGroup : MonoBehaviour
{
    [SerializeField] private GameObject _levelButtonPrefab;

    private int _countLevels;

    private void Awake()
    {
        _countLevels = SceneManager.sceneCountInBuildSettings - 1;

        for (int i = 0; i < _countLevels; i++)
        {
            Instantiate(_levelButtonPrefab, transform);
        }
    }
}
