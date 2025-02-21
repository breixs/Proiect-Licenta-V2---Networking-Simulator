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

    private Dictionary<GameObject, List<GameObject>> switchConnections;

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

        switchConnections = new Dictionary<GameObject, List<GameObject>>();
    }

    void Update()
    {
        switchConnections.Clear();

        for (int i = 0; i < cables.Length; i++)
        {
            if (startNodes[i].transform.parent != null && endNodes[i].transform.parent != null)
            {
                GameObject startSwitch = startNodes[i].transform.parent.gameObject;
                GameObject endSwitch = endNodes[i].transform.parent.gameObject;

                if (!switchConnections.ContainsKey(startSwitch))
                {
                    switchConnections[startSwitch] = new List<GameObject>();
                }
                if (!switchConnections.ContainsKey(endSwitch))
                {
                    switchConnections[endSwitch] = new List<GameObject>();
                }

                switchConnections[startSwitch].Add(endSwitch);
                switchConnections[endSwitch].Add(startSwitch);
            }
        }

        bool isConnected = BFS(switch1, switch2);

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

    private bool BFS(GameObject start, GameObject end)
    {
        if (start == end) return true;

        Queue<GameObject> queue = new Queue<GameObject>();
        HashSet<GameObject> visited = new HashSet<GameObject>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            GameObject current = queue.Dequeue();

            if (switchConnections.ContainsKey(current))
            {
                foreach (GameObject neighbor in switchConnections[current])
                {
                    if (neighbor == end)
                    {
                        return true;
                    }

                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return false;
    }

    private void updateTasktext(string message)
    {
        taskText.text = message;
    }
}