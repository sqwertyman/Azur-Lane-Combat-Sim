using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship")]

public class ShipData : DatabaseItem
{
    [Header("Ship Data")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int speed;
    [SerializeField]
    private int reload;
    [SerializeField]
    private int firepower;
    [SerializeField]
    private int torpedo;
    [SerializeField]
    private EquipmentType[] slot1 = new EquipmentType[1];
    [SerializeField]
    private EquipmentType[] slot2 = new EquipmentType[1];
    [SerializeField]
    private FleetType fleetType;

    public int Health { get => health; set => health = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Reload { get => reload; set => reload = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public EquipmentType[] Slot1 { get => slot1; set => slot1 = value; }
    public EquipmentType[] Slot2 { get => slot2; set => slot2 = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
    public FleetType FleetType { get => fleetType; set => fleetType = value; }
}
