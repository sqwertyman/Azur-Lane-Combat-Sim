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

    private Sprite sprite;
    private Color dmgNumberColour;
    private int projectileSpeed;

    public EquipmentType Type { get => type; set => type = value; }
    public int Firepower { get => firepower; set => firepower = value; }
    public int Torpedo { get => torpedo; set => torpedo = value; }
    public float StartDelay { get => startDelay; set => startDelay = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Damage { get => damage; set => damage = value; }
    public AmmoData Ammo { get => ammo; set => ammo = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public Color DmgNumberColour { get => dmgNumberColour; set => dmgNumberColour = value; }
    public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }

    private void OnEnable()
    {
        Sprite = ammo.Sprite;
        ProjectileSpeed = ammo.ProjectileSpeed;
        DmgNumberColour = ammo.DmgNumberColour;
    }
}
