using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject levelPanel;

    private void Start()
    {
        levelPanel.SetActive(false);
    }

    public void ActivateLevelSelect()
    {
        levelPanel.SetActive(true);
    }
    public void DeactivateLevelSelect()
    {
        levelPanel.SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
