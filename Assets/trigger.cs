
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class BloomTrigger : MonoBehaviour
{
    public PostProcessVolume postProcessingVolume; // Reference to the volume with the post-processing effects
    private Bloom bloomEffect; // Reference to the Bloom effect

    // Animation Curves for smooth transition
    public AnimationCurve intensityCurve = AnimationCurve.Linear(0, 0, 1, 1);  // From 0 to 1 over time
    public AnimationCurve thresholdCurve = AnimationCurve.Linear(0, 1, 1, 1);  // From 1 to 1 (no change) by default

    // Bloom settings to change
    public float newBloomIntensity = 1.5f;
    public float newBloomThreshold = 1.0f;

    // Duration for animation (time to complete transition)
    public float transitionDuration = 1.0f;

    // Variables to store the original bloom settings
    private float originalBloomIntensity;
    private float originalBloomThreshold;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerBloomSettingsChange();
        }
    }

    void Start()
    {
        //postProcessingVolume = CamWithPost.GetComponent<PostProcessVolume>();

        // Ensure the Volume component is assigned
        if (postProcessingVolume != null)
        {
            // Try to get the Bloom effect from the volume
            if (postProcessingVolume.profile.TryGetSettings(out bloomEffect))
            {
                // Store the original settings
                originalBloomIntensity = bloomEffect.intensity.value;
                originalBloomThreshold = bloomEffect.threshold.value;

                Debug.Log("Bloom effect found and ready to modify.");
            }
            else
            {
                Debug.LogWarning("No Bloom effect found in the Volume profile.");
            }
        }
        else
        {
            Debug.LogError("PostProcessing Volume is not assigned.");
        }
    }

    // Coroutine to smoothly change bloom settings over time
    private IEnumerator AnimateBloomSettings(float fromIntensity, float toIntensity, float fromThreshold, float toThreshold)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            // Use the curves to interpolate the intensity and threshold
            bloomEffect.intensity.value = Mathf.Lerp(fromIntensity, toIntensity, intensityCurve.Evaluate(t));
            bloomEffect.threshold.value = Mathf.Lerp(fromThreshold, toThreshold, thresholdCurve.Evaluate(t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final values are set
        bloomEffect.intensity.value = toIntensity;
        bloomEffect.threshold.value = toThreshold;
    }

    // Trigger method to change bloom settings
    public void TriggerBloomSettingsChange()
    {
        if (bloomEffect != null)
        {
            StartCoroutine(AnimateBloomSettings(originalBloomIntensity, newBloomIntensity, originalBloomThreshold, newBloomThreshold));
            Debug.Log("Bloom settings animation started.");
        }
        else
        {
            Debug.LogError("Bloom effect is not initialized.");
        }
    }

    // Restore the original bloom settings
    public void RestoreBloomSettings()
    {
        if (bloomEffect != null)
        {
            StartCoroutine(AnimateBloomSettings(bloomEffect.intensity.value, originalBloomIntensity, bloomEffect.threshold.value, originalBloomThreshold));
            Debug.Log("Bloom settings restoration started.");
        }
        else
        {
            Debug.LogError("Bloom effect is not initialized.");
        }
    }

    // Triggered when something enters the trigger zone
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Replace with the tag of the object that should trigger the change
        {
            TriggerBloomSettingsChange();
        }
    }
    */
    // Triggered when something exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Replace with the tag of the object that should restore the settings
        {
            RestoreBloomSettings();
        }
    }
}