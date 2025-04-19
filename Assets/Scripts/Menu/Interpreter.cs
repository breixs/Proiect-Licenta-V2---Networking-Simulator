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

    private bool terminalEnabled = false;
    private bool configureTerminal = false;
    private bool inInterface = false;

    private int currentVlan;

    private bool firstEnbale = true;

    List<string> response = new List<string>();

    private void OnEnable()
    {
        TerminalManager terminalManager = GetComponent<TerminalManager>();
        if (laptop.transform.childCount > 1)
        {
            GameObject newConnectedDevice = CheckCableParents.instance.getConsoleStartNodeParent();
            GameObject alternativeDevice = CheckCableParents.instance.getConsoleEndNodeParent();

            // Check if either connection is valid
            if (newConnectedDevice != null && (newConnectedDevice.tag =="Switch"))
            {
                // Valid connection via start node
            }
            else if (alternativeDevice != null && (alternativeDevice.tag == "Switch"))
            {
                // Valid connection via end node
                newConnectedDevice = alternativeDevice;
            }
            else
            {
                Debug.Log("No valid cable connection to a device");
                CloseTerminal();
                return;
            }

            if (connectedDevice!=null && newConnectedDevice!=connectedDevice)
            {
                terminalManager.ClearTerminal();
                terminalEnabled = false;
                configureTerminal = false;
                inInterface = false;
                deviceText.text = connectedDevice.transform.name + ">";
                deviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200f);
                sampleDeviceText.text = connectedDevice.transform.name + ">";
                sampleDeviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200f);
            }

            connectedDevice = newConnectedDevice;

            if (firstEnbale)
            {
                deviceText.text = connectedDevice.transform.name + ">";
                deviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200f);
                sampleDeviceText.text = connectedDevice.transform.name + ">";
                sampleDeviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200f);
                firstEnbale = false;
            }

         

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
            case "?":
                if (!terminalEnabled)
                {
                    response.Add("Commands available:");
                    response.Add("enable");
                    return response;
                }
                else if(terminalEnabled && !configureTerminal)
                {
                    response.Add("Commands available:");
                    response.Add("configure terminal");
                    response.Add("show vlan");
                    response.Add("exit");
                    return response;
                }
                else if(terminalEnabled && configureTerminal)
                {
                    response.Add("Commands available:");
                    response.Add("hostname 'name'");
                    response.Add("vlan 'n'");
                    response.Add("interface vlan 'n'");
                    response.Add("exit");
                    return response;
                }
                else if(terminalEnabled && configureTerminal && inInterface)
                {
                    response.Add("Commands available:");
                    response.Add("ip address 'x.x.x.x m.m.m.m'");
                    response.Add("exit");
                    return response;
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            case "enable":
            case "ena":
            case "en":
                response.Add("enabled");
                terminalEnabled = true;

                deviceText.text = connectedDevice.transform.name + "#";
                sampleDeviceText.text = connectedDevice.transform.name + "#";

                return response;
            case "configure":
            case "conf":
                if (arguments.Length == 2 && (arguments[1] == "t" || arguments[1]=="terminal"))
                {
                    if (terminalEnabled)
                    {
                        response.Add("configure terminal");
                        configureTerminal = true;

                        deviceText.text = connectedDevice.transform.name + "(conf-t)";
                        deviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300f);
                        sampleDeviceText.text = connectedDevice.transform.name + "(conf-t)";
                        sampleDeviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300f);

                        return response;
                    }
                    else
                    {
                        response.Add("Invalid Command");
                        return response;
                    }
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            case "ip":
                if(arguments.Length==4 && arguments[1]=="address" && arguments[2]!=null && arguments[3]!=null && currentVlan>0 && currentVlan<100)
                { 
                    deviceScript.SetIpAddress(currentVlan, arguments[2] + " " + arguments[3]);
                    response.Add("IP Adress Set: "+arguments[2] + " " + arguments[3]);
                    return response;
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            case "vlan":
                if (terminalEnabled && configureTerminal && arguments.Length==2 && arguments[1]!=null)
                {
                    bool canConvert=int.TryParse(arguments[1], out int vlanValue);
                    if(canConvert)
                    {
                        deviceScript.AddVlan(vlanValue);
                        response.Add("Vlan added : "+ vlanValue);
                        return response;
                    }
                    else
                    {
                        response.Add("Vlan value must be a number");
                        return response;
                    }
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            case "interface":
            case "int":
                if (terminalEnabled && configureTerminal && arguments.Length == 3 && arguments[1] == "vlan" && arguments[2] != null)
                {
                    bool canConvert = int.TryParse(arguments[2], out int vlanValue);
                    currentVlan=vlanValue;
                    if (canConvert && deviceScript.ContainsVlan(currentVlan))
                    {
                        inInterface = true;
                        response.Add("Entered Vlan Interface");

                        deviceText.text = connectedDevice.transform.name + "(conf-t-int)";
                        deviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400f);
                        sampleDeviceText.text = connectedDevice.transform.name + "(conf-t-int)";
                        sampleDeviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400f);

                        return response;
                    }
                    else
                    {
                        response.Add("Vlan does not exist");
                        return response;
                    }
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            case "hostname":
                if(terminalEnabled && configureTerminal && arguments.Length==2 && arguments[1]!=string.Empty)
                {
                    connectedDevice.name = arguments[1];
                    response.Add("Changed hostname");
                    return response;
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            case "sho":
            case "show":
                if(terminalEnabled && !configureTerminal && arguments.Length==2 && arguments[1]=="vlan")
                {
                    string[] responses = deviceScript.GetDeviceInfo().Split('\n');
                    foreach (var resp in responses)
                    {
                        if (!string.IsNullOrEmpty(resp))
                            response.Add(resp);
                    }
                   
                    return response;
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            case "do":
                if (terminalEnabled && configureTerminal && arguments.Length == 3 && (arguments[1] == "show" || arguments[1]=="sho") && arguments[2]=="vlan")
                {
                    string[] responses = deviceScript.GetDeviceInfo().Split('\n');
                    foreach (var resp in responses)
                    {
                        if (!string.IsNullOrEmpty(resp))
                            response.Add(resp);
                    }

                    return response;
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            case "exit":
                if (terminalEnabled && configureTerminal && inInterface)
                {
                    deviceText.text = connectedDevice.transform.name + "(conf-t)";
                    deviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300f);
                    sampleDeviceText.text = connectedDevice.transform.name + "(conf-t)";
                    sampleDeviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300f);

                    inInterface = false;
                    return response;
                }
                else if (terminalEnabled && configureTerminal && !inInterface)
                {
                    deviceText.text = connectedDevice.transform.name + "#";
                    deviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200f);
                    sampleDeviceText.text = connectedDevice.transform.name + "#";
                    sampleDeviceText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200f);

                    configureTerminal = false;
                    return response;
                }
                else if(terminalEnabled && !configureTerminal && !inInterface)
                {
                    deviceText.text = connectedDevice.transform.name + ">";
                    sampleDeviceText.text = connectedDevice.transform.name + ">";

                    terminalEnabled = false;
                    return response;
                }
                else
                {
                    response.Add("Invalid Command");
                    return response;
                }
            default:
                response.Add("Unknown command. Type '?' for a list of commands");
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
