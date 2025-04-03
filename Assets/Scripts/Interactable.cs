using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promoptMessage;

    public void BaseInteract()
    {
        Interact();
    }

    public void ConditionalInterract()
    {
        CondInteract();
    }

    protected virtual void Interact()
    {

    }

    protected virtual void CondInteract()
    {

    }
}
