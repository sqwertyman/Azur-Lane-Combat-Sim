using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class FleetMenuController : MonoBehaviour
{
    public ShipMenu[] vanguardShipMenus = new ShipMenu[3];
    public ShipMenu[] mainShipMenus = new ShipMenu[3];
    public ShipLoadoutData[] vanguardShipLoadouts = new ShipLoadoutData[3];
    public ShipLoadoutData[] mainShipLoadouts = new ShipLoadoutData[3];
    
    //initialise database and json directory (if needed), and load and correctly set ship menus
    public virtual void Start()
    {
        //initialises the menus, and loads the ships' saved loadouts
        for (int x = 0; x < vanguardShipMenus.Length; x++)
        {
            vanguardShipMenus[x].Init(FleetType.Vanguard);
            mainShipMenus[x].Init(FleetType.Main);

            LoadLoadouts(vanguardShipLoadouts[x], vanguardShipMenus[x], Database.vanguardFileName + (x + 1));
            LoadLoadouts(mainShipLoadouts[x], mainShipMenus[x], Database.mainFileName + (x + 1));
        }

        ShowNecessaryMenus();
    }


    //called when exiting fleet menu. if there's at least one main and vanguard ship, finalises loadouts, saves to json
    public virtual bool ExitFleetMenu()
    {
        if (vanguardShipMenus[0].GetShip() != "None" && mainShipMenus[0].GetShip() != "None")
        {
            for (int x = 0; x < vanguardShipMenus.Length; x++)
            {
                SaveLoadout(vanguardShipLoadouts[x], vanguardShipMenus[x], Database.vanguardFileName + (x + 1));
                SaveLoadout(mainShipLoadouts[x], mainShipMenus[x], Database.mainFileName + (x + 1));
            }

            return true;
        }

        else
        {
            return false;
        }
    }

    protected void SaveLoadout(ShipLoadoutData shipLoadout, ShipMenu shipMenu, string fileName)
    {
        shipMenu.GetSelections();
        SaveToLoadoutData(shipLoadout, shipMenu);

        File.WriteAllText(Database.savePath + fileName, shipLoadout.GetJson());
    }

    //stores the selected ships and guns from shipMenu into the shipLoadout
    protected void SaveToLoadoutData(ShipLoadoutData shipLoadout, ShipMenu shipMenu)
    {
        if (shipMenu.GetShip() != "None")
        {
            shipLoadout.Ship = Database.ShipList[shipMenu.GetShip()];
            if (shipMenu.GetSlot1() != "None")
            {
                shipLoadout.Slot1 = Database.GunList[shipMenu.GetSlot1()];
            }
            else
            {
                shipLoadout.Slot1 = null;
            }
            if (shipMenu.GetSlot2() != "None")
            {
                shipLoadout.Slot2 = Database.GunList[shipMenu.GetSlot2()];
            }
            else
            {
                shipLoadout.Slot2 = null;
            }
            if (shipMenu.GetSlot3() != "None")
            {
                shipLoadout.Slot3 = Database.GunList[shipMenu.GetSlot3()];
            }
            else
            {
                shipLoadout.Slot3 = null;
            }
        }
        else
        {
            shipLoadout.Ship = null;
            shipLoadout.Slot1 = null;
            shipLoadout.Slot2 = null;
            shipLoadout.Slot3 = null;
        }
    }

    //show/hide individual ship menus based on if the previous ship is selected
    protected virtual bool ShowHideMenus(ShipMenu[] shipMenus)
    {
        if (shipMenus[0].GetShip() != "None")
        {
            shipMenus[1].gameObject.SetActive(true);
            if (shipMenus[1].GetShip() != "None")
            {
                shipMenus[2].gameObject.SetActive(true);
            }
            else
            {
                shipMenus[2].ResetAll();
                shipMenus[2].gameObject.SetActive(false);
            }
            return true;
        }
        else
        {
            shipMenus[1].ResetAll();
            shipMenus[1].gameObject.SetActive(false);
            shipMenus[2].ResetAll();
            shipMenus[2].gameObject.SetActive(false);
            return false;
        }
    }

    //shows/hides all necessary menus. for other scripts to be able to do so. also shows/hides start button
    public void ShowNecessaryMenus()
    {
        bool vanguardOkay = ShowHideMenus(vanguardShipMenus);
        bool mainOkay = ShowHideMenus(mainShipMenus);
    }

    //loads the ship's saved loadout
    protected void LoadLoadouts(ShipLoadoutData shipLoadout, ShipMenu shipMenu, string fileName)
    {
        if (File.Exists(Database.savePath + fileName))
        {
            string tempJson = File.ReadAllText(Database.savePath + fileName);
            JsonUtility.FromJsonOverwrite(tempJson, shipLoadout);
        }

        shipMenu.SetSelections(shipLoadout);
    }
}
