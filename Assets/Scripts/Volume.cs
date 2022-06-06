using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Volume : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        if (PlayerPrefs.HasKey(Preferences.Volume))
            _slider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", _slider.value);
    }
}
