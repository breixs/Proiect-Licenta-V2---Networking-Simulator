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
    private Collider heldObjColl;

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
            if (pickUpObj.GetComponent<Collider>())
            {
                Debug.Log("Got Collider of " + pickUpObj.name);
                heldObjColl = pickUpObj.GetComponent<Collider>();
                heldObjColl.isTrigger=true;
            }
            //if (pickUpObj.GetComponent<SphereCollider>())
            //{
            //    Debug.Log("Got Collider of " + pickUpObj.name);
            //    heldObjColl = pickUpObj.GetComponent<SphereCollider>();
            //    heldObjColl.isTrigger = true;
            //}
                heldObjRb = pickUpObj.GetComponent<Rigidbody>();  
            if (heldObjRb.useGravity == false)
                heldObjRb.useGravity = true;
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;
            //pickUpCam.gameObject.SetActive(true);
            //Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    public void DropObject(bool kinematic, GameObject parentObj)
    {
        
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        //pickUpCam.gameObject.SetActive(false);
        if (kinematic == true)
            heldObjRb.isKinematic = true;
        else
            heldObjRb.isKinematic = false;
        if(parentObj==null)
        heldObj.transform.parent = null;
        else
        {
            heldObj.transform.parent=parentObj.transform;
        }    
        heldObj = null;
        heldObjColl.isTrigger = false;
    }
    public void MoveObject()
    {
        //positionam obiectul la pozitia setata
        heldObj.transform.position = holdPos.transform.position;
        //rotim obiectul la rotatia (0,0,0)
        heldObj.transform.rotation = holdPos.transform.rotation;
        heldObjColl.isTrigger = true;
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
    public void SetObject(Vector3 pos)
    {
        Vector3 rot = new Vector3(0, -90, 0);
        heldObj.transform.position = pos;
        heldObjRb.isKinematic = true;
        heldObjRb.useGravity = false;
        heldObj.transform.rotation = Quaternion.Euler(rot);
        heldObjColl.isTrigger = false;
        DropObject(true, null);

    }

    public void ConnectObject(Vector3 pos, GameObject parentObj)
    {
        Vector3 rot = new Vector3(0, -90, 0);
        heldObj.transform.position = pos;
        heldObjRb.isKinematic = true;
        heldObjRb.useGravity = false;
        heldObj.transform.rotation = Quaternion.Euler(rot);
        DropObject(true, parentObj);
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
