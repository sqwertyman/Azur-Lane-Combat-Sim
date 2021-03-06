﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines some stuffs for projectile weapons (guns, torps, etc). shouldn't be directly used by object, only its subclasses should
public class ProjectileWeaponController : WeaponController
{
    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected float projectileSpacing;

    protected float volleyTime, spread;
    protected int noOfShots, projPerShot, firingRange, projRange;
    protected GameObject target;
    protected float[] projSpreads;
    protected float[] projOffsets;

    public override void Init()
    {
        ProjectileWeaponData newTempData = weaponData as ProjectileWeaponData;
        firingRange = newTempData.FiringRange * 2; //mulitplied to exaggerate for now
        projRange = newTempData.ProjRange * 2;  //same here
        projPerShot = newTempData.ProjPerShot;
        spread = newTempData.Spread;

        CalculateAngles();

        base.Init();
    }

    //populates projSpreads array with angles for each projectile to be fired at
    protected virtual void CalculateAngles()
    {
        projSpreads = new float[projPerShot];
        projOffsets = new float[projPerShot];

        //calc spread between projectiles, or set to 0 if only 1 projectile
        float spreadPerProj = 0;
        if (projPerShot > 1)
        {
            spreadPerProj = spread / (projPerShot - 1);
        }

        int spreadCounter = 1;
        for (int y = 0; y < projPerShot; y++)
        {
            if (y % 2 == 0)
            {
                projSpreads[y] = spreadPerProj * spreadCounter;
                projOffsets[y] = projectileSpacing * spreadCounter;
            }
            else
            {
                projSpreads[y] = -spreadPerProj * spreadCounter;
                projOffsets[y] = -projectileSpacing * spreadCounter;
                spreadCounter++;
            }
        }
        projSpreads[projPerShot - 1] = 0f;
        projOffsets[projPerShot - 1] = 0f;

        //shift all angles by half spread if even number of projectiles
        if (projPerShot % 2 == 0)
        {
            float halfSpread = spreadPerProj / 2;
            float halfOffset = projectileSpacing / 2;
            for (int x = 0; x < projPerShot; x++)
            {
                projSpreads[x] -= halfSpread;
                projOffsets[x] -= halfOffset;
            }
        }
    }

    //returns the distance to the nearest enemy
    protected float DistanceToNearest()
    {
        thisShip.FindNearestEnemy();
        target = thisShip.GetTarget();
        Vector3 heading;

        //stops errors. positiveinfinity makes it seem as if nothing is in range
        if (target)
            heading = target.transform.position - transform.position;
        else
            heading = Vector3.positiveInfinity;

        return heading.magnitude;
    }

    //returns the damage (rounded to int) of the gun to the armour type passed in (with random variation too)
    public override int GetDamage(ArmourType armour)
    {
        float multiplier = Database.ArmourMultiplier(gunClass, armour, ammo);
        //clamp with max to keep number non-negative (due to random)
        return Mathf.Max(0, ((int)((finalDamage * multiplier)) + Random.Range(-1, 3)));
    }

    //instantiates the projectile prefab with data, and plays sound. default
    protected virtual void FireProjectile(Vector3 targetPosition, int projNumber, bool useOffset)
    {
        Vector3 offset = transform.up * projOffsets[projNumber];

        //bool given to determine whether to adjust target position by the proj's offset. fixes issues when firing straight ahead
        if (useOffset)
            targetPosition += offset;
        
        GameObject lastProj = Instantiate(projectilePrefab, transform.position + offset, Quaternion.identity);
        lastProj.GetComponent<BaseProjectile>().Setup(targetPosition, projSpreads[projNumber], ammoData, projRange, gameObject, targetTag);
    }
}
