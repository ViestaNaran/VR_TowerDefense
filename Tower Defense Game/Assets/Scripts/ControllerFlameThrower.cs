using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* The purpose of this Class is to enable and disable the flamethrower which is mounted on the fire tower
*/

public class ControllerFlameThrower : MonoBehaviour
{
    public GameObject flameThrower;
    private GameObject target;

    private void Update()
    {
        target = GetComponent<Tower>().GetTarget();
        if (target == null)
        {
            flameThrower.SetActive(false);
        }
        else
        {
            flameThrower.SetActive(true);
        }
    }
}
