using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Laptop : Interactable
{
    public GameObject terminal;
    protected override void Interact()
    {
        terminal.SetActive(true);
        PlayerUI.inTerminal = true;
        Cursor.visible = true;
    }
}
