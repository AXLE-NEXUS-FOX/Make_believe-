using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

// Automatically adds a Collider component if one doesn't exist.
// You still need to manually set 'Is Trigger' to true on the Collider.
[RequireComponent(typeof(Collider))]
public class SceneLoadTrigger : MonoBehaviour
{
    [Header("Scene Loading Settings")]
    [Tooltip("The exact name of the scene file to load when triggered.")]
    public string sceneNameToLoad;

    [Tooltip("The name of your dedicated loading scene.")]
    public string loadingSceneName = "LoadingScene"; // Make sure this matches your loading scene file name

    [Header("Trigger Settings")]
    [Tooltip("The tag the colliding object must have to trigger the scene load.")]
    public string requiredTag = "Player"; // Common tag for player characters

    private bool isLoading = false; // Flag to prevent triggering multiple loads simultaneously

    private void Awake()
    {
        // Ensure the attached collider is set as a trigger
        Collider col = GetComponent<Collider>();
        if (!col.isTrigger)
        {
            Debug.LogWarning($"The Collider on GameObject '{gameObject.name}' is not set to 'Is Trigger'. SceneLoadTrigger requires 'Is Trigger' to be enabled.", this);
            // Optional: Force it to be a trigger?
            // col.isTrigger = true;
        }
    }

    // This function is called when another Collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if already loading to prevent multiple triggers
        if (isLoading)
        {
            return;
        }

        // Check if the object that entered has the required tag
        if (other.CompareTag(requiredTag))
        {
            Debug.Log($"'{other.gameObject.name}' with tag '{requiredTag}' entered the trigger. Attempting to load scene: '{sceneNameToLoad}' via '{loadingSceneName}'.");

            // --- Validate Scene Names ---
            if (string.IsNullOrEmpty(sceneNameToLoad))
            {
                Debug.LogError("SceneNameToLoad is not set in the Inspector on the SceneLoadTrigger script!", this);
                return; // Stop execution if target scene is not set
            }
            if (string.IsNullOrEmpty(loadingSceneName))
            {
                Debug.LogError("LoadingSceneName is not set in the Inspector on the SceneLoadTrigger script!", this);
                return; // Stop execution if loading scene is not set
            }
            // Optional: Check if SceneChangerKeybindings class exists if needed
            // (Though accessing a static variable won't error immediately if the class is missing,
            // the loading scene controller *will* fail later if SceneChangerKeybindings.sceneToLoad wasn't set)


            // --- Initiate loading via the loading scene ---
            isLoading = true; // Set flag to prevent re-triggering

            // Store the target scene name for the LoadingScreenController to pick up
            SceneChangerKeybindings.sceneToLoad = sceneNameToLoad;

            // Load the loading scene itself
            SceneManager.LoadScene(loadingSceneName);
        }
        // Optional: Add an else block here if you want to log when something *else* enters the trigger
        // else
        // {
        //     Debug.Log($"'{other.gameObject.name}' entered trigger, but did not have the required tag '{requiredTag}'.");
        // }
    }

    // --- Optional: Reset flag if the object leaves ---
    // Useful if the player could potentially enter, leave, and re-enter
    // the trigger without the scene actually changing (e.g., if loading fails).
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag(requiredTag))
    //     {
    //          // Reset the flag ONLY if the object leaving is the one we care about
    //          // Be cautious with this if loading should be a one-way trip.
    //          // isLoading = false;
    //          // Debug.Log($"'{other.gameObject.name}' exited the trigger.");
    //     }
    // }

}