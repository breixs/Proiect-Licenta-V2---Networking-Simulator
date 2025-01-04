using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public Camera pickUpCam;
    public float throwForce = 500f; 
    public float pickUpRange = 5f; 
    private GameObject heldObj; 
    private Rigidbody heldObjRb;

    PlayerLook playerLookScript;
    void Start()
    {
        playerLookScript = player.GetComponent<PlayerLook>();
    }
    public void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) 
        {
            heldObj = pickUpObj; 
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); 
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;
            pickUpCam.gameObject.SetActive(true);
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    public void DropObject()
    {
        
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        pickUpCam.gameObject.SetActive(false);
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; 
        heldObj = null; 
    }
    public void MoveObject()
    {
        //positionam obiectul la pozitia setata
        heldObj.transform.position = holdPos.transform.position;
        //rotim obiectul la rotatia (0,0,0)
        heldObj.transform.rotation = holdPos.transform.rotation;
    }
    public void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    public void StopClipping() 
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); 
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); 
        }
    }
}
