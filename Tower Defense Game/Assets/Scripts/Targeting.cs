using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* the purpose of this class is to find a object with a given tag (set in unity) inside a given range
*/

public class Targeting : MonoBehaviour {
    public string targetTag;   

    public GameObject FindTarget(float range)    
    {
        GameObject nearestTarget = null;
        //makes a list of all known enemies
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject target in allEnemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestTarget = target; //.GetComponent<Health>().hitbox;
            }
        }

        // return the targen when it has a target and it is inside the given range
        if (nearestTarget != null && shortestDistance <= range)
        {
            return nearestTarget as GameObject;           
        }
        else
        {
            return null;
        }
    }
}