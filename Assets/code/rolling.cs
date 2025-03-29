using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{
    // Public variable to control the rolling speed
    public float rollSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its Z-axis
        // Time.deltaTime ensures the rotation is frame-rate independent
        transform.Rotate(Vector3.forward * rollSpeed * Time.deltaTime);
    }
}
