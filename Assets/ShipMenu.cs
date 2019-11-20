using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMenu : MonoBehaviour
{
    public Dropdown shipDrop, mainGunDrop, auxGunDrop;

    private string ship, mainGun, auxGun;
    
    public void SetOptions()
    {
        List<string> tempList = new List<string>();
        foreach (string item in Database.ShipNamesList.Keys)
        {
            tempList.Add(item);
        }
        shipDrop.AddOptions(tempList);

        tempList = new List<string>();
        foreach (string item in Database.GunNamesList.Keys)
        {
            tempList.Add(item);
        }
        mainGunDrop.AddOptions(tempList);

        tempList = new List<string>();
        foreach (string item in Database.GunNamesList.Keys)
        {
            tempList.Add(item);
        }
        auxGunDrop.AddOptions(tempList);
    }
    //needs improving


    private void TempMeth(Dictionary<int, DatabaseItem> databaseList, Dropdown dropdown)
    {
        List<string> tempList = new List<string>();
        foreach (DatabaseItem item in databaseList.Values)
        {
            tempList.Add(item.Name);
        }
        dropdown.AddOptions(tempList);
    }

    public void GetSelections()
    {
        ship = shipDrop.options[shipDrop.value].text;
        mainGun = mainGunDrop.options[mainGunDrop.value].text;
        auxGun = auxGunDrop.options[auxGunDrop.value].text;
    }

    public string GetShip()
    {
        return ship;
    }

    public string GetMainGun()
    {
        return mainGun;
    }

    public string GetAuxGun()
    {
        return auxGun;
    }
}
