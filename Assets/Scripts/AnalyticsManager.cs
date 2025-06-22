using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine.SocialPlatforms.Impl;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;
    private bool isInitialised = false;

    private void Awake()
    {
        if(Instance!=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        isInitialised = true;
    }

    public void CompletedLevel(string levelName)
    {
        if (!isInitialised)
        {
            return;
        }
        CustomEvent completedLevelEvent = new CustomEvent("completed_level")
        {
            {"completed_level", levelName }
        };
        AnalyticsService.Instance.RecordEvent(completedLevelEvent);
        AnalyticsService.Instance.Flush();
        Debug.Log("Completed Level = " + levelName);
    }

    public void HighestLevel(string levelName)
    {
        if(!isInitialised)
        {
            return;
        }
        CustomEvent levelEvent = new CustomEvent("highest_level")
        {
            {"highest_level", levelName }
        };
        AnalyticsService.Instance.RecordEvent(levelEvent);
        AnalyticsService.Instance.Flush();
        Debug.Log("Highest Level = " + levelName);
    }

    public void CurrentScore(int score)
    {
        if (!isInitialised)
        {
            return;
        }
        CustomEvent scoreEvent = new CustomEvent("current_score")
        {
            {"current_score", score }
        };
        AnalyticsService.Instance.RecordEvent(scoreEvent);
        AnalyticsService.Instance.Flush();
        Debug.Log("Current Score = " + score);
    }

    public void HighestScore(int score)
    {
        if (!isInitialised)
        {
            return;
        }
        CustomEvent highScoreEvent = new CustomEvent("highest_score")
        {
            {"highest_score", score }
        };
        AnalyticsService.Instance.RecordEvent(highScoreEvent);
        AnalyticsService.Instance.Flush();
        Debug.Log("Highest Score = " + score);
    }

    public void TimeForCompletion(string level_name, string time)
    {
        if (!isInitialised)
        {
            return;
        }
        CustomEvent timeEvent = new CustomEvent("time_event")
        {
            {"level_name", level_name },
            { "time", time }
        };
        AnalyticsService.Instance.RecordEvent(timeEvent);
        AnalyticsService.Instance.Flush();
        Debug.Log("Time = " + time);
    }
}
