using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //calculeaza rotatia camerei pentru privirea sus/jos
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        Debug.Log(xRotation);
        //aplicam la camera transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //rotim jucatorul pentru a ne uita stanga/dreapta
        transform.Rotate(Vector3.up*(mouseX*Time.deltaTime)*xSensitivity);
    }
}
