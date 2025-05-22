using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : Interactable
{
    public GameObject endMenu;
    private string sceneName;
    private UpdateScoreText updateScoreText;
    private bool firstInteract = true;
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        updateScoreText = GetComponent<UpdateScoreText>();
    }
    protected override void CondInteract()
    {
        endMenu.SetActive(true);
        if(sceneName.Equals("Level_1") && firstInteract)
        {
            PlayerPrefs.SetInt("lv1", 1);
            firstInteract = false;
        }
        if (sceneName.Equals("Level_2") && firstInteract)
        {
            PlayerPrefs.SetInt("lv2", 1);
            firstInteract = false;
        }
        if (sceneName.Equals("Level_3") && firstInteract)
        {
            PlayerPrefs.SetInt("lv3", 1);
            firstInteract = false;
        }
        if (sceneName.Equals("Repeatable_level") && firstInteract)
        {
            CurrentScoreScript.currentScore += 1;
            updateScoreText.UpdateText("Current Score: " + CurrentScoreScript.currentScore);

            if(CurrentScoreScript.currentScore>PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", CurrentScoreScript.currentScore);
            }

            updateScoreText.UpdateHighScoreText("High Score : " + PlayerPrefs.GetInt("Highscore"));

            firstInteract = false;
        }

        Time.timeScale = 0f;
    }
    protected override void Interact()
    {
        Debug.Log("cannot leave yet");
    }
}
