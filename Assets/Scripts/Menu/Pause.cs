using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private InputManager inputManager;
    private static bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.onFoot.Pause.triggered)
        {
            if (!paused)
            {
                Debug.Log("Pause");
                PauseGame();
            }
            else
            {
                Debug.Log("UnPause");
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

}
