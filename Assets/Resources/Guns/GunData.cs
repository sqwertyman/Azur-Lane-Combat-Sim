using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : EquipmentData
{
    [Header("Gun Specific Data")]
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float volleyTime;
    [SerializeField]
    private float spread;
    [SerializeField]
    private int noOfShots, projPerShot, damage, angle;

    public float StartDelay { get => startDelay; set => startDelay = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float VolleyTime { get => volleyTime; set => volleyTime = value; }
    public int Damage { get => damage; set => damage = value; }
    public float Spread { get => spread; set => spread = value; }
    public int NoOfShots { get => noOfShots; set => noOfShots = value; }
    public int ProjPerShot { get => projPerShot; set => projPerShot = value; }
    public int Angle { get => angle; set => angle = value; }
}
