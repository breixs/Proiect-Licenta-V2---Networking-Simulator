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
    public bool completed=false;

    private void Start()
    {
        playerUI=GetComponent<PlayerUI>();
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level_1")
        {
            if (Level1Task())
            {
                completed = true;
            }
            else
            {
                completed = false;
            }
        }
        if (currentScene.name == "Level_2")
        {
            if (Level2Task())
            {
                completed = true;
            }
            else
            {
                completed = false;
            }
        }
    }

    private bool Level1Task()
    {
        //bool task1Completed = false;
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

    private bool Level2Task()
    {
        if (endDevices == null)
        {
            endDevices = ChooseCableParents.instance.ChooseEndDevices("PatchPanelPort", "Router");
            endDevice1 = endDevices[0];
            endDevice2 = endDevices[1];
            middleDevice = ChooseCableParents.instance.ChooseMiddleDevice("Switch");
            playerUI.UpdateTaskText("Connect " + endDevice1.name + " with " + endDevice2.name +" using " + middleDevice.name);
        }
        isConnected = CheckCableParents.instance.CheckParents(endDevice1, endDevice2, middleDevice);

        if (isConnected)
        {
            taskCompleted = true;
            playerUI.UpdateTaskText("TASK COMPLETED");
            return true;

        }
        else if (!isConnected && taskCompleted)
        {
            taskCompleted = false;
            playerUI.UpdateTaskText("Connect " + endDevice1.name + " with " + endDevice2.name + " using " + middleDevice.name);
            return false;
        }
        return false;
    }
}
