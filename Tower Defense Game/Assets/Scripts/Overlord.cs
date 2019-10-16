using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The purpose of this script is to control the behavior of the overlord
 * This includes building a list of instantiated minions and pass it to the spawner
 * Change the stats and paths of the minions before spawn
 * receive reports from returning minions
 */

public class Overlord : MonoBehaviour
{
    private List<GameObject> towerListPath1;
    private List<GameObject> towerListPath2;
    private List<GameObject> towerListPath3;
    public bool printList;
    public DropDown whichPathToTest;

    [Header("Setup")]
    public GameObject spawner;
    public bool test;
    public bool spawnNextWave;

    [Header("Minions")]
    public GameObject knight;
    public GameObject archer;
    public GameObject mage;
    public int maxResistPoints;

    [Header("Paths")]
    public GameObject path1;
    public GameObject path2;
    public GameObject path3;

    [Header("Wave Stats")]
    public int waveSize;
    public int minionsCount;
    public float timeBetweenWaves;
    public float WaveCountDown;

    [Header("Read Only")]
    public int waveNumber;

    private float dpmFireRatio = 0.25f;
    private float dpmWaterRatio = 0.25f;
    private float dpmLightningRatio = 0.25f;
    private float dpmPhysicalRatio = 0.25f;

    public enum DropDown
    {   //the different paths to choose from in a dropdown menu in unity inspector
        path1,
        path2,
        path3
    };

    void Start()
    {
        towerListPath1 = new List<GameObject>();
        towerListPath2 = new List<GameObject>();
        towerListPath3 = new List<GameObject>();
    }

    void Update()
    {
        if (minionsCount < 1)
        {
            minionsCount = 0;
            WaveCountDown -= Time.deltaTime;

            if (WaveCountDown <= 0)
            {
                WaveCountDown = timeBetweenWaves;    
                spawnNextWave = true;
                waveNumber++;
            }
        }

        if (spawnNextWave == true)
        {
            // this calls the SpawnWave function in the MinionWaves Script
            // where it add the list from generateListToSpawn to the constructur
            GetComponent<MinionWaves>().SpawnWave(AddMinionToSpawnList(waveSize));
            spawnNextWave = false;
        }

        // test for printing objects of list into console depending on which path you wanna know about
        if (printList == true)
        {
            if (whichPathToTest.ToString() == "path1")
            {
                PrintListDebug(towerListPath1);
                CalculateDamageRatios(towerListPath1);
            }

            if (whichPathToTest.ToString() == "path2")
            {
                PrintListDebug(towerListPath2);
                CalculateDamageRatios(towerListPath2);
            }

            if (whichPathToTest.ToString() == "path3")
            {
                PrintListDebug(towerListPath3);
                CalculateDamageRatios(towerListPath3);
            }
        }
    }

    // generate a list of random minions with random paths
    private List<GameObject> AddMinionToSpawnList(int waveSize)
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < waveSize; i++)
        {
            //Instantiate a to change it stats from the prefab without interfering with other instantiated minions, and lastly disable it
            GameObject minion = GameObject.Instantiate(RandomMinion());
            minion.SetActive(false);

            // set path to minion
            GameObject path = RandomPath();
            minion.GetComponent<PathFinding>().SetPathList(path);

            // add more health depending on which wave the game is at
            float minionHealth = minion.GetComponent<Health>().health + waveNumber;
            minion.GetComponent<Health>().health = minionHealth;

            // add a resist profil to the minion
            if (path == path1)
            {
                CalculateDamageRatios(towerListPath1);
            }
            else if (path == path2)
            {
                CalculateDamageRatios(towerListPath1);
            }
            else if (path == path3)
            {
                CalculateDamageRatios(towerListPath1);
            }

            minion = ApplyResistanceProfil(minion);

            // add minion to the listToSpawn
            list.Add(minion);
        }
        return list;
    }

    // this will take a minion, generate and add a resistProfile to the minion, and lastely return the minion with the new stats 
    private GameObject ApplyResistanceProfil(GameObject minion)
    {
        int physical = 10 + Mathf.RoundToInt(dpmPhysicalRatio);
        int fire = 10 + Mathf.RoundToInt(dpmFireRatio);
        int water = 10 + Mathf.RoundToInt(dpmWaterRatio);
        int lightning = 10 + Mathf.RoundToInt(dpmLightningRatio);

        minion.GetComponent<Health>().SetResistanceProfil(physical, fire, water, lightning);
        return minion;
    }

    // function to add information about reporting minions to the storage lists
    public void ReceiveSpotList(List<GameObject> list, GameObject path)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (path == path1 && !towerListPath1.Contains(list[i]))
            {
                towerListPath1.Add(list[i]);
            }
            else if (path == path2 && !towerListPath1.Contains(list[i]))
            {
                towerListPath2.Add(list[i]);
            }
            else if (path == path3 && !towerListPath1.Contains(list[i]))
            {
                towerListPath3.Add(list[i]);
            }
        }
    }

    // return a random minion to caller 
    private GameObject RandomMinion()
    {
        switch (Random.Range(0, 3))
        {
            case 0: return knight;
            case 1: return archer;
            case 2: return mage;
        }
        return knight; //Default fallover
    }

    // return a random path to caller 
    private GameObject RandomPath()
    {
        switch (Random.Range(0, 3))
        {
            case 0: return path1;
            case 1: return path2;
            case 2: return path3;
        }
        return path3; //Default fallover
    }

    // 2 functions to increase or decrease minion count
    public void IncreaseMinionCount()
    {
        this.minionsCount++;
    }

    public void DecreaseMinionCount()
    {
        this.minionsCount--;
    }

    // this function evaluete the ratio of damage of the specific damage types from a given path
    void CalculateDamageRatios(List<GameObject> path)
    {
        float dpmFire = 0.0f;
        float dpmWater = 0.0f;
        float dpmLightning = 0.0f;
        float dpmPhysical = 0.0f;
        float dpmTotal = 0.0f;

        for (int i = 0; i < path.Count; i++)
        {
            dpmTotal = dpmTotal + (float)path[i].GetComponent<Tower>().getDamagePerMinut();
            switch (path[i].GetComponent<Tower>().getDamageType().ToString())
            {
                case "fire":
                    dpmFire = dpmFire + (float)path[i].GetComponent<Tower>().getDamagePerMinut();
                    break;

                case "water":
                    dpmWater = dpmWater + (float)path[i].GetComponent<Tower>().getDamagePerMinut();
                    break;

                case "lightning":
                    dpmLightning = dpmLightning + (float)path[i].GetComponent<Tower>().getDamagePerMinut();
                    break;

                case "physical":
                    dpmPhysical = dpmPhysical + (float)path[i].GetComponent<Tower>().getDamagePerMinut();
                    break;
            }
        }

        if (dpmTotal != 0)
        {
            dpmFireRatio = dpmFire / dpmTotal * maxResistPoints;
            dpmWaterRatio = dpmWater / dpmTotal * maxResistPoints;
            dpmLightningRatio = dpmLightning / dpmTotal * maxResistPoints;
            dpmPhysicalRatio = dpmPhysical / dpmTotal * maxResistPoints;
        }
        
        // output results in console
        Debug.Log("Fire dmg: " + dpmFire);
        Debug.Log("Water dmg: " + dpmWater);
        Debug.Log("Lightning dmg: " + dpmLightning);
        Debug.Log("Physical dmg: " + dpmPhysical);
        Debug.Log("Total dmg: " + dpmTotal);
    }

    // this is a function to print the list of spotted towers to console depending on which path that is selected in unity
    void PrintListDebug(List<GameObject> path)
    {
        for (int i = 0; i < path.Count; i++)
        {
            Debug.Log(path[i].name);
        }
        printList = false;
    }
}