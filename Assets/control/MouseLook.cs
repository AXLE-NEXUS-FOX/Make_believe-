using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float controllerSensitivity = 5f; // Sensitivity for controller input
    public Transform playerBody;
    public float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get input from both mouse and controller
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float controllerX = Input.GetAxis("Right Stick Horizontal") * controllerSensitivity * Time.deltaTime;
        float controllerY = Input.GetAxis("Right Stick Vertical") * controllerSensitivity * Time.deltaTime;

        // Combine inputs (prioritize controller if both are present)
        float totalX = controllerX == 0 ? mouseX : controllerX;
        float totalY = controllerY == 0 ? mouseY : controllerY;
        
        //Invert Y for controller
        totalY *= -1;

        xRotation -= totalY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * totalX);
    }
}
