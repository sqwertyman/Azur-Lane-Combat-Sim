using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : DatabaseItem
{
    [Header("General Weapon Data")]
    [SerializeField]
    private EquipmentType type;
    [SerializeField]
    private int firepower, torpedo, range;
    [SerializeField]
    private AmmoData ammo;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int projPerShot, damage, spread;

    public int Damage { get => damage; set => damage = value; }
    public int Spread { get => spread; set => spread = value; }
    public int ProjPerShot { get => projPerShot; set => projPerShot = value; }
    public EquipmentType Type { get => type; set => type = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
    public int ProjectileSpeed { get; set; }
    public Sprite Sprite { get; set; }
    public int Range { get => range; set => range = value; }
    public Color DmgNumberColour { get; set; }
    public AmmoData Ammo { get => ammo; set => ammo = value; }
    public float StartDelay { get => startDelay; set => startDelay = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }

    private void OnEnable()
    {
        Sprite = ammo.Sprite;
        ProjectileSpeed = ammo.ProjectileSpeed;
        DmgNumberColour = ammo.DmgNumberColour;
    }
}
