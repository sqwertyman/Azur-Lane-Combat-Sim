using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponData : WeaponData
{
    [Header("Projectile Weapon Data")]
    [SerializeField]
    private int firingRange;
    [SerializeField]
    private int projRange, projPerShot, spread;

    public int Spread { get => spread; set => spread = value; }
    public int ProjPerShot { get => projPerShot; set => projPerShot = value; }
    public int FiringRange { get => firingRange; set => firingRange = value; }
    public int ProjRange { get => projRange; set => projRange = value; }
}
