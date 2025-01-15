using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangerKeybindings : MonoBehaviour
{
    // --- Configuration ---
    [Header("Keybindings")]
    [Tooltip("List of actions and their associated key/joystick bindings.")]
    public List<KeyActionPair> keybindings = new List<KeyActionPair>();

    [Header("Scene Management")]
    [Tooltip("Optional: Name of the scene to load. Use for direct scene loading.")]
    public string sceneToLoad;

    [Serializable]
    public class KeyActionPair
    {
        [Tooltip("Unique name for this action (e.g., 'Jump', 'Fire', 'NextScene').")]
        public string actionName;

        [Tooltip("Key to trigger this action on the keyboard.")]
        public KeyCode keyCode = KeyCode.None; // Default to None

        [Tooltip("Joystick button to trigger this action (e.g., 'Joystick1 Button0', 'Joystick2 ButtonA'). Leave empty for no joystick binding.")]
        public string joystickButton;

        [Tooltip("Description of what this action does. Shown in debug logs.")]
        public string actionDescription;

        //Helper function to make it easier to see what actions are bound to what
        public override string ToString()
        {
            return $"{actionName}: Key: {keyCode}, Joystick: {joystickButton}";
        }
    }

    void Start()
    {
        LoadKeybindings();

        // Log all keybindings at start for debugging
        DebugKeybindings();

        // Example Usage (You can remove these or add your own in the Inspector):
        AssignActionToKeybind("NextScene", ChangeToNextScene);
        AssignActionToKeybind("ExitGame", Exit);
    }

    void Update()
    {
        foreach (var binding in keybindings)
        {
            if (binding.keyCode != KeyCode.None && Input.GetKeyDown(binding.keyCode))
            {
                Debug.Log($"Action '{binding.actionName}' triggered by key '{binding.keyCode}'. Description: {binding.actionDescription}");
                CallAction(binding.actionName);
            }

            if (!string.IsNullOrEmpty(binding.joystickButton) && Input.GetButtonDown(binding.joystickButton))
            {
                Debug.Log($"Action '{binding.actionName}' triggered by joystick button '{binding.joystickButton}'. Description: {binding.actionDescription}");
                CallAction(binding.actionName);
            }
        }
    }

    public void AssignActionToKeybind(string actionName, Action action)
    {
        foreach (var binding in keybindings)
        {
            if (binding.actionName == actionName)
            {
                Debug.Log($"Assigned action '{action.Method.Name}' to keybind '{actionName}'.");
                return;
            }
        }
        Debug.LogWarning($"Keybind not found: {actionName}");
    }

    // Saving and Loading (using PlayerPrefs)
    private void SaveKeybindings()
    {
        for (int i = 0; i < keybindings.Count; i++)
        {
            PlayerPrefs.SetInt("Keybind_" + keybindings[i].actionName + "_Key", (int)keybindings[i].keyCode);
            PlayerPrefs.SetString("Keybind_" + keybindings[i].actionName + "_Joystick", keybindings[i].joystickButton);
        }
        PlayerPrefs.Save();
    }

    private void LoadKeybindings()
    {
        for (int i = 0; i < keybindings.Count; i++)
        {
            if (PlayerPrefs.HasKey("Keybind_" + keybindings[i].actionName + "_Key"))
            {
                keybindings[i].keyCode = (KeyCode)PlayerPrefs.GetInt("Keybind_" + keybindings[i].actionName + "_Key");
            }
            if (PlayerPrefs.HasKey("Keybind_" + keybindings[i].actionName + "_Joystick"))
            {
                keybindings[i].joystickButton = PlayerPrefs.GetString("Keybind_" + keybindings[i].actionName + "_Joystick");
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveKeybindings();
    }

    // Scene Management
    public void ChangeScene() // Now uses the sceneToLoad variable
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("No scene to load specified in the Inspector.");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void ChangeToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.Log("There is no next scene.");
        }
    }

    private void CallAction(string actionName)
    {
        switch (actionName)
        {
            case "NextScene":
                ChangeToNextScene();
                break;
            case "ExitGame":
                Exit();
                break;
            default:
                Debug.LogWarning("No action found for: " + actionName);
                break;
        }
    }

    public void OnButtonClick(string actionName)
    {
        Debug.Log($"Button clicked: {actionName}");
        CallAction(actionName);
    }

    private void DebugKeybindings()
    {
        Debug.Log("Current Keybindings:");
        foreach (var binding in keybindings)
        {
            Debug.Log(binding.ToString());
        }
    }
}
