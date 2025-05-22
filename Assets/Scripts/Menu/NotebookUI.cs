using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotebookUI : MonoBehaviour
{
    public GameObject MainBackground;
    public GameObject TasksBackground;
    public GameObject ToolsBackground;
    public TextMeshProUGUI taskText;

    public void MainEnable()
    {
        MainBackground.SetActive(true);
        TasksBackground.SetActive(false);
        ToolsBackground.SetActive(false);
    }
    public void TasksEnable()
    {
        TasksBackground.SetActive(true);
        MainBackground.SetActive(false);
        ToolsBackground.SetActive(false);
    }
    public void ToolsEnable()
    {
        ToolsBackground.SetActive(true);
        TasksBackground.SetActive(false);
        MainBackground.SetActive(false);
    }
    public void UpdateTaskNotebook(string txt)
    {
        taskText.text = txt;
    }

}
