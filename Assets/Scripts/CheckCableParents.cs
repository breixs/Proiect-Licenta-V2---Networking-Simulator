using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckCableParents : MonoBehaviour
{
    public GameObject startNode;
    public GameObject endNode;

    public TextMeshProUGUI taskText;

    private GameObject switch1, switch2;
    void Start()
    {
        int rand1=0;
        int rand2=0;

        while(rand1==rand2)
        {
            rand1= Random.Range(0, 3);
            rand2= Random.Range(0, 3);
            Debug.Log("rand1= " + rand1.ToString() + "rand2= "+rand2.ToString());
        }

        var switches = GameObject.FindGameObjectsWithTag("Switch");
        for(int i=0;i<switches.Length;i++)
        { 
           if(i==rand1)
           {
               switch1 = switches[i];
           }
           if(i==rand2)
           {
               switch2 = switches[i];
           }
        }
        Debug.Log("switch1= "+switch1.name);
        Debug.Log("switch2= "+switch2.name);

        updateTasktext("Task : Connect " + switch1 + " with " + switch2);
    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log("start node parent= " + startNode.transform.parent.parent.name+ " end node parent= " + endNode.transform.parent.parent.name);
        if(startNode.transform.parent != null && endNode.transform.parent != null)
        {
           //Debug.Log("start node parent= " + startNode.transform.parent.name+ " end node parent= " + endNode.transform.parent.name);
            if (startNode.transform.parent.gameObject==switch1)
            {
                if(endNode.transform.parent.gameObject==switch2)
                {
                    updateTasktext("TASKS COMPLETED");
                }
                else
                {
                    updateTasktext("Task : Connect " + switch1 + " with " + switch2);
                }
            }
            if (startNode.transform.parent.gameObject == switch2)
            {
                if (endNode.transform.parent.gameObject == switch1)
                {
                    updateTasktext("TASKS COMPLETED");
                }
                else
                {
                    updateTasktext("Task : Connect " + switch1 + " with " + switch2);
                }
            }
        }
    }

    /*public string GetNames()
    {
        string retString = switch1.name +" and "+ switch2.name;
        return retString;
    }*/

    private void updateTasktext(string message)
    {
        taskText.text=message;
    }

}
