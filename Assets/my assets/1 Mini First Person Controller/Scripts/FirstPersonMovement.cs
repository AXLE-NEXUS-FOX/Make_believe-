using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    [Header("Collectables")]
    public TextMeshProUGUI coinsText;
    private int Coins = 0;
    public GameObject SettingsMenu;
    private bool isSettingsMenuOpen = false;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        coinsText.text = "Coins: " + Coins.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            Coins++;
            coinsText.text = "Coins: " + Coins.ToString();
            // Play Sound effect if you want
            Destroy(other.gameObject);
        }
    }


    void OpenSettingMenu(bool SettingsMenueBool)
    {
        SettingsMenueBool = !SettingsMenueBool;
        isSettingsMenuOpen = SettingsMenueBool;
        Debug.Log("Settings Menu Opened: " + SettingsMenueBool);
        if (SettingsMenueBool)
        {
        SettingsMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        }
        else
        {
            SettingsMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenSettingMenu(isSettingsMenuOpen);
        }
    }
}