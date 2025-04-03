using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskScript : MonoBehaviour
{
    //private static TaskScript instance;
    private PlayerUI playerUI;
    //private void Awake()
    //{
    //    instance = this;
    //}
    private GameObject[] endDevices;
    private GameObject endDevice1, endDevice2, middleDevice;
    bool isConnected = false;
    bool taskCompleted = false;

    private void Start()
    {
        playerUI=GetComponent<PlayerUI>();
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level_1")
            if(Level1Task())
            {
                TaskCompleted();
            }
    }

    private bool Level1Task()
    {
        if (endDevices == null)
        {
            endDevices = ChooseCableParents.instance.ChooseEndDevices("Switch", "Switch");
            endDevice1 = endDevices[0];
            endDevice2 = endDevices[1];
            playerUI.UpdateTaskText("Connect " + endDevice1.name + " with " + endDevice2.name);
        }

        isConnected=CheckCableParents.instance.CheckParents(endDevice1, endDevice2);

        if(isConnected)
        {
            taskCompleted = true;
            playerUI.UpdateTaskText("TASK COMPLETED");
            return true;
            
        }
        else if (!isConnected && taskCompleted)
        {
            taskCompleted = false;
            playerUI.UpdateTaskText("Connect " + endDevice1.name + " with "+ endDevice2.name);
            return false;
        }
        return false;
        
    }

    public bool TaskCompleted()
    {
        return true;
    }


}
