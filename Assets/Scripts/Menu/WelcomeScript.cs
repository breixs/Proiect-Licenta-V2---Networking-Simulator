using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScript : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(true);
        
    }
    void Update()
    {
        if(gameObject.activeSelf && Input.GetKeyUp(KeyCode.Tab))
        {
            gameObject.SetActive(false);
        }
    }
}
