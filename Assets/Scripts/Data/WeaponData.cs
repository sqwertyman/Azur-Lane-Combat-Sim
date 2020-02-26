using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : DatabaseItem
{
    [Header("General Weapon Data")]
    [SerializeField]
    private EquipmentType type;
    [SerializeField]
    private int firepower, torpedo, damage;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private AmmoData ammo;
    [SerializeField]
    private AudioClip sfx;
    
    public EquipmentType Type { get => type; set => type = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
    public float StartDelay { get => startDelay; set => startDelay = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Damage { get => damage; set => damage = value; }
    public AmmoData Ammo { get => ammo; set => ammo = value; }
    public Sprite Sprite { get => ammo.Sprite; set => ammo.Sprite = value; }
    public Color DmgNumberColour { get => ammo.DmgNumberColour; set => ammo.DmgNumberColour = value; }
    public int ProjectileSpeed { get => ammo.ProjectileSpeed; set => ammo.ProjectileSpeed = value; }
    public AudioClip Sfx { get => sfx; set => sfx = value; }
}
