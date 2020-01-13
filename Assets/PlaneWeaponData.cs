using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plane", menuName = "Plane")]
public class PlaneWeaponData : WeaponData
{
    [Header("Plane Specific Data")]
    [SerializeField]
    private int speed;

    public int Speed { get => speed; set => speed = value; }
}
