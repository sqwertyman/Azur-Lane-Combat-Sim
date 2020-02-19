using System.Collections;
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
    protected int noOfShots, projPerShot, range;
    protected GameObject target;
    protected float[] projSpreads;
    protected float[] projOffsets;

    public virtual void Init(GunData gunData)
    {
        Init(gunData as ProjectileWeaponData);
    }

    public virtual void Init(TorpedoData torpdata)
    {
        Init(torpdata as ProjectileWeaponData);
    }

    public override void Init(ProjectileWeaponData gunData)
    {
        range = gunData.Range * 2; //mulitplied to exaggerate for now
        projPerShot = gunData.ProjPerShot;
        spread = gunData.Spread;

        CalculateAngles();
        
        base.Init(gunData);
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
        target = thisShip.GetEnemy();

        Vector3 heading = target.transform.position - transform.position;
        return heading.magnitude;
    }

    //returns the damage (rounded to int) of the gun to the armour type passed in (with random variation too)
    public override int GetDamage(ArmourType armour)
    {
        float multiplier = Database.ArmourMultiplier(gunClass, armour, ammo);
        return ((int)((finalDamage * multiplier)) + Random.Range(-1, 3));
    }

    //turns the gun to look at target, instantiates the projectile prefab with data, and plays sound
    protected virtual void FireProjectile(Vector3 targetPosition, int projNumber)
    {
        Vector3 offset = transform.up * projOffsets[projNumber];
        transform.LookAt(targetPosition);

        GameObject lastProj = Instantiate(projectilePrefab, transform.position + offset, Quaternion.identity);
        lastProj.GetComponent<BaseProjectile>().Setup(targetPosition + offset, projSpreads[projNumber], projectileSpeed, sprite, range, gameObject);

        PlayFireSound();
    }
}
