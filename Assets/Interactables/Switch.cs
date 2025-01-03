using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        Debug.Log("interacted with" + gameObject.name);
        //PickUpScript pickUpScript = gameObject.GetComponent<PickUpScript>();
        
        //pickUpScript.PickUpObject(gameObject);

        
    }
}
