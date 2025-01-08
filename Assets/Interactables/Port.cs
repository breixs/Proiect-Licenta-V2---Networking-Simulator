using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : Interactable
{
    public GameObject parent;
    private Rigidbody objRb;
    // Start is called before the first frame update
    void Start()
    {
        objRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent == null)
        {
            gameObject.transform.parent = parent.transform;
            objRb.isKinematic = false;
            objRb.useGravity = true;
        }
    }
    protected override void Interact()
    {
        Debug.Log("interacted with " + gameObject.name);
        if(gameObject.transform.parent != null && gameObject.transform.parent!=parent.transform)
        {
            gameObject.transform.parent = parent.transform;
            objRb.isKinematic = false;
            objRb.useGravity=true;
            
        }


    }
}
