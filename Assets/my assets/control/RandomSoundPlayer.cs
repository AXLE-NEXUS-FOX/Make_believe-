using UnityEngine;
using System.Collections.Generic;

public class RandomSoundPlayer : MonoBehaviour
{
    public List<AudioClip> soundClips; // List of sound clips to play
    private AudioSource audioSource;

    void Start()
    {
        // Ensure there are sound clips in the list
        if (soundClips == null || soundClips.Count == 0)
        {
            Debug.LogError("No sound clips assigned to the RandomSoundPlayer!");
            enabled = false; // Disable the script if no clips are assigned
            return;
        }

        // Add an AudioSource component if one doesn't exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Check for the Enter key press
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            PlayRandomSound();
        }
    }

    void PlayRandomSound()
    {
        if (soundClips.Count > 0)
        {
            // Pick a random index within the list
            int randomIndex = Random.Range(0, soundClips.Count);

            // Play the selected sound clip
            audioSource.clip = soundClips[randomIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No sound clips available to play.");
        }
    }
}