using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        ResetTimeScale();

        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        ResetTimeScale();

        int nextIndex =
            SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("No next scene available.");
            return;
        }

        SceneManager.LoadScene(nextIndex);
    }

    public void ReloadScene()
    {
        ResetTimeScale();

        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        Debug.Log("Quit Game");
#endif
    }

    private void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
}