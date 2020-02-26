using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Loadout", menuName = "Ship Loadout")]

public class ShipLoadoutData : ScriptableObject
{
    [SerializeField]
    private ShipData ship;
    [SerializeField]
    private WeaponData slot1, slot2;

    public ShipData Ship { get => ship; set => ship = value; }
    public WeaponData Slot1 { get => slot1; set => slot1 = value; }
    public WeaponData Slot2 { get => slot2; set => slot2 = value; }

    public string GetJson()
    {
        return JsonUtility.ToJson(this);
    }
}
