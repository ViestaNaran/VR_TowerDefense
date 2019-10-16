using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* The purpose of this class is to gathere information about the towers and report this to the overlord
*/

public class Intel : MonoBehaviour
{
    public bool reportIntel;
    public float range;
    private float health;
    private Targeting targeting;
    private List<GameObject> spottedList;
    public bool printList; // to run test function

    private void Start()
    {
        spottedList = new List<GameObject>();
        targeting = GetComponent<Targeting>();
    }

    void Update()
    {
        if (targeting.FindTarget(range) != null)
        {   //add a non-null Tower-GameObject to the List
            AddTarget(targeting.FindTarget(range));
        }

        health = GetComponent<Health>().health;

        // return to overlord if health is 3 or lower
        if (health <= 3)
        {
            reportIntel = true;
        }

        // runs test script
        if (printList == true) PrintListDebug();
    }

    void AddTarget(GameObject target)
    {
        // This statement adds spotted towers to a list of gameObjects rather than their types.
        // This is to ensure that we dont end up with a list of strings so that a minion can spot multiple towers of the same type. 
        if (!spottedList.Contains(target))
        {
            spottedList.Add(target);
        }
    }

    // getter
    public List<GameObject> GetSpottedList()
    {
        return spottedList;
    }

    // a function to test if a minion got a list of towers.
    void PrintListDebug()
    {
        for (int i = 0; i < spottedList.Count; i++)
        {
            Debug.Log(spottedList[i].name);
        }
        printList = false;
    }
}