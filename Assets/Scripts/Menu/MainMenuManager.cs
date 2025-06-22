using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject levelPanel;
    public GameObject alertPanel;
    public GameObject optionsPanel;
    public Slider volumeSlider;

    private void Start()
    {
        levelPanel.SetActive(false);
        alertPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if(!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void ActivateLevelSelect()
    {
        Cursor.visible = true;
        levelPanel.SetActive(true);
    }
    public void DeactivateLevelSelect()
    {
        Cursor.visible = true;
        levelPanel.SetActive(false);
    }

    public void ActivateOptions()
    {
        Cursor.visible = true;
        optionsPanel.SetActive(true);
    }

    public void DeactivateOptions()
    {
        Cursor.visible = true;
        optionsPanel.SetActive(false);
    }

    public void LoadLv1()
    { 
        SceneManager.LoadScene("Level_1"); 
    }
    public void LoadLv2()
    {
        if (PlayerPrefs.GetInt("lv1") == 1)
            SceneManager.LoadScene("Level_2");
        else
        {
            Cursor.visible = true;
            alertPanel.SetActive(true);
        }
    }
    public void LoadLv3()
    {
        if (PlayerPrefs.GetInt("lv2") == 1)
            SceneManager.LoadScene("Level_3");
        else
        {
            Cursor.visible = true;
            alertPanel.SetActive(true);
        }
    }
    public void LoadRepLv()
    {
        if (PlayerPrefs.GetInt("lv3") == 1)
            SceneManager.LoadScene("Repeatable_level");
        else
        {
            Cursor.visible = true;
            alertPanel.SetActive(true);
        }
    }

    public void DeactivateALertPanel()
    {
        Cursor.visible = true;
        alertPanel.SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }
}
