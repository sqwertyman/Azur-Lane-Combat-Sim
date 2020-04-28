using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AA Gun", menuName = "Anti Air Gun")]
public class AntiAirData : WeaponData
{
    [SerializeField]
    private int range;

    public int Range { get => range; set => range = value; }
}
