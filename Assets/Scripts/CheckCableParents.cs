using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCableParents : MonoBehaviour
{
    public GameObject startNode;
    public GameObject endNode;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("start node parent= " + startNode.transform.parent.name + " end node parent= " + endNode.transform.parent.name);
    }
}
