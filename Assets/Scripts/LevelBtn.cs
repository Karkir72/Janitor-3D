using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Button))]
public class LevelBtn : MonoBehaviour
{
    [SerializeField] private Sprite _disabledSprite;

    private int _levelNum;
    private TextMeshProUGUI _text;
    private Button _button;

    private void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
        _levelNum = transform.GetSiblingIndex() + 1;
        _text.text = _levelNum.ToString();

        if (IsAvailable() == false)
        {
            GetComponentInChildren<Image>().sprite = _disabledSprite;
            _text.gameObject.SetActive(false);
            _button.interactable = false;
        }
    }

    private bool IsAvailable()
    {
        if (_levelNum == 1)
        {
            return true;
        }
        else if (PlayerPrefs.GetInt("LastLevel") + 1 >= _levelNum)
        {
            return true;
        }

        return false;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelNum);
    }
}
