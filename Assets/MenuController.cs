using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public ShipMenu[] shipMenus = new ShipMenu[3];

    public ShipLoadoutData[] shipLoadouts = new ShipLoadoutData[3];

    public void Start()
    {
        Database.Start();
        foreach (ShipMenu ship in shipMenus)
        {
            ship.Init();
        }

        ShowNecessaryMenus();
    }
    
    //called by start button. finalises loadouts and then loads game scene
    public void StartGame()
    {
        int x = 0;
        foreach (ShipMenu ship in shipMenus)
        {
            ship.GetSelections();
            SaveLoadout(shipLoadouts[x], ship);
            x++;
        }

        SceneManager.LoadScene("TestingScene", LoadSceneMode.Single);
    }

    //stores the selected ships and guns from shipMenu into the shipLoadout
    private void SaveLoadout(ShipLoadoutData shipLoadout, ShipMenu shipMenu)
    {
        if (shipMenu.GetShip() != "None")
        {
            shipLoadout.Ship = Database.ShipNamesList[shipMenu.GetShip()];
            if (shipMenu.GetSlot1() != "None")
            {
                shipLoadout.Slot1 = Database.GunNamesList[shipMenu.GetSlot1()];
            }
            else
            {
                shipLoadout.Slot1 = null;
            }
            if (shipMenu.GetSlot2() != "None")
            {
                shipLoadout.Slot2 = Database.GunNamesList[shipMenu.GetSlot2()];
            }
            else
            {
                shipLoadout.Slot2 = null;
            }
        }
        else
        {
            shipLoadout.Ship = null;
            shipLoadout.Slot1 = null;
            shipLoadout.Slot2 = null;
        }
    }

    //show/hide individual ship menus based on if the previous ship is selected
    public void ShowNecessaryMenus()
    {
        shipMenus[1].gameObject.SetActive(false);
        shipMenus[2].gameObject.SetActive(false);
        if (shipMenus[0].GetShip() != "None")
        {
            shipMenus[1].gameObject.SetActive(true);
            if (shipMenus[1].GetShip() != "None")
            {
                shipMenus[2].gameObject.SetActive(true);
            }
        }
    }
}
