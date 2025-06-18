using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckCableParents : MonoBehaviour
{
    public GameObject CableManager;
    private GameObject[] startNodes;
    private GameObject[] endNodes;
    private GameObject[] cables;
    public GameObject consoleCable;
    private GameObject consoleStartNode;
    private GameObject consoleEndNode;

    private Dictionary<GameObject, List<GameObject>> switchConnections;

    public static CheckCableParents instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        consoleStartNode = consoleCable.transform.GetChild(0).gameObject;
        consoleEndNode=consoleCable.transform.GetChild(1).gameObject;

        cables = new GameObject[CableManager.gameObject.transform.childCount - 1];
        for (int i = 0; i < cables.Length; i++)
        {
            cables[i] = CableManager.transform.GetChild(i).gameObject;
        }
        Debug.Log(cables.Length);

        startNodes = new GameObject[cables.Length];
        endNodes = new GameObject[cables.Length];

        for (int i = 0; i < cables.Length; i++)
        {
            startNodes[i] = cables[i].transform.GetChild(0).gameObject;
            endNodes[i] = cables[i].transform.GetChild(1).gameObject;
        }

        switchConnections = new Dictionary<GameObject, List<GameObject>>();
    }

    public bool CheckParents(GameObject obj1, GameObject obj2, GameObject midObj=null, int vlanID=0, string ipAddress=null)
    {
        switchConnections.Clear();
        bool isConnected=false;
        bool correctIP;
        if(vlanID==0 && ipAddress==null)
        {
            correctIP = true;
        }
        else
        {
            correctIP=false;
        }

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

        if (midObj == null)
        {
            isConnected = BFS(obj1, obj2);
            Debug.Log(isConnected);
        }
        else
        {
            bool connectedStart = BFS(obj1, midObj);
            bool connectedEnd = BFS(midObj, obj2);

            if (connectedStart && connectedEnd)
            {
                isConnected = true;
                if(midObj.GetComponent<Switch>() && vlanID>0 && ipAddress!=null)
                {
                    if (midObj.GetComponent<Switch>().ContainsVlan(vlanID))
                    {
                        if (midObj.GetComponent<Switch>().ContainsIp(vlanID, ipAddress))
                        {
                            correctIP = true;
                        }
                    }
                }
            }
        }

        Debug.Log("is connected: "+ isConnected);
        Debug.Log("correct ip: "+ correctIP);

        if (isConnected && correctIP)
        {
            return true;
        }
        return false;
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

            if (current == end)
                return true;

            if (switchConnections.ContainsKey(current))
            {
                foreach (GameObject neighbor in switchConnections[current])
                {
                    if (neighbor!=end && !neighbor.CompareTag("Switch"))
                    {
                        continue;
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

    public GameObject getConsoleStartNodeParent()
    {
        return consoleStartNode.transform.parent.gameObject;
    }
    public GameObject getConsoleEndNodeParent()
    {
        return consoleEndNode.transform.parent.gameObject;
    }
}