using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public ShipMenu[] ships = new ShipMenu[3];

    public ShipLoadoutData[] shipLoadouts = new ShipLoadoutData[3];

    public void Start()
    {
        Database.Start();
        foreach (ShipMenu ship in ships)
        {
            ship.Init();
        }
    }

    public void StartGame()
    {
        int x = 0;
        foreach (ShipMenu ship in ships)
        {
            ship.GetSelections();
            SaveLoadout(shipLoadouts[x], ship);
            x++;
        }

        SceneManager.LoadScene("TestingScene", LoadSceneMode.Single);
    }

    private void SaveLoadout(ShipLoadoutData shipLoadout, ShipMenu shipMenu)
    {
        if (shipMenu.GetShip() != "None")
        {
            shipLoadout.Ship = Database.ShipNamesList[shipMenu.GetShip()];
            if (shipMenu.GetMainGun() != "None")
            {
                shipLoadout.MainGun = Database.GunNamesList[shipMenu.GetMainGun()];
            }
            else
            {
                shipLoadout.MainGun = null;
            }
            if (shipMenu.GetAuxGun() != "None")
            {
                shipLoadout.AuxGun = Database.GunNamesList[shipMenu.GetAuxGun()];
            }
            else
            {
                shipLoadout.AuxGun = null;
            }
        }
        else
        {
            shipLoadout.Ship = null;
            shipLoadout.MainGun = null;
            shipLoadout.AuxGun = null;
        }
    }
}
