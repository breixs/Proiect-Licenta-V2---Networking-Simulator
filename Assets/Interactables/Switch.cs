using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TMPro;
using UnityEngine;

public class Switch : Interactable
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI deviceInfo;
    public GameObject infoPanel;

    private Dictionary<int, string> vlanIp;

    private bool panelActive = false;
    void Start()
    {
        nameText.text=gameObject.name;
        vlanIp = new Dictionary<int, string>();
        AddVlan(1);
        infoPanel.SetActive(false);
    }

    protected override void Interact()
    {
        if (!panelActive)
        {
            deviceInfo.text = GetDeviceInfo();
            infoPanel.SetActive(true);
            panelActive = true;
        }
        else
        {
            infoPanel.SetActive(false);
            panelActive = false;
        }

        Debug.Log("interacted with " + gameObject.name );
        foreach (var entry in vlanIp)
        {
            Debug.Log($"VLAN {entry.Key}: {entry.Value}");
        }
    }
    public void SetIpAddress(int vlanId, string ipAddress)
    {
        if(vlanIp.ContainsKey(vlanId))
        {
            vlanIp[vlanId] = ipAddress; 
        }
    }
    public void AddVlan(int vlanId)
    {
        if(!vlanIp.ContainsKey(vlanId))
        {
            vlanIp.Add(vlanId, null);
        }
    }
    public bool ContainsVlan(int vlanId)
    {
        return vlanIp.ContainsKey(vlanId);
    }

    public bool ContainsIp(int vlanId, string ip)
    {
        Debug.Log("current ip for vlan "+ vlanId + ": "+vlanIp[vlanId]);
        if (vlanIp[vlanId] != null)
        {
            return vlanIp[vlanId].Equals(ip);
        }
       else
           return false;
    }

    public string GetIpAdress(int vlanId)
    {
        if (vlanIp[vlanId] != null)
        {
            return vlanIp[vlanId];
        }
        else
        {
            return null;
        }
    }

    public string GetDeviceInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(gameObject.name + " VLAN - IP Adress: ");
        foreach (var entry in vlanIp)
        {
            sb.AppendLine($"VLAN {entry.Key}: {entry.Value}");
        }
        return sb.ToString();
    }
}
