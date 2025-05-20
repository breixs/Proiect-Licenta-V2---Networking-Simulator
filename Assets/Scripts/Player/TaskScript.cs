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
    private GameObject[] endDevicesNet2;
    private GameObject endDevice1Net2, endDevice2Net2, middleDeviceNet2;
    public bool completed=false;
    int vlanId;
    int vlanIdNet2;
    string ipAdress;
    string ipAdressNet2;
    string subnetMask;
    string subnetMaskNet2;
    string ipAdressWithMask;
    string ipAdressWithMaskNet2;
    int prefix;
    int prefixNet2;

    private bool debugCompleted = false;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        playerUI =GetComponent<PlayerUI>();

        if (currentScene.name == "Level_2" || currentScene.name=="Level_3" || currentScene.name.Equals("Repeatable_level"))
        {
            vlanId = Random.Range(1, 9) * 10;
            Debug.Log(vlanId);
            prefix = Random.Range(16, 26);
            subnetMask = SubentMaskGenerator(prefix);
            Debug.Log("prefix1= " + prefix + " mask1= " + subnetMask);

            ipAdress = "192.168." + vlanId.ToString() + "." + Random.Range(2, 100).ToString();
            ipAdressWithMask = ipAdress + " " + subnetMask;
            Debug.Log("ipAdress 1: "+ipAdressWithMask);

            if(currentScene.name=="Level_3" || currentScene.name.Equals("Repeatable_level"))
            {
                vlanIdNet2 = Random.Range(1, 9) * 10;
                Debug.Log(vlanIdNet2);
                prefixNet2 = Random.Range(16, 26);
                subnetMaskNet2 = SubentMaskGenerator(prefixNet2);
                Debug.Log("prefix2= " + prefixNet2 + " mask2= " + subnetMaskNet2);

                ipAdressNet2 = "192.168." + vlanIdNet2.ToString() + "." + Random.Range(2, 100).ToString();
                ipAdressWithMaskNet2 = ipAdressNet2 + " " + subnetMaskNet2;
                Debug.Log("ipAdress 2: "+ipAdressWithMaskNet2);
            }
        }
    }

    private void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.Y))
        {
            debugCompleted = true;
        }

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
        if (currentScene.name == "Level_3" || currentScene.name.Equals("Repeatable_level"))
        {
            if (Level3Task() || debugCompleted)
            {
                completed = true;
            }
            else
            {
                completed = false;
            }
        }
        if(currentScene.name=="Sandbox")
        {
            completed = true;
        }
    }

    private bool Level1Task()
    {
        bool isConnected = false;

        if (endDevices == null)
        {
            endDevices = ChooseCableParents.instance.ChooseEndDevices("Switch", "Switch");
            endDevice1 = endDevices[0];
            endDevice2 = endDevices[1];
            playerUI.UpdateTaskText("Connect " + endDevice1.name + " with " + endDevice2.name + '\n' + '\n' + "Optional : Use another intermediary switch to connect the 2 devices");
        }

        isConnected=CheckCableParents.instance.CheckParents(endDevice1, endDevice2);

        if(isConnected)
        {
            playerUI.UpdateTaskText("TASK COMPLETED");
            return true;
            
        }
        else
        {
            playerUI.UpdateTaskText("Connect " + endDevice1.name + " with "+ endDevice2.name + '\n' + '\n' + "Optional : Use another intermediary switch to connect the 2 devices");
            return false;
        }
        
    }

    private bool Level2Task()
    {
        string taskText = null;
        bool isConnected = false;
        //bool taskCompleted = false;

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
        Debug.Log("TASK2 CONNECTED: "+ isConnected);

        if (isConnected)
        {
            //taskCompleted = true;
            playerUI.UpdateTaskText("TASK COMPLETED");
            return true;

        }
        else
        {
            //taskCompleted = false;
            taskText = "Connect " + endDevice1.name + " with " + endDevice2.name + " using " + middleDevice.name + '\n' + '\n' + middleDevice.name + " ip adress = " + ipAdress + "/" + prefix.ToString();
            playerUI.UpdateTaskText(taskText);
            return false;
        }
    }

    private bool Level3Task()
    {
        string taskText = null;
        bool isConnected1 = false;
        bool isConnected2 = false;
        bool isConnected3 = false;

        if (endDevices == null)
        {
            do
            {
                endDevices = ChooseCableParents.instance.ChooseEndDevices("PatchPanelPort", "Router");
                endDevice1 = endDevices[0];
                endDevice2 = endDevices[1];
                middleDevice = ChooseCableParents.instance.ChooseMiddleDevice("Switch");
            } while (endDevice1.name.Equals("SpawnRouter") || endDevice2.name.Equals("SpawnRouter") || middleDevice.name.Equals("SpawnSwitch"));

            do {
                endDevicesNet2 = ChooseCableParents.instance.ChooseEndDevices("PatchPanelPort", "Router");
                endDevice1Net2 = endDevicesNet2[0];
                endDevice2Net2 = endDevicesNet2[1];
                middleDeviceNet2 = ChooseCableParents.instance.ChooseMiddleDevice("Switch");
            }while(endDevice1.Equals(endDevice1Net2) || middleDevice.Equals(middleDeviceNet2)  || (endDevice1Net2.name.Equals("SpawnRouter") || endDevice2Net2.name.Equals("SpawnRouter") || 
            middleDeviceNet2.name.Equals("SpawnSwitch")));

            taskText = "Connect " + endDevice1.name + " with " + endDevice2.name + " using " + middleDevice.name + '\n' + '\n'
                + middleDevice.name + " ip adress = " + ipAdress + "/" + prefix.ToString() + '\n' + '\n'
                + "Connect " + endDevice1Net2.name + " with " + endDevice2Net2.name + " using " + middleDeviceNet2.name + '\n' + '\n'
                + middleDeviceNet2.name + " ip adress = " + ipAdressNet2 + "/" + prefixNet2.ToString();
            playerUI.UpdateTaskText(taskText);
        }

       
         isConnected1 = CheckCableParents.instance.CheckParents(endDevice1, endDevice2, middleDevice, vlanId, ipAdressWithMask);
         isConnected2 = CheckCableParents.instance.CheckParents(endDevice1Net2, endDevice2Net2, middleDeviceNet2, vlanIdNet2, ipAdressWithMaskNet2);
   
        if (endDevice2.Equals(endDevice2Net2))
            isConnected3 = true;
        else
        {
            isConnected3=CheckCableParents.instance.CheckParents(endDevice2, endDevice2Net2);
        }

        Debug.Log("TASK3 CONNECTED: " + isConnected1 + isConnected2 + isConnected3);

        if (isConnected1 && isConnected2 && isConnected3)
        {
            playerUI.UpdateTaskText("TASK COMPLETED");
            return true;

        }
        else
        {
            taskText = "Connect " + endDevice1.name + " with " + endDevice2.name + " using " + middleDevice.name + '\n' + '\n'
                + middleDevice.name + " ip adress = " + ipAdress + "/" + prefix.ToString() + '\n' + '\n'
                + "Connect " + endDevice1Net2.name + " with " + endDevice2Net2.name + " using " + middleDeviceNet2.name + '\n' +'\n'
                + middleDeviceNet2.name + " ip adress = " + ipAdressNet2 + "/" + prefixNet2.ToString();
            playerUI.UpdateTaskText(taskText);
            return false;
        }
       
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
