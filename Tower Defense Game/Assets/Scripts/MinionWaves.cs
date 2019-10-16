using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The purpose of this script is to call the spawner scripts with each of 
 * the minions in a list given to this script in a timely manner 
 */

public class MinionWaves : MonoBehaviour
{
    public GameObject spawner;
    private GameObject path;
    private GameObject minion;
    private List<GameObject> minionList;
    public float delayBetweenMinions;

    [Header("ReadOnly Info")]
    public float countDown;
    public int minionsToSpawn = 0;
    public bool isSpawning = false;
    private int i;

    public void SpawnWave(List<GameObject> minionList)
    {
        this.minionList = minionList;
        this.minionsToSpawn = minionList.Count;
        isSpawning = true;
    }

    private void Update()
    {
        countDown -= Time.deltaTime;
        if (isSpawning && countDown <= 0)
        {
            countDown = delayBetweenMinions;
            if (minionsToSpawn > 0)
            {
                minionsToSpawn--;

                // call the spawner script with the minion on i position in the list
                spawner.GetComponent<Spawner>().Spawn(minionList[i]);
                i++;
            }
            else if (minionsToSpawn <= 0)
            {
                isSpawning = false;
                i = 0;
            }
        }
    }
}
