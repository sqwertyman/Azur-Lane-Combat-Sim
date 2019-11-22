using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMenu : MonoBehaviour
{
    public Dropdown shipDrop, mainGunDrop, auxGunDrop;

    private string ship, mainGun, auxGun;
    
    //set ship dropdown choices
    public void Init()
    {
        List<string> tempList = new List<string>();
        foreach (ShipData item in Database.ShipNamesList.Values)
        {
            tempList.Add(item.Name);
        }
        shipDrop.AddOptions(tempList);
    }

    //call to reset equipment dropdown choices depending on ship chosen
    private void ResetOptions()
    {
        mainGunDrop.options.RemoveRange(1, mainGunDrop.options.Count - 1);
        auxGunDrop.options.RemoveRange(1, auxGunDrop.options.Count - 1);

        List<string> tempList = new List<string>();
        tempList = new List<string>();
        foreach (GunData item in Database.GunNamesList.Values)
        {
            if (Database.ShipNamesList[ship].Slot1 == item.Type)
            {
                tempList.Add(item.Name);
            }
        }
        mainGunDrop.AddOptions(tempList);

        tempList = new List<string>();
        foreach (GunData item in Database.GunNamesList.Values)
        {
            if (Database.ShipNamesList[ship].Slot2 == item.Type)
            {
                tempList.Add(item.Name);
            }
        }
        auxGunDrop.AddOptions(tempList);
    }

    public void GetSelections()
    {
        ship = shipDrop.options[shipDrop.value].text;
        mainGun = mainGunDrop.options[mainGunDrop.value].text;
        auxGun = auxGunDrop.options[auxGunDrop.value].text;
    }

    //called when ship selection is changed, showing or hiding gun options depending on whether "none" is selected
    public void SelectionChanged()
    {
        if (shipDrop.value != 0)
        {
            ship = shipDrop.options[shipDrop.value].text;
            ResetOptions();
            mainGunDrop.gameObject.SetActive(true);
            auxGunDrop.gameObject.SetActive(true);
        }
        else
        {
            mainGunDrop.gameObject.SetActive(false);
            auxGunDrop.gameObject.SetActive(false);
        }
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
