using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship")]

public class ShipData : DatabaseItem
{
    [Header("Ship Data")]
    [SerializeField]
    private FleetType fleetType;
    [SerializeField]
    private Class shipClass;
    [SerializeField]
    private ArmourType armour;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int health, speed, reload, firepower, torpedo, aviation, accuracy, evasion, luck;
    [SerializeField]
    private EquipmentType[] slot1 = new EquipmentType[1];
    [SerializeField]
    private int noOfSlot1 = 1;
    [SerializeField]
    private EquipmentType[] slot2 = new EquipmentType[1];
    [SerializeField]
    private int noOfSlot2 = 1;

    public int Health { get => health; set => health = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Reload { get => reload; set => reload = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public EquipmentType[] Slot1 { get => slot1; set => slot1 = value; }
    public EquipmentType[] Slot2 { get => slot2; set => slot2 = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
    public FleetType FleetType { get => fleetType; set => fleetType = value; }
    public ArmourType Armour { get => armour; set => armour = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public Class ShipClass { get => shipClass; set => shipClass = value; }
    public int NoOfSlot1 { get => noOfSlot1; set => noOfSlot1 = value; }
    public int NoOfSlot2 { get => noOfSlot2; set => noOfSlot2 = value; }
    public int Aviation { get => aviation; set => aviation = value; }
    public int Accuracy { get => accuracy; set => accuracy = value; }
    public int Evasion { get => evasion; set => evasion = value; }
    public int Luck { get => luck; set => luck = value; }
}
