﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMenu : MonoBehaviour
{
    public Dropdown shipDrop, slot1Drop, slot2Drop;
    public MenuController menuController;

    private string ship, slot1, slot2;
    
    //set ship dropdown choices
    public void Init()
    {
        List<string> tempList = new List<string>();
        foreach (ShipData item in Database.ShipNamesList.Values)
        {
            tempList.Add(item.Name);
        }
        shipDrop.AddOptions(tempList);

        ship = shipDrop.options[shipDrop.value].text;
    }

    //call to reset equipment dropdown choices depending on ship chosen
    private void ResetOptions()
    {
        slot1Drop.options.RemoveRange(1, slot1Drop.options.Count - 1);
        slot2Drop.options.RemoveRange(1, slot2Drop.options.Count - 1);

        List<string> tempList = new List<string>();
        foreach (EquipmentData item in Database.GunNamesList.Values)
        {
            if (Database.ShipNamesList[ship].Slot1 == item.Type)
            {
                tempList.Add(item.Name);
            }
        }
        slot1Drop.AddOptions(tempList);

        tempList = new List<string>();
        foreach (EquipmentData item in Database.GunNamesList.Values)
        {
            if (Database.ShipNamesList[ship].Slot2 == item.Type)
            {
                tempList.Add(item.Name);
            }
        }
        slot2Drop.AddOptions(tempList);
    }

    //called when ship selection is changed, showing or hiding gun options depending on whether "none" is selected
    public void SelectionChanged()
    {
        ship = shipDrop.options[shipDrop.value].text;
        
        if (shipDrop.value != 0)
        {
            ResetOptions();
            slot1Drop.gameObject.SetActive(true);
            slot2Drop.gameObject.SetActive(true);
        }
        else
        {
            slot1Drop.gameObject.SetActive(false);
            slot2Drop.gameObject.SetActive(false);
        }

        menuController.ShowNecessaryMenus();
    }

    //sets values of each dropdown to previously saved selection, if applicable
    public void SetSelections(ShipLoadoutData loadoutData)
    {
        if (loadoutData.Ship)
        {
            SetDropdownSelection(shipDrop, loadoutData.Ship.Name);
        }
        if (loadoutData.Slot1)
        {
            SetDropdownSelection(slot1Drop, loadoutData.Slot1.Name);
        }
        if (loadoutData.Slot2)
        {
            SetDropdownSelection(slot2Drop, loadoutData.Slot2.Name);
        }
    }

    //searches the dropdown's options for option and sets the selection to it
    private void SetDropdownSelection(Dropdown dropdown, string option)
    {
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text.Equals(option))
            {
                dropdown.value = i;
                break;
            }
        }
    }

    //resets all dropdowns to none
    public void ResetAll()
    {
        shipDrop.value = 0;
        slot1Drop.value = 0;
        slot2Drop.value = 0;
    }

    public void GetSelections()
    {
        ship = shipDrop.options[shipDrop.value].text;
        slot1 = slot1Drop.options[slot1Drop.value].text;
        slot2 = slot2Drop.options[slot2Drop.value].text;
    }

    public string GetShip()
    {
        return ship;
    }

    public string GetSlot1()
    {
        return slot1;
    }

    public string GetSlot2()
    {
        return slot2;
    }
}
