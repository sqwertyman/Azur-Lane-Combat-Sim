using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public ShipMenu[] vanguardShipMenus = new ShipMenu[3];
    public ShipMenu[] mainShipMenus = new ShipMenu[3];
    public ShipLoadoutData[] vanguardShipLoadouts = new ShipLoadoutData[3];
    public ShipLoadoutData[] mainShipLoadouts = new ShipLoadoutData[3];
    public Button startButton;

    private string savePath, mainFileName, vanguardFileName;

    //initialise database and json directory (if needed), and load and correctly set ship menus
    public void Start()
    {
        savePath = Application.persistentDataPath + "/Saves";
        mainFileName = "/mainshipsave";
        vanguardFileName = "/vanguardshipsave"; 

        Database.Start();

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        
        //initialises the menus, and loads the ships' saved loadouts
        for (int x = 0; x < vanguardShipMenus.Length; x++)
        {
            vanguardShipMenus[x].Init(FleetType.Vanguard);
            mainShipMenus[x].Init(FleetType.Main);

            LoadLoadouts(vanguardShipLoadouts[x], vanguardShipMenus[x], vanguardFileName + (x + 1));
            LoadLoadouts(mainShipLoadouts[x], mainShipMenus[x], mainFileName + (x + 1));
        }

        ShowNecessaryMenus();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    //called by start button. if there's at least one main and vanguard ship, finalises loadouts, saves to json, then loads game scene
    public void StartGame()
    {
        if (vanguardShipMenus[0].GetShip() != "None" && mainShipMenus[0].GetShip() != "None")
        {
            for (int x = 0; x < vanguardShipMenus.Length; x++)
            {
                SaveLoadout(vanguardShipLoadouts[x], vanguardShipMenus[x], vanguardFileName + (x + 1));
                SaveLoadout(mainShipLoadouts[x], mainShipMenus[x], mainFileName + (x + 1));
            }

            SceneManager.LoadScene("TestingScene", LoadSceneMode.Single);
        }

        else
        {
            print("must select a ship");
        }
    }

    private void SaveLoadout(ShipLoadoutData shipLoadout, ShipMenu shipMenu, string fileName)
    {
        shipMenu.GetSelections();
        SaveToLoadoutData(shipLoadout, shipMenu);

        File.WriteAllText(savePath + fileName, shipLoadout.GetJson());
    }

    //stores the selected ships and guns from shipMenu into the shipLoadout
    private void SaveToLoadoutData(ShipLoadoutData shipLoadout, ShipMenu shipMenu)
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
        }
        else
        {
            shipLoadout.Ship = null;
            shipLoadout.Slot1 = null;
            shipLoadout.Slot2 = null;
        }
    }

    //show/hide individual ship menus based on if the previous ship is selected
    private bool ShowHideMenus(ShipMenu[] shipMenus)
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

        startButton.gameObject.SetActive(vanguardOkay && mainOkay);
    }

    //loads the ship's saved loadout
    private void LoadLoadouts(ShipLoadoutData shipLoadout, ShipMenu shipMenu, string fileName)
    {
        if (File.Exists(savePath + fileName))
        {
            string tempJson = File.ReadAllText(savePath + fileName);
            JsonUtility.FromJsonOverwrite(tempJson, shipLoadout);
        }

        shipMenu.SetSelections(shipLoadout);
    }
}
