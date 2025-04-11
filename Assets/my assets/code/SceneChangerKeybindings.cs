using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerKeybindings : MonoBehaviour
{
    public static string sceneToLoad; // Static variable to hold the target scene name
    public string loadingSceneName = "LoadingScene"; // Name of your loading scene

    public void ChangeScene(string targetSceneName)
    {
        sceneToLoad = targetSceneName; // Store the target scene name
        SceneManager.LoadScene(loadingSceneName); // Load the loading scene
    }

    public void Exit()
    {
        Application.Quit();
    }
}

