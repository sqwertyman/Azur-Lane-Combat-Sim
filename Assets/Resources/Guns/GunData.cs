using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : DatabaseItem
{
    [SerializeField]
    private float startDelay, fireRate, volleyTime;
    [SerializeField]
    private int noOfShots, projPerShot, damage, spread;
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private GunType type;

    public float StartDelay { get => startDelay; set => startDelay = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float VolleyTime { get => volleyTime; set => volleyTime = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Spread { get => spread; set => spread = value; }
    public int NoOfShots { get => noOfShots; set => noOfShots = value; }
    public int ProjPerShot { get => projPerShot; set => projPerShot = value; }
    public Projectile Projectile { get => projectile; set => projectile = value; }
    public GunType Type { get => type; set => type = value; }
}
