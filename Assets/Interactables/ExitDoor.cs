using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : Interactable
{
    public GameObject endMenu;
    public GameObject player;
    private string sceneName;
    private UpdateScoreText updateScoreText;
    private Timer playerTimer;
    private bool firstInteract = true;
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        updateScoreText = GetComponent<UpdateScoreText>();
        playerTimer = player.GetComponent<Timer>();
        
    }
    protected override void CondInteract()
    {
        endMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (sceneName.Equals("Level_1") && firstInteract)
        {
            PlayerPrefs.SetInt("lv1", 1);
            AnalyticsManager.Instance.CompletedLevel(sceneName);
            getHighestLevel();
            firstInteract = false;
        }
        if (sceneName.Equals("Level_2") && firstInteract)
        {
            PlayerPrefs.SetInt("lv2", 1);
            AnalyticsManager.Instance.CompletedLevel(sceneName);
            getHighestLevel();
            firstInteract = false;
        }
        if (sceneName.Equals("Level_3") && firstInteract)
        {
            PlayerPrefs.SetInt("lv3", 1);
            AnalyticsManager.Instance.CompletedLevel(sceneName);
            getHighestLevel();
            firstInteract = false;
        }
        if (sceneName.Equals("Repeatable_level") && firstInteract)
        {
            AnalyticsManager.Instance.CompletedLevel(sceneName);
            CurrentScoreScript.currentScore += 1;
            AnalyticsManager.Instance.CurrentScore(CurrentScoreScript.currentScore);
            updateScoreText.UpdateText("Current Score: " + CurrentScoreScript.currentScore);

            AnalyticsManager.Instance.TimeForCompletion(sceneName, playerTimer.getElapsedTime());

            if(CurrentScoreScript.currentScore>PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", CurrentScoreScript.currentScore);
                AnalyticsManager.Instance.HighestScore(CurrentScoreScript.currentScore);
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

    private void getHighestLevel()
    {
        if(PlayerPrefs.GetInt("lv1")==1 && PlayerPrefs.GetInt("lv2") != 1 && PlayerPrefs.GetInt("lv3") != 1)
            AnalyticsManager.Instance.HighestLevel("Level_1");
        else if(PlayerPrefs.GetInt("lv1") == 1 && PlayerPrefs.GetInt("lv2") == 1 && PlayerPrefs.GetInt("lv3") != 1)
            AnalyticsManager.Instance.HighestLevel("Level_2");
        else if(PlayerPrefs.GetInt("lv1") == 1 && PlayerPrefs.GetInt("lv2") == 1 && PlayerPrefs.GetInt("lv3") == 1)
            AnalyticsManager.Instance.HighestLevel("Level_3");
        else
            AnalyticsManager.Instance.HighestLevel("Bad Data");
    }
}
