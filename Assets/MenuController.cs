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
    public Button startButton;

    private string savePath;

    //initialise database and json directory (if needed), and load and correctly set ship menus
    public void Start()
    {
        savePath = Application.persistentDataPath + "/Saves";

        Database.Start();

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        for (int x = 0; x < shipMenus.Length; x++)
        {
            shipMenus[x].Init();

            if (File.Exists(savePath + "/shipsave" + (x + 1)))
            {
                string tempJson = File.ReadAllText(savePath + "/shipsave" + (x + 1));
                JsonUtility.FromJsonOverwrite(tempJson, shipLoadouts[x]);
            }

            shipMenus[x].SetSelections(shipLoadouts[x]);
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

    //called by start button. if there's at least one ship, finalises loadouts and saves to json, then loads game scene
    public void StartGame()
    {
        shipMenus[0].GetSelections();
        if (shipMenus[0].GetShip() != "None")
        {
            for (int x = 0; x < shipMenus.Length; x++)
            {
                shipMenus[x].GetSelections();
                SaveLoadout(shipLoadouts[x], shipMenus[x]);

                File.WriteAllText(savePath + "/shipsave" + (x + 1), shipLoadouts[x].GetJson());
            }

            SceneManager.LoadScene("TestingScene", LoadSceneMode.Single);
        }

        else
        {
            print("must select a ship");
        }
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
    //also hides start button if no ship is selected
    public void ShowNecessaryMenus()
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
            startButton.gameObject.SetActive(true);
        }
        else
        {
            shipMenus[1].ResetAll();
            shipMenus[1].gameObject.SetActive(false);
            shipMenus[2].ResetAll();
            shipMenus[2].gameObject.SetActive(false);
            startButton.gameObject.SetActive(false);
        }
    }
}
