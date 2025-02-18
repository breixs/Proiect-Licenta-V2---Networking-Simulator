using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class CheckCableParents : MonoBehaviour
{
    public GameObject CableManager;
    private GameObject[] startNodes;
    private GameObject[] endNodes;
    private GameObject[] cables;

    public TextMeshProUGUI taskText;

    private GameObject switch1, switch2;
    private GetCablesScript getCablesScript;
    private bool taskCompleted = false;

    void Start()
    {
        cables = new GameObject[CableManager.gameObject.transform.childCount - 1];
        for (int i = 0; i < cables.Length; i++)
        {
            cables[i] = CableManager.transform.GetChild(i).gameObject;
        }
        Debug.Log(cables.Length);

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
        Debug.Log("switch1= " + switch1.name);
        Debug.Log("switch2= " + switch2.name);

        updateTasktext("Task : Connect " + switch1.name + " with " + switch2.name);

        startNodes = new GameObject[cables.Length];
        endNodes = new GameObject[cables.Length];

        for (int i = 0; i < cables.Length; i++)
        {
            startNodes[i] = cables[i].transform.GetChild(0).gameObject;
            endNodes[i] = cables[i].transform.GetChild(1).gameObject;
        }
    }

    void Update()
    {
        bool isConnected = false;

        for (int i = 0; i < cables.Length; i++)
        {
            if (startNodes[i].transform.parent != null && endNodes[i].transform.parent != null)
            {
                Debug.Log("start node parent= " + startNodes[i].transform.parent.name + " end node parent= " + endNodes[i].transform.parent.name);
                if ((startNodes[i].transform.parent.gameObject == switch1 && endNodes[i].transform.parent.gameObject == switch2) ||
                    (startNodes[i].transform.parent.gameObject == switch2 && endNodes[i].transform.parent.gameObject == switch1))
                {
                    isConnected = true;
                    break;
                }
            }
        }

        if (isConnected && !taskCompleted)
        {
            taskCompleted = true;
            updateTasktext("TASKS COMPLETED");
        }
        else if (!isConnected && taskCompleted)
        {
            taskCompleted = false;
            updateTasktext("Task : Connect " + switch1.name + " with " + switch2.name);
        }
    }

    private void updateTasktext(string message)
    {
        taskText.text = message;
    }
}