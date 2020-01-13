using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponData : WeaponData
{
    [Header("Projectile Weapon Data")]
    [SerializeField]
    private int range;
    [SerializeField]
    private int projPerShot, spread, projectileSpeed;
    [SerializeField]
    private AmmoData ammo;

    private Sprite sprite;
    private Color dmgNumberColour;

    public int Spread { get => spread; set => spread = value; }
    public int ProjPerShot { get => projPerShot; set => projPerShot = value; }
    public int Range { get => range; set => range = value; }
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
