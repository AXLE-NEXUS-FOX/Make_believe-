using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SynchronizedObjectToggler : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToToggle = new List<GameObject>();
    [SerializeField] private float minInterval = 1f;
    [SerializeField] private float maxInterval = 3f;

    private void Start()
    {
        if (objectsToToggle.Count == 0)
        {
            Debug.LogError("No objects assigned to toggle!");
            enabled = false;
            return;
        }

        StartCoroutine(ToggleObjects());
    }

    private IEnumerator ToggleObjects()
    {
        while (true)
        {
            float randomInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(randomInterval);

            // Toggle ALL objects in the list
            bool newState = !objectsToToggle[0].activeSelf; // Get the opposite of the current state of the first object
            foreach (GameObject obj in objectsToToggle)
            {
                obj.SetActive(newState);
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}