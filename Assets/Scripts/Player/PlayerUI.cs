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
    public TextMeshProUGUI alertText;

    public GameObject pauseMenu;
    public GameObject notebookMenu;
    public GameObject endMenu;
    public GameObject gameOverMenu;
    public GameObject terminal;
    private InputManager inputManager;
    public static bool paused = false;
    public static bool notebookState = false;
    public static bool inTerminal = false;
    public static bool gameOver = false;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        pauseMenu.SetActive(false);
        notebookMenu.SetActive(false);
        endMenu.SetActive(false);
        promptText.gameObject.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (inputManager.onFoot.Pause.triggered && !notebookState && !inTerminal && !gameOver)
        {
            if (!paused)
            {
                Debug.Log("Pause");
                Cursor.visible = true;
                PauseGame();
            }
            else
            {
                Debug.Log("UnPause");
                ResumeGame();
            }
        }
        if (inputManager.onFoot.Notebook.triggered && !paused && (!endMenu.activeSelf || !gameOverMenu.activeSelf))
        {
            notebookState = !notebookState;
            OpenNotebook(notebookState);
        }
        if(inputManager.onFoot.Pause.triggered && inTerminal)
        {
            terminal.SetActive(false);
            inTerminal = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
        if (!endMenu.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }
    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
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
        if (state)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(!state && inTerminal)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        notebookMenu.SetActive(state);
    }

    public void UpdateTaskText(string txt)
    {
        taskText.text = txt;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        gameOver = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
