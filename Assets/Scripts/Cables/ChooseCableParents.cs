using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCableParents : MonoBehaviour
{
    private GameObject switch1, switch2;
    void Start()
    {
        int rand1 = 0;
        int rand2 = 0;

        while (rand1 == rand2)
        {
            rand1 = Random.Range(0, 3);
            rand2 = Random.Range(0, 3);
            Debug.Log("rand1= " + rand1.ToString() + "rand2= " + rand2.ToString());
        }

        var switches = GameObject.FindGameObjectsWithTag("Switch");
        for (int i = 0; i < switches.Length; i++)
        {
            if (i == rand1)
            {
                switch1 = switches[i];
            }
            if (i == rand2)
            {
                switch2 = switches[i];
            }
        }
        //Debug.Log("switch1= " + switch1.name);
        //Debug.Log("switch2= " + switch2.name);
    }

    public GameObject getSwitch1()
    {
        return switch1;
    }
    public GameObject getSwitch2()
    {
        return switch2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
