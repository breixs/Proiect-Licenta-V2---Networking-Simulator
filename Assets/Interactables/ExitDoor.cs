using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitDoor : Interactable
{
    public GameObject endMenu;
    protected override void CondInteract()
    {
        endMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    protected override void Interact()
    {
        Debug.Log("cannot leave yet");
    }
}
