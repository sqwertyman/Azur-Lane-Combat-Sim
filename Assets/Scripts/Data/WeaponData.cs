using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : EquipmentData
{
    [Header("General Weapon Data")]
    [SerializeField]
    private int damage;
    [SerializeField]
    private float preFireTime, postFireTime;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private AmmoData ammo;
    [SerializeField]
    private AudioClip sfx;
    
    public float PreFireTime { get => preFireTime; set => preFireTime = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Damage { get => damage; set => damage = value; }
    public AmmoData Ammo { get => ammo; set => ammo = value; }
    public Sprite ProjectileSprite { get => ammo.Sprite; set => ammo.Sprite = value; }
    public Color DmgNumberColour { get => ammo.DmgNumberColour; set => ammo.DmgNumberColour = value; }
    public int ProjectileSpeed { get => ammo.ProjectileSpeed; set => ammo.ProjectileSpeed = value; }
    public AudioClip Sfx { get => sfx; set => sfx = value; }
    public float PostFireTime { get => postFireTime; set => postFireTime = value; }
}
