using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerTxt;
    private float elapsedTime;
    void Start()
    {
        
    }

    void Update()
    {
        elapsedTime+=Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
