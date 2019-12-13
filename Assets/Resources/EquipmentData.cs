using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : DatabaseItem
{
    [SerializeField]
    private EquipmentType type;
    [SerializeField]
    private int firepower, torpedo, projectileSpeed, despawnTime;
    [SerializeField]
    private Sprite sprite;

    public EquipmentType Type { get => type; set => type = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
    public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int DespawnTime { get => despawnTime; set => despawnTime = value; }
}
