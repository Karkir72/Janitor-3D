using UnityEngine;

public class ResetBtn : MonoBehaviour
{
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
