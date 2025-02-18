using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCablesScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    public GameObject[] children;
    void Start()
    {
        children = new GameObject[gameObject.transform.childCount];
        for(int i=0;i<children.Length;i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
    }

    public GameObject[] getCables()
    {
        return children;
    }
}
