using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCableParents : MonoBehaviour
{
    public static ChooseCableParents instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject[] ChooseEndDevices(string tag1, string tag2)
    {
        int rand1 = 0, rand2 = 0;
        //GameObject obj1 = null, obj2 = null;
        GameObject[] retObjs = new GameObject[2];
        GameObject[] tag1Objects = null;
        GameObject[] tag2Objects = null;
        if (tag1 == tag2)
        {
            tag1Objects = GameObject.FindGameObjectsWithTag(tag1);
            tag2Objects = tag1Objects;

            rand1 = Random.Range(0, tag1Objects.Length);
            Debug.Log("rand1= " + rand1.ToString());
            Debug.Log(tag2Objects.Length.ToString());

            while (rand1 == rand2)
            {
                rand2 = Random.Range(0, tag2Objects.Length);
                Debug.Log("rand2= " + rand2.ToString());
            }
        }
        else
        {
            Debug.Log("Is aici");
            tag1Objects = GameObject.FindGameObjectsWithTag(tag1);
            tag2Objects = GameObject.FindGameObjectsWithTag(tag2);

            rand1 = Random.Range(0, tag1Objects.Length);
            Debug.Log("rand1= " + rand1.ToString());
            Debug.Log(tag2Objects.Length.ToString());
            rand2 = Random.Range(0, tag2Objects.Length);
            Debug.Log("rand2= " + rand2.ToString());
        }

        for (int i = 0; i < tag1Objects.Length; i++)
        {
            if (i == rand1)
            {
                retObjs[0] = tag1Objects[i];
            }
        }
        for (int i = 0; i < tag2Objects.Length; i++)
        {
            if (i == rand2)
            {
                retObjs[1] = tag2Objects[i];
            }
        }
        return retObjs;
    }

    public GameObject ChooseMiddleDevice(string tag)
    {
        int rand1 = 0;
        //GameObject obj1 = null, obj2 = null;
        GameObject retObj = null;
        var randomObjects = GameObject.FindGameObjectsWithTag(tag);

        rand1 = Random.Range(0, randomObjects.Length);
        Debug.Log("rand1= " + rand1.ToString());

        for (int i = 0; i < randomObjects.Length; i++)
        {
            if (i == rand1)
            {
                retObj = randomObjects[i];
            }

        }
        return retObj;
    }
}
