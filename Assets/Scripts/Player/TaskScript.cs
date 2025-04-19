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
    int vlanId;
    string ipAdress;
    string subnetMask;
    string ipAdressWithMask;
    int prefix;

    private void Start()
    {
        playerUI=GetComponent<PlayerUI>();
        vlanId = Random.Range(1, 9) * 10;
        Debug.Log(vlanId);
        prefix=Random.Range(16, 26);
        subnetMask= SubentMaskGenerator(prefix);
        Debug.Log("prefix= " + prefix + " mask= "+subnetMask);

        ipAdress ="192.168." + vlanId.ToString() + "."+ Random.Range(2,100).ToString();
        ipAdressWithMask = ipAdress + " " + subnetMask;
        Debug.Log(ipAdress);
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
        string taskText = null;

        if (endDevices == null)
        {
            endDevices = ChooseCableParents.instance.ChooseEndDevices("PatchPanelPort", "Router");
            endDevice1 = endDevices[0];
            endDevice2 = endDevices[1];
            middleDevice = ChooseCableParents.instance.ChooseMiddleDevice("Switch");
            taskText = "Connect " + endDevice1.name + " with " + endDevice2.name + " using " + middleDevice.name + '\n' + '\n' + middleDevice.name + " ip adress = " + ipAdress + "/" + prefix.ToString();
            playerUI.UpdateTaskText(taskText);
        }
       
        isConnected = CheckCableParents.instance.CheckParents(endDevice1, endDevice2, middleDevice, vlanId, ipAdressWithMask);

        if (isConnected)
        {
            taskCompleted = true;
            playerUI.UpdateTaskText("TASK COMPLETED");
            return true;

        }
        else if (!isConnected && taskCompleted)
        {
            taskCompleted = false;
            playerUI.UpdateTaskText(taskText);
            return false;
        }
        return false;
    }

    private string SubentMaskGenerator(int prefix)
    {
        int value = 0;
        int added = 128;
        int counter = 0;
        string mask=null;

        switch(prefix)
        {
            case 16:
                mask = "255.255." + value.ToString() + ".0";
                break;
            case int i when i>16 && i<=23:
                counter = 16;
                while(counter<i)
                {
                    value += added;
                    added /= 2;
                    counter++;
                }
                mask = "255.255." + value.ToString() + ".0";
                break;
            case 24:
                mask = "255.255.255." + value.ToString();
                break;
            case int i when i > 24 && i <= 32:
                counter = 24;
                while (counter < i)
                {
                    value += added;
                    added /= 2;
                    counter++;
                }
                mask = "255.255.255." + value.ToString();
                break;
            default:
                Debug.Log("bad prefix");
                break;
        }

     
        return mask;
    }
}
