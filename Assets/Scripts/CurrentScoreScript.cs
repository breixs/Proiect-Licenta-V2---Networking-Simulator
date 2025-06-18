using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScoreScript : MonoBehaviour
{
    public static int currentScore=0;

    public void ResetScore()
    {
        currentScore=0;
    }
}
