using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingsMenuUI;
    public GameObject[] mainMenuUI;

    public void settings()
    {
        settingsMenuUI.SetActive(true);
        foreach (GameObject menu in mainMenuUI)
        {
            menu.SetActive(false);
        }
    }
    public void back()
    {
        settingsMenuUI.SetActive(false);
        foreach (GameObject menu in mainMenuUI)
        {
            menu.SetActive(true);
        }
    }
    
}
