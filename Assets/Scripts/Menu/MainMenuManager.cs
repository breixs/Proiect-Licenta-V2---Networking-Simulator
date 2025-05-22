using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject levelPanel;
    public GameObject alertPanel;

    private void Start()
    {
        levelPanel.SetActive(false);
        alertPanel.SetActive(false);
    }

    public void ActivateLevelSelect()
    {
        levelPanel.SetActive(true);
    }
    public void DeactivateLevelSelect()
    {
        levelPanel.SetActive(false);
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
            alertPanel.SetActive(true);
        }
    }
    public void LoadLv3()
    {
        if (PlayerPrefs.GetInt("lv2") == 1)
            SceneManager.LoadScene("Level_3");
        else
        {
            alertPanel.SetActive(true);
        }
    }
    public void LoadRepLv()
    {
        if (PlayerPrefs.GetInt("lv3") == 1)
            SceneManager.LoadScene("Repeatable_level");
        else
        {
            alertPanel.SetActive(true);
        }
    }

    public void DeactivateALertPanel()
    {
        alertPanel.SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
