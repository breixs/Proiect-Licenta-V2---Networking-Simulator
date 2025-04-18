using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class Switch : Interactable
{
    public TextMeshProUGUI nameText;
    private Dictionary<int, string> vlanIp;
    void Start()
    {
        nameText.text=gameObject.name;
        vlanIp = new Dictionary<int, string>();
        AddVlan(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
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
        return vlanIp[vlanId].Equals(ip);
    }
}
