using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerInteract : MonoBehaviour
{
    public Transform holdPos;
    public Material transparent;
    public Material activ;
    private Camera cam;
    [SerializeField]
    private float distance = 4f;
    [SerializeField]
    private LayerMask mask;

    private PlayerUI playerUI;

    private InputManager inputManager;

    private PickUpScript pickUpScript;
    private bool isHolding = false;
    private bool isHoldingRope = false;
    //private bool isHoldingSW = false;
    //private bool isHoldingR = false;
    private string holdTag;

    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        pickUpScript = gameObject.GetComponent<PickUpScript>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isHoldingRope == true)
            Debug.Log("rope");
        else
            Debug.Log("no rope");*/
        if(playerUI != null)
            playerUI.DeactivateText();

        GameObject[] placeHoldersSW = GameObject.FindGameObjectsWithTag("PlaceHolderSW");
        GameObject[] placeHoldersR = GameObject.FindGameObjectsWithTag("PlaceHolderR");
        GameObject[] placeHolders = new GameObject[placeHoldersSW.Length + placeHoldersR.Length];

        placeHoldersSW.CopyTo(placeHolders, 0);
        placeHoldersR.CopyTo(placeHolders, placeHoldersSW.Length);
        foreach (var go in placeHolders)
        {
            go.GetComponent<MeshRenderer>().material = transparent;
        }


        //playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;

        if (isHolding && inputManager.onFoot.PickUp.triggered)
        {
            pickUpScript.StopClipping();
            pickUpScript.DropObject(false, null);
            isHolding = false;
            if (isHoldingRope)
                isHoldingRope = false;
        }

        if (Physics.Raycast(ray, out hitInfo, distance,mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(hitInfo.collider.GetComponent<Interactable>().promoptMessage);
                //interactionare cu obiect (tasta E)
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
                //ridicarea unui obiect (tasta F)
                if (inputManager.onFoot.PickUp.triggered)
                {
                    if (isHolding == false) 
                    {
                        if (hitInfo.transform.gameObject.tag == "CanPickUp" || hitInfo.transform.gameObject.tag=="Switch" || hitInfo.transform.gameObject.tag == "Router")
                        {
                            holdTag = hitInfo.transform.gameObject.tag;
                            Debug.Log(holdTag);
                            isHolding = true;
                            pickUpScript.PickUpObject(hitInfo.transform.gameObject);
                            pickUpScript.MoveObject();
                            //pickUpScript.RotateObject();
                        }
                        if (hitInfo.transform.gameObject.tag == "PickUpRope")
                        {
                            isHolding = true;
                            isHoldingRope = true;
                            pickUpScript.PickUpObject(hitInfo.transform.gameObject);
                            pickUpScript.MoveObject();
                        }

                    }
                    else
                    {
                        pickUpScript.StopClipping();
                        pickUpScript.DropObject(false, null);
                        isHolding = false;
                        if (isHoldingRope)
                            isHoldingRope = false;
                    }
                    
                }             
            }
            if (hitInfo.collider.CompareTag("PlaceHolderSW") || hitInfo.collider.CompareTag("PlaceHolderR"))
            {
                if (isHolding && !isHoldingRope && holdTag == "Switch")
                {
                    if(hitInfo.collider.CompareTag("PlaceHolderSW"))
                        hitInfo.collider.GetComponent<MeshRenderer>().material = activ;

                    if(inputManager.onFoot.Interact.triggered)
                    {
                        pickUpScript.SetObject(hitInfo.collider.transform.position);
                        isHolding = false;
                        //hitInfo.collider.transform.position
                    }
                }
                else if(isHolding && !isHoldingRope && holdTag == "Router")
                {
                    if(hitInfo.collider.CompareTag("PlaceHolderR"))
                        hitInfo.collider.GetComponent<MeshRenderer>().material = activ;

                    if (inputManager.onFoot.Interact.triggered)
                    {
                        pickUpScript.SetObject(hitInfo.collider.transform.position);
                        isHolding = false;
                        //hitInfo.collider.transform.position
                    }
                }
                
            }

            if (hitInfo.collider.CompareTag("SwitchPort"))
            {
                if (isHoldingRope)
                {
                    if (inputManager.onFoot.Interact.triggered)
                    {
                        pickUpScript.ConnectObject(hitInfo.collider.transform.position, hitInfo.collider.transform.parent.gameObject);
                        isHoldingRope = false;
                        isHolding = false;
                    }

                }

            }
        }
    }
}
