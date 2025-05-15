using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject switchDevice;
    public GameObject routerDevice;

    private void Awake()
    {
        int deviceNumber = 0;
        GameObject[] spawnPoints;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        List<GameObject> availableSpawnPoints = new List<GameObject>(spawnPoints);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (availableSpawnPoints.Count == 0) break;

            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            GameObject spawn = availableSpawnPoints[randomIndex];
            availableSpawnPoints.RemoveAt(randomIndex);

            if (i > 2)
            {
                var newSwitch = Instantiate(switchDevice, spawn.transform.position, spawn.transform.rotation);
                newSwitch.name = "Switch " + deviceNumber;
                deviceNumber++;
            }
            else
            {
                var newRouter = Instantiate(routerDevice, spawn.transform.position, spawn.transform.rotation);
                newRouter.name = "Router " + i;
            }
        }

        Destroy(switchDevice);
        Destroy(routerDevice);

    }        
}
