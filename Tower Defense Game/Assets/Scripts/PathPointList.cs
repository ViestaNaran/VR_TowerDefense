using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* The purpose of this path is to make a template where we can add gameobjects to, and generate a list a minion can follow.
* in short this contains the route the minion have to follow
*/

public class PathPointList : MonoBehaviour
{
    public GameObject pathPoint0;
    public GameObject pathPoint1;
    public GameObject pathPoint2;
    public GameObject pathPoint3;
    public GameObject pathPoint4;
    public GameObject pathPoint5;
    public GameObject pathPoint6;
    public GameObject pathPoint7;
    public GameObject pathPoint8;
    public GameObject pathPoint9;

    public List<GameObject> path = new List<GameObject>();

    public List<GameObject> getPathList()
    {
        return path;
    }

    //adds all the points to the list if it isn't null
    void listAdder(GameObject point)
    {
        if (point != null)
        {
            path.Add(point);
        }
    }

    // Use this for initialization
    void Start()
    {
        listAdder(pathPoint0);
        listAdder(pathPoint1);
        listAdder(pathPoint2);
        listAdder(pathPoint3);
        listAdder(pathPoint4);
        listAdder(pathPoint5);
        listAdder(pathPoint6);
        listAdder(pathPoint7);
        listAdder(pathPoint8);
        listAdder(pathPoint9);
    }
}
