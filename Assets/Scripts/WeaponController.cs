﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected float startDelay, fireRate, reloadTime;
    protected int damage, finalDamage, projectileSpeed;
    protected ShipController thisShip;
    protected Color dmgNumberColour;
    protected EquipmentType gunClass;
    protected Sprite sprite;
    protected AmmoType ammo;

    public virtual void Init(ProjectileWeaponData gunData)
    {
        BaseInit(gunData);
    }

    public virtual void Init(PlaneWeaponData planeData)
    {
        BaseInit(planeData);
    }

    //init method for any weapon type. each specific init calls this. whole system feels messy at the moment
    private void BaseInit(WeaponData weaponData)
    {
        gameObject.name = weaponData.name;
        gunClass = weaponData.Type;
        startDelay = weaponData.StartDelay;
        fireRate = weaponData.FireRate;
        damage = weaponData.Damage;
        ammo = weaponData.Ammo.Ammo;
        sprite = weaponData.Sprite;
        projectileSpeed = weaponData.ProjectileSpeed;
        dmgNumberColour = weaponData.DmgNumberColour;

        thisShip = GetComponentInParent<ShipController>();

        CalculateDamage();
        CalculateReloadTime();

        StartCoroutine(Fire());
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected virtual IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);
    }

    protected void CalculateReloadTime()
    {
        reloadTime = fireRate * (Mathf.Sqrt(200 / (thisShip.GetFireRate() + 100)));
    }

    //calculates the "finaldamage" dealt, which is used later. does not take into account enemy armour; that's done later
    protected virtual void CalculateDamage()
    {

    }

    //returns the damage (rounded to int) of the gun to the armour type passed in (with random variation too)
    public virtual int GetDamage(ArmourType armour)
    {
        return 0;
    }

    public Color GetDmgNumberColour()
    {
        return dmgNumberColour;
    }
}