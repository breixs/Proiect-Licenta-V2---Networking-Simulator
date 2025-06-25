using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotebookUI : MonoBehaviour
{
    public GameObject MainBackground;
    public GameObject TasksBackground;
    public GameObject ToolsBackground;
    public GameObject LearnBackground;
    public GameObject switchBackground;
    public GameObject routerBackground;
    public GameObject commandBackground;
    public TextMeshProUGUI taskText;

    public void MainEnable()
    {
        MainBackground.SetActive(true);
        TasksBackground.SetActive(false);
        ToolsBackground.SetActive(false);
        LearnBackground.SetActive(false);
    }
    public void TasksEnable()
    {
        TasksBackground.SetActive(true);
        MainBackground.SetActive(false);
        ToolsBackground.SetActive(false);
        LearnBackground.SetActive(false);
    }
    public void ToolsEnable()
    {
        ToolsBackground.SetActive(true);
        TasksBackground.SetActive(false);
        MainBackground.SetActive(false);
        LearnBackground.SetActive(false);
    }
    public void LearnEnable()
    {
        LearnBackground.SetActive(true);
        ToolsBackground.SetActive(false);
        TasksBackground.SetActive(false);
        MainBackground.SetActive(false);
    }
    public void UpdateTaskNotebook(string txt)
    {
        taskText.text = txt;
    }

    public void SwitchesEnable()
    {
        switchBackground.SetActive(true);
        routerBackground.SetActive(false);
        commandBackground.SetActive(false);
    }
    public void RoutersEnable()
    {
        switchBackground.SetActive(false);
        routerBackground.SetActive(true);
        commandBackground.SetActive(false);
    }
    public void CommandsEnable()
    {
        switchBackground.SetActive(false);
        routerBackground.SetActive(false);
        commandBackground.SetActive(true);
    }

}
