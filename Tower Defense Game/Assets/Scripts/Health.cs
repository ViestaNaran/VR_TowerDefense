using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* The purpose of this class is to enable prefabs to have health, and a set of resistense to different type of damage
*/

public class Health : MonoBehaviour
{
    private GameObject overlord;

    [Header("Defensive Stats")]
    public float health;
    public int resistancePhysical;
    public int resistanceFire;
    public int resistanceWater;
    public int resistanceLightning;
    private int resistance;

    // set resistance to the resisten of the specific damageType that is being dealt.
    public void TakeDamage(int damageValue, string damageType, string objTag)
    {
        switch (damageType)
        {
            case "physical":
                resistance = resistancePhysical;
                break;
            case "fire":
                resistance = resistanceFire;
                break;
            case "water":
                resistance = resistanceWater;
                break;
            case "lightning":
                resistance = resistanceLightning;
                break;
        }

        health = health - (float)damageValue * ((float)(100 - resistance) / 100);

        // Take care of despawning the minion if killed
        if (health <= 0)
        {
            overlord.GetComponent<Overlord>().DecreaseMinionCount();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        overlord = GameObject.FindGameObjectWithTag("Overlord");
        overlord.GetComponent<Overlord>().IncreaseMinionCount();
    }

    // function to set all the resistances of the minion
    public void SetResistanceProfil(int physical, int fire, int water, int lightning)
    {
        resistancePhysical = physical;
        resistanceFire = fire;
        resistanceWater = water;
        resistanceLightning = lightning;
    }
}