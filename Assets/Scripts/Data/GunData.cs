﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ProjectileWeaponData
{
    [Header("Gun Specific Data")]
    [SerializeField]
    private FiringType firingType;
    [SerializeField]
    private float volleyTime;
    [SerializeField]
    private int noOfShots, angle;

    public float VolleyTime { get => volleyTime; set => volleyTime = value; }
    public int NoOfShots { get => noOfShots; set => noOfShots = value; }
    public int Angle { get => angle; set => angle = value; }
    public FiringType FiringType { get => firingType; set => firingType = value; }
}
