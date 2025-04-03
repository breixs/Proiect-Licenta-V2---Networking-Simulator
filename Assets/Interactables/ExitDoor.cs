using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitDoor : Interactable
{
    public TextMeshProUGUI nameText;
    void Start()
    {
        //nameText.text = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void Interact()
    {
        Debug.Log("interacted with " + gameObject.name);
      
    }
}
