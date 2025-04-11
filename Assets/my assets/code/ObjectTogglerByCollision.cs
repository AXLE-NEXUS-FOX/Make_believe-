using UnityEngine;

public class ObjectTogglerByCollision : MonoBehaviour
{
    [Tooltip("The GameObject to toggle on/off.")]
    public GameObject targetObject;

    [Tooltip("The tag(s) of the other object(s) that can trigger the toggle. Leave empty to trigger with any collider.")]
    public string[] triggeringTags;

    [Tooltip("Should the object toggle when the other object enters the collider?")]
    public bool toggleOnEnter = true;

    [Tooltip("Should the object toggle when the other object exits the collider?")]
    public bool toggleOnExit = false;

    private bool isObjectActive;

    void Start()
    {
        // Ensure a Collider is attached to this GameObject
        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("This script requires a Collider component to be attached to the same GameObject.", this);
            enabled = false; // Disable the script if no collider is present
            return;
        }

        // Ensure a Target Object is assigned
        if (targetObject == null)
        {
            Debug.LogError("Target Object is not assigned in the Inspector!", this);
            enabled = false; // Disable the script if no target object is set
            return;
        }

        // Initialize the state based on the target object's initial active state
        isObjectActive = targetObject.activeSelf;
    }

    private bool CanTrigger(Collider other)
    {
        // If no triggering tags are specified, any collider can trigger
        if (triggeringTags.Length == 0)
        {
            return true;
        }

        // Check if the other collider's tag is in the triggeringTags array
        foreach (string tag in triggeringTags)
        {
            if (other.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (toggleOnEnter && CanTrigger(other))
        {
            ToggleObject();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (toggleOnExit && CanTrigger(other))
        {
            ToggleObject();
        }
    }

    /// <summary>
    /// Toggles the active state of the target GameObject.
    /// </summary>
    public void ToggleObject()
    {
        if (targetObject != null)
        {
            isObjectActive = !isObjectActive;
            targetObject.SetActive(isObjectActive);
            Debug.Log($"Object '{targetObject.name}' toggled to: {isObjectActive} (by collision with '{name}')");
        }
        else
        {
            Debug.LogError("ToggleObject called but Target Object is null!", this);
            enabled = false; // Ensure the script is disabled if the target is lost
        }
    }

    /// <summary>
    /// Sets the active state of the target GameObject.
    /// </summary>
    /// <param name="newState">The desired active state (true for active, false for inactive).</param>
    public void SetObjectActive(bool newState)
    {
        if (targetObject != null && targetObject.activeSelf != newState)
        {
            isObjectActive = newState;
            targetObject.SetActive(isObjectActive);
            Debug.Log($"Object '{targetObject.name}' set to active: {isObjectActive} (by collision with '{name}')");
        }
        else if (targetObject == null)
        {
            Debug.LogError("SetObjectActive called but Target Object is null!", this);
            enabled = false;
        }
    }

    /// <summary>
    /// Returns the current active state of the target GameObject.
    /// </summary>
    /// <returns>True if the object is active, false otherwise.</returns>
    public bool IsObjectActive()
    {
        if (targetObject != null)
        {
            return targetObject.activeSelf;
        }
        else
        {
            Debug.LogError("IsObjectActive called but Target Object is null!", this);
            enabled = false;
            return false; // Return false as a default if the object is null
        }
    }
}