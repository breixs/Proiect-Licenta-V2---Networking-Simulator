using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerInteract : MonoBehaviour
{
    public Transform holdPos;
    private Camera cam;
    [SerializeField]
    private float distance = 4f;
    [SerializeField]
    private LayerMask mask;

    private PlayerUI playerUI;

    private GameObject heldObj;

    private InputManager inputManager;

    private PickUpScript pickUpScript;
    private bool isHolding = false;

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
        playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;

        if (isHolding && inputManager.onFoot.PickUp.triggered)
        {
            Debug.Log("hold");

            pickUpScript.StopClipping();
            pickUpScript.DropObject();
            isHolding = false;
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
                        isHolding = true;
                        Debug.Log("null");
                        if (hitInfo.transform.gameObject.tag == "CanPickUp")
                        {
                            pickUpScript.PickUpObject(hitInfo.transform.gameObject);
                            pickUpScript.MoveObject();
                            //pickUpScript.RotateObject();
                        }

                    }
                    else
                    {
                        Debug.Log("hold");

                        pickUpScript.StopClipping();
                        pickUpScript.DropObject();
                        isHolding = false;
                    }
                    
                }             
            }
        }
    }
}
