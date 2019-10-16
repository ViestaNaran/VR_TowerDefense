using UnityEngine;
using System.Collections;
using Valve.VR;

public class BuildingMenu : MonoBehaviour
{

    Vector3 BenjaminPoints = new Vector3();
    SteamVR_TrackedObject obj; // finding the controller
    [Header("Setup")]
    public int itemID;
    public GameObject buttonHolder; //empty object that contains the buttons
    public GameObject Hand;
    public GameObject TowerFoundation;
    public GameObject BuildingBall;
    public GameObject UpgradeObject;

    [Header("Debug")]
    public bool buttonEnabled; // saying whether or the empty object is enabled

    // Public booleans, need to be accesed in other scripts
    public bool holding;
    public bool holdingFoundation;
    public bool holdingBall;
    public bool holdingUpgrade;
    public bool validBuildVec;
    Vector3 buildVec;
    RaycastHit hit;

    void Start()
    {
        obj = GetComponent<SteamVR_TrackedObject>();
        buttonHolder.SetActive(false);
        buttonEnabled = false;
        holdingFoundation = false;
        holdingBall = false;
        holding = false;
        holdingUpgrade = false;
        validBuildVec = false;
    } // Start end

    void Update()
    {
        MenuOpen();
        CheckItemID();
        PlaceObject();
    } // Update end

    void MenuOpen()
    {

        var device = SteamVR_Controller.Input(4); //you are getting the device and setting it here
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) //When you press the button above the Dpad you will do this function
        {
            Debug.Log("Buttons should be enabled");
            if (buttonEnabled == false)
            {
                buttonHolder.SetActive(true);
                buttonEnabled = true;
            }
            else if (buttonEnabled == true)
            {
                buttonHolder.SetActive(false);
                buttonEnabled = false;
            }

        }
    }// MenuOpen end

    void CheckItemID()
    {
        var device = SteamVR_Controller.Input(4);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis0))
        {
            itemID++;
            Debug.Log(itemID);
        }
        else if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis1))
        {
            itemID--;
            Debug.Log(itemID);
        }
    }

    void PlaceObject()
    {

        var device = SteamVR_Controller.Input(3);

        if (holdingFoundation || holdingBall || holdingUpgrade)
        {
            Hand.GetComponent<SteamVR_LaserPointer>().pointer.active = true;

            var ray = new Ray(Hand.transform.position, Hand.transform.forward);

            if (Physics.Raycast(Hand.transform.position, Hand.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                validBuildVec = true;
                Hand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else
            {
                validBuildVec = false;
                Hand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        else {
            Hand.GetComponent<SteamVR_LaserPointer>().pointer.active = false;
        }

        if (holdingFoundation)
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (validBuildVec)
                {
                    buildVec = hit.point;
                }
            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Instantiate(TowerFoundation, buildVec, Quaternion.identity);

                holdingFoundation = false;
                holding = false;
            }


        }
        if (holdingBall)
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (validBuildVec)
                {
                    buildVec = hit.point;
                }
            }

            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (validBuildVec)
                {
                    Instantiate(BuildingBall, buildVec, Hand.transform.rotation);

                    holdingBall = false;
                    holding = false;
                }
            }
        }
        if (holdingUpgrade)
        {

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (validBuildVec)
                {
                    buildVec = hit.point;
                }
            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (validBuildVec)
                {
                    Instantiate(UpgradeObject, buildVec, Hand.transform.rotation);

                    holdingUpgrade = false;
                    holding = false;
                }
            }

        }
    } // PlaceObjecft end

} // BuildingMenu end