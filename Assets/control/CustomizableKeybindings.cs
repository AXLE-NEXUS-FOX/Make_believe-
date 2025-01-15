using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableKeybindings : MonoBehaviour
{
    // Serializable dictionary to store keybindings.
    [Serializable]
    public class KeyActionPair
    {
        public string actionName;
        public KeyCode keyCode;
    }

    public List<KeyActionPair> keybindings = new List<KeyActionPair>();
    private Dictionary<string, KeyCode> keybindDictionary = new Dictionary<string, KeyCode>();

    void Start()
    {
        LoadKeybindings(); // Load saved keybindings (if any)
        UpdateKeybindDictionary(); // Create a dictionary for faster lookup

        //Example usage of how to assign a function to a keybind
        AssignActionToKeybind("Jump", JumpFunction);
    }

    void Update()
    {
        foreach (var binding in keybindings)
        {
            if (Input.GetKeyDown(binding.keyCode))
            {
                // Execute the action associated with this key.
                Debug.Log("Key pressed: " + binding.actionName);

                //You can now use this to call a function using a string for example
                //This is how the example function is called
                //If you have a function called "Fire" and the keybind "Fire" is pressed it will run FireFunction()
                CallAction(binding.actionName);
            }
        }
    }

    //Example function to be called from a keybind
    private void JumpFunction()
    {
        Debug.Log("Jumping!");
        // Your jump logic here
    }

    //Example function to be called from a keybind
    private void FireFunction()
    {
        Debug.Log("Firing!");
        // Your fire logic here
    }

    //This function calls a function based on a string name
    private void CallAction(string actionName)
    {
        //Use a switch statement for more efficient lookups if you have many actions
        switch (actionName)
        {
            case "Jump":
                JumpFunction();
                break;
            case "Fire":
                FireFunction();
                break;
            default:
                Debug.LogWarning("No action found for: " + actionName);
                break;
        }
    }

    // Function to assign an action to a keybind.
    public void AssignActionToKeybind(string actionName, Action action)
    {
        // Find the keybinding for the given action name.
        foreach (var binding in keybindings)
        {
            if (binding.actionName == actionName)
            {
                // You would typically store the 'action' in some way.
                // For this simple example, we'll just log a message.
                Debug.Log("Assigned action to " + actionName);
                return;
            }
        }

        Debug.LogWarning("Keybind not found: " + actionName);
    }

    private void UpdateKeybindDictionary()
    {
        keybindDictionary.Clear();
        foreach (var binding in keybindings)
        {
            keybindDictionary[binding.actionName] = binding.keyCode;
        }
    }

    // Saving and Loading (using PlayerPrefs for simplicity)
    private void SaveKeybindings()
    {
        for (int i = 0; i < keybindings.Count; i++)
        {
            PlayerPrefs.SetInt("Keybind_" + keybindings[i].actionName, (int)keybindings[i].keyCode);
        }
        PlayerPrefs.Save();
    }

    private void LoadKeybindings()
    {
        for (int i = 0; i < keybindings.Count; i++)
        {
            if (PlayerPrefs.HasKey("Keybind_" + keybindings[i].actionName))
            {
                keybindings[i].keyCode = (KeyCode)PlayerPrefs.GetInt("Keybind_" + keybindings[i].actionName);
            }
        }
    }

    //Call this function to save the keybinds
    private void OnApplicationQuit()
    {
        SaveKeybindings();
    }
}