using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Torpedo", menuName = "Torpedo")]
public class TorpedoData : EquipmentData
{
    [Header("Torpedo Specific Data")]
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int projPerShot, damage, spread;

    public float StartDelay { get => startDelay; set => startDelay = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Spread { get => spread; set => spread = value; }
    public int ProjPerShot { get => projPerShot; set => projPerShot = value; }
}
