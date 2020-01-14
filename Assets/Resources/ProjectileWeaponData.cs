using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponData : WeaponData
{
    [Header("Projectile Weapon Data")]
    [SerializeField]
    private int range;
    [SerializeField]
    private int projPerShot, spread;

    public int Spread { get => spread; set => spread = value; }
    public int ProjPerShot { get => projPerShot; set => projPerShot = value; }
    public int Range { get => range; set => range = value; }
}
