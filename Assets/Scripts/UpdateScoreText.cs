using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
   public void UpdateText(string txt)
    {
        scoreText.text = txt;
    }
    public void UpdateHighScoreText(string text)
    {
        highScoreText.text = text; 
    }
}
