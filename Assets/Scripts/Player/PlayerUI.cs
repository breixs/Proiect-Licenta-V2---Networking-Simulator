using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI taskText;
    public GameObject pauseMenu;
    public GameObject notebookMenu;
    private InputManager inputManager;
    public static bool paused = false;
    public static bool notebookState = false;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        pauseMenu.SetActive(false);
        notebookMenu.SetActive(false);
        promptText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (inputManager.onFoot.Pause.triggered && !notebookState)
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
        if (inputManager.onFoot.Notebook.triggered && !paused)
        {
            notebookState = !notebookState;
            OpenNotebook(notebookState);
        }
    }
    public void UpdateText(string promptMessage)
    {
        promptText.gameObject.SetActive(true);
        promptText.text = promptMessage;
    }
    public void DeactivateText()
    {
        promptText.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
    public void QuitToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("Menu");
    }

    public void OpenNotebook(bool state)
    {
        notebookMenu.SetActive(state);
    }
}
