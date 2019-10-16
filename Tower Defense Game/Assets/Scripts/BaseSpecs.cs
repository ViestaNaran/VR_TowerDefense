using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * the purpose of this script is to add functionallity to the base, 
 * so it can report to the overlord if it has taken damaged and despawned a minion
 */

public class BaseSpecs : MonoBehaviour
{
    bool gameover = false;

    [Header("Setup")]
    public int health;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Enemy")
        {
            health--;
            Destroy(Other.gameObject);
            if (health <= 0)
            {
                gameover = true;
                Destroy(gameObject);
            }
        }
    }

    public void ReduceHealth(int amount)
    {
        health = health - amount;
    }
}