using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnButton : Interactable
{
    public TextMeshProUGUI devicesRemainingText;

    public GameObject spawnPoint;
    public GameObject switchDevice;
    public GameObject routerDevice;
    private int switchDeviceNumber=0;
    private int routerDeviceNumber=0;

    public static int switchesRemaining = 10;
    public static int routersRemaining = 8;

    private void Start()
    {
        switchesRemaining = 10;
        routersRemaining = 8;
    }
    protected override void Interact()
    {
        if (gameObject.CompareTag("SpawnSwitch") && switchDeviceNumber<10)
        {
            var newSwitch = Instantiate(switchDevice, spawnPoint.transform.position, spawnPoint.transform.rotation);
            newSwitch.name = "Switch " + switchDeviceNumber;
            switchDeviceNumber++;
            switchesRemaining--;
            UpdateDevicesText("Switches : " + switchesRemaining + " Routers : " + routersRemaining);
        }
        else if(gameObject.CompareTag("SpawnRouter") && routerDeviceNumber<8)
        {
            var newRouter = Instantiate(routerDevice, spawnPoint.transform.position, spawnPoint.transform.rotation);
            newRouter.name = "Router " + routerDeviceNumber;
            routerDeviceNumber++;
            routersRemaining--;
            UpdateDevicesText("Switches : " + switchesRemaining + " Routers : " + routersRemaining);
        }
        else
        {
            Debug.Log("Could not spawn");
        }
    }
    private void UpdateDevicesText(string txt)
    {
        devicesRemainingText.text = txt;
    }
}
