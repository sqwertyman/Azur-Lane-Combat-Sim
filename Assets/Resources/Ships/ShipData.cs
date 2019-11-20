using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship")]

public class ShipData : DatabaseItem
{
    [SerializeField]
    private int health, speed, reload, firepower;

    public int Health { get => health; set => health = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Reload { get => reload; set => reload = value; }
    public int Firepower { get => firepower; set => firepower = value; }
}
