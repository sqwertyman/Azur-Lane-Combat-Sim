using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMenu : MonoBehaviour
{
    public Dropdown shipDrop, slot1Drop, slot2Drop;
    public FleetMenuController menuController;

    private string ship, slot1, slot2;

    //set ship dropdown choices
    public void Init(FleetType type)
    {
        List<string> tempList = new List<string>();

        foreach (ShipData ship in Database.ShipList.Values)
        {
            if (ship.FleetType == type)
            {
                tempList.Add(ship.Name);
            }
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
        foreach (WeaponData gun in Database.GunList.Values)
        {
            foreach (EquipmentType type in Database.ShipList[ship].Slot1)
            {
                if (type == gun.Type)
                {
                    tempList.Add(gun.Name);
                }
            }
        }
        slot1Drop.AddOptions(tempList);

        tempList = new List<string>();
        foreach (WeaponData gun in Database.GunList.Values)
        {
            foreach (EquipmentType type in Database.ShipList[ship].Slot2)
            {
                if (type == gun.Type)
                {
                    tempList.Add(gun.Name);
                }
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

    //force the selections to be updated
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
