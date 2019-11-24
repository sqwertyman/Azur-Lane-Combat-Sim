using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship")]

public class ShipData : DatabaseItem
{
    [SerializeField]
    private int health, speed, reload, firepower;
    [SerializeField]
    private EquipmentType slot1, slot2;

    public int Health { get => health; set => health = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Reload { get => reload; set => reload = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public EquipmentType Slot1 { get => slot1; set => slot1 = value; }
    public EquipmentType Slot2 { get => slot2; set => slot2 = value; }
}
