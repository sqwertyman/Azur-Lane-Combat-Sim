using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : DatabaseItem
{
    [Header("General Weapon Data")]
    [SerializeField]
    private EquipmentType type;
    [SerializeField]
    private int firepower, torpedo, damage;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float fireRate;

    public EquipmentType Type { get => type; set => type = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
    public float StartDelay { get => startDelay; set => startDelay = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Damage { get => damage; set => damage = value; }
}
