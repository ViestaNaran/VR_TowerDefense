using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* The purpose of this class is to add the feature to upgrade the stats of the towers,
* it has predefined stats which it adds to the tower when upgradeStats() is called
*/

public class StatsUpgrade : MonoBehaviour
{
    Tower towerStats;
    public int maxLevel;
    public int upgradeLevel;
    public float upgradeRange;
    public int upgradeDamage;
    public bool testUpgrade; // adds a button to unity inspector to test if upgrading stats works

    private void Start()
    {
        towerStats = GetComponent<Tower>();
    }

    private void Update()
    {
        //the test button, which add a level per click
        if (testUpgrade == true)
        {
            upgradeStats();
            testUpgrade = false;
        }
    }

    public void upgradeStats()
    {
        if (upgradeLevel < maxLevel)
        {
            upgradeLevel++;
            upDamage();
            upRange();
        }
    }

    // unused functions, which is made to upgrade specific stats.
    private void upDamage()
    {
        int damage;
        damage = towerStats.getDamage() + upgradeDamage;
        towerStats.setDamage(damage);
    }

    private void upRange()
    {
        float range;
        range = towerStats.getRange() + upgradeRange;
        towerStats.setRange(range);
    }
}