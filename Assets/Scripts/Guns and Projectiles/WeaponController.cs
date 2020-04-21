using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    protected float preFireTime, postFireTime, fireRate, reloadTime, finalDamage;
    protected int damage, projectileSpeed, slotNumber, noOfMounts, efficiency;
    protected ShipController thisShip;
    protected Color dmgNumberColour;
    protected EquipmentType gunClass;
    protected Sprite sprite;
    protected AmmoType ammo;
    protected AudioSource audioSource;
    protected AmmoData ammoData;
    protected string targetTag;
    protected Vector3 targetDirection;
    protected Image cooldownBar;
    protected float cooldownTimer;

    protected WeaponData weaponData;

    //sets up data for the weapon, needs weapondata to have been set first
    //subclasses need to cast the weapondata to their appropriate equivalent to then get their specific data
    public virtual void Init()
    {
        ammoData = weaponData.Ammo;

        gameObject.name = weaponData.name;
        gunClass = weaponData.Type;
        preFireTime = weaponData.PreFireTime;
        postFireTime = weaponData.PostFireTime;
        fireRate = weaponData.FireRate;
        damage = weaponData.Damage;
        ammo = weaponData.Ammo.Ammo;
        sprite = weaponData.Sprite;
        projectileSpeed = weaponData.ProjectileSpeed;
        dmgNumberColour = weaponData.DmgNumberColour;

        thisShip = GetComponentInParent<ShipController>();
        targetTag = thisShip.GetTargetTag();
        noOfMounts = thisShip.GetSlotMounts(slotNumber);
        efficiency = thisShip.GetSlotEfficiency(slotNumber);

        audioSource = GetComponent<AudioSource>();
        if (weaponData.Sfx)
            audioSource.clip = weaponData.Sfx;

        //select direction to fire based on target
        if (targetTag == "Friendly")
            targetDirection = -Vector3.right;
        else
            targetDirection = Vector3.right;

        CalculateDamage();
        CalculateReloadTime();
    }

    public void StartFiring()
    {
        StartCoroutine(FiringLoop());
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected virtual IEnumerator FiringLoop()
    {
        yield return new WaitForSeconds(preFireTime);
    }

    protected virtual void CalculateReloadTime()
    {
        reloadTime = fireRate * (Mathf.Sqrt(200 / (thisShip.GetFireRate() + 100)));
    }

    //calculates the "finaldamage" dealt, which is used later. does not take into account enemy armour; that's done later
    protected virtual void CalculateDamage()
    {

    }

    //coroutine for weapon cooldowns. fills the weapon's bar over the period of time given by cooldown
    protected IEnumerator CooldownBarFill(float cooldown)
    {
        cooldownTimer = 0;
        while (cooldownTimer < cooldown)
        {
            cooldownBar.fillAmount = cooldownTimer / cooldown;
            cooldownTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
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

    //play's the gun's firing sfx
    protected void PlayFireSound()
    {
        audioSource.pitch = Random.Range(0.85f, 1.1f);
        audioSource.Play();
    }

    //set the weapon's data. separate from init method to prevent excessive duplicate methods, and tidy up the gun initialisations. just set mounts here as convinent
    public void SetWeaponData(WeaponData weaponData, int slotNumber)
    {
        this.weaponData = weaponData;
        this.slotNumber = slotNumber;
    }

    public float GetReload()
    {
        return reloadTime;
    }

    public int GetNoOfMounts()
    {
        return noOfMounts;
    }

    public void SetCooldownBar(Image bar)
    {
        cooldownBar = bar;
        cooldownBar.color = thisShip.GetCooldownBarColour();
    }
}
