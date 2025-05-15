using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonLook : MonoBehaviour
{
    [Header("First Person Look")]

    [SerializeField]
    Transform character;
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityText;

    [Range(0, 10)]
    public float sensitivity = 0.5f;
    public float smoothing = 1.5f;
    public float multiply = 1.5f;
    
    Vector2 velocity;
    Vector2 frameVelocity;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        sensitivitySlider.maxValue = 10;
        sensitivitySlider.minValue = 0.1f;
        sensitivitySlider.value = sensitivity;
        sensitivityText.text = "Sensitivity: " + sensitivity.ToString();
    }
    private void OnGUI()
    {
        sensitivitySlider.value = sensitivity;
    }

    void Update()
    {
        FirstPersonLookCamController();

    }
    void FirstPersonLookCamController()
    {   // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);
        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    public void UpdateSensitivity()
    {
       sensitivity = sensitivitySlider.value;
       sensitivityText.text = "Sensitivity: " + sensitivity.ToString("0.0");
    }
}
