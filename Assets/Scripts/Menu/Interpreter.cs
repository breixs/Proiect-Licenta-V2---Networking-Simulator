using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    public TextMeshProUGUI deviceText;
    public TextMeshProUGUI sampleDeviceText;
    public GameObject laptop;
    private GameObject connectedDevice;
    private Switch deviceScript;

    List<string> response = new List<string>();

    private void OnEnable()
    {
        if (laptop.transform.childCount > 1)
        {
            connectedDevice = CheckCableParents.instance.getConsoleStartNodeParent();
            if (connectedDevice.tag=="ConsoleCableParent" || connectedDevice == null)
            {
                connectedDevice = CheckCableParents.instance.getConsoleEndNodeParent();
                if (connectedDevice.tag == "ConsoleCableParent" || connectedDevice == null)
                {
                    Debug.Log("no cable connected to second device");
                    CloseTerminal();
                }
            }
            deviceText.text =connectedDevice.transform.name + ">";
            sampleDeviceText.text= connectedDevice.transform.name + ">";

            if(connectedDevice.GetComponent<Switch>())
            {
                deviceScript = connectedDevice.GetComponent<Switch>();
            }
        }
        else
        {
            Debug.Log("no cable connected to laptop");
            CloseTerminal();
        }
    }

    public List<string> Interpret(string userInput)
    {
        response.Clear();

        string[] arguments = userInput.Split(" ");

        //if (arguments[0]=="help")
        //{
        //    response.Add("Commands available:");
        //    response.Add("enable");
        //    return response;
        //}
        //else
        //{
        //    response.Add("Unknown command. Type help for a list of commands");
        //    return response;
        //}

        switch(arguments[0])
        {
            case "help":
                response.Add("Commands available:");
                response.Add("enable");
                response.Add("ip");
                return response;
            case "ip":
                if(arguments.Length==4 && arguments[1]=="address" && arguments[2]!=null && arguments[3]!=null)
                {
                    response.Add("ip adress set : " + arguments[2] + " " + arguments[3]);
                    deviceScript.setIpAddress(arguments[2] + " " + arguments[3]);
                    return response;
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            default:
                response.Add("Unknown command. Type help for a list of commands");
                return response;
        }
    }

    private void CloseTerminal()
    {
        gameObject.SetActive(false);
        PlayerUI.inTerminal = false;
        Cursor.visible = false;
        Debug.Log(PlayerUI.inTerminal);
    }
}
