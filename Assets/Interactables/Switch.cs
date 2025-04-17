using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Switch : Interactable
{
    public TextMeshProUGUI nameText;
    private string ipAddress;
    void Start()
    {
        nameText.text=gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        Debug.Log("interacted with " + gameObject.name + "ip address: " + ipAddress);
        //PickUpScript pickUpScript = gameObject.GetComponent<PickUpScript>();
        
        //pickUpScript.PickUpObject(gameObject);

        
    }
    public void setIpAddress(string ip)
    {
        ipAddress = ip;
    }
}
