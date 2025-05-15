using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeSettingValue : MonoBehaviour
{
   private FirstPersonLook firstPersonLookFromSettings;
   private FirstPersonLook firstPersonLookFromCam;
   public Slider SensisitvitySlider;


    private void Start()
    {

        firstPersonLookFromSettings = GameObject.Find("fps").GetComponent<FirstPersonLook>();
        firstPersonLookFromCam = gameObject.GetComponent<FirstPersonLook>();
        firstPersonLookFromCam.sensitivity = firstPersonLookFromSettings.sensitivity;
        firstPersonLookFromCam.sensitivitySlider = SensisitvitySlider;
    }
}
