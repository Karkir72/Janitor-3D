using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public void StartNextLevel()
    {
        int nexLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nexLevel < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nexLevel);
        else
            SceneManager.LoadScene(0);
    }
}
