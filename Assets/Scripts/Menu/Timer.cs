using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerTxt;
    private float elapsedTime;
    private string sceneName;
    private PlayerUI playerUI;
    private bool isGameOver = false;
    private int timeLimit=7;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneName=scene.name;
        playerUI=GetComponent<PlayerUI>();
        if (CurrentScoreScript.currentScore >= 3 && CurrentScoreScript.currentScore<5)
        {
            timeLimit = 6;
        }
        else if (CurrentScoreScript.currentScore >= 5 && CurrentScoreScript.currentScore < 7)
        {
            timeLimit = 5;
        }
        else if (CurrentScoreScript.currentScore >= 7)
        {
            timeLimit = 4;
        }
        else
        {
            timeLimit = 7;
        }

    }
    void Update()
    {
        elapsedTime+=Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        Debug.Log("minutes: "+minutes);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (!isGameOver && sceneName.Equals("Repeatable_level") && minutes>=timeLimit)
        {
            isGameOver=true;
            playerUI.GameOver();
        }
        
    }
}
