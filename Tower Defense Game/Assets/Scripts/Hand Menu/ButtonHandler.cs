using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {
    [Header("Setup")]
    public bool button1;
    public bool button2;
    public bool button3;
    public GameObject Hand;
    BuildingMenu buildingMenu;


    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "VRHand2")
        {
            buildingMenu = Hand.GetComponent<BuildingMenu>();
            
            if (button1 && !buildingMenu.holding)
            {
                buildingMenu.holdingFoundation = true;
                buildingMenu.holding = true;                         
            }
            if (button2 && !buildingMenu.holding)
            {
                buildingMenu.holdingBall = true;
                buildingMenu.holding = true;
            }
            if (button3 && !buildingMenu.holding)
            {
                buildingMenu.holdingUpgrade = true;
                buildingMenu.holding = true;
            }
        }
    }// OnTriggerEnter end
}// ButtonHandler end
