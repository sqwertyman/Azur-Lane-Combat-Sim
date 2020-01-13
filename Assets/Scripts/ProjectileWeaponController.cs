using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that handles a ship's weapon, e.g. DD gun or torpedo.
public class ProjectileWeaponController : WeaponController
{
    [SerializeField]
    protected GameObject projectilePrefab;

    protected float volleyTime, spread;
    protected Sprite sprite;
    protected int noOfShots, projPerShot, projectileSpeed, range;
    protected GameObject target;
    protected float[] projSpreads;
    protected AmmoType ammo;

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
        dmgNumberColour = gunData.DmgNumberColour;
        sprite = gunData.Sprite;
        projectileSpeed = gunData.ProjectileSpeed;
        range = gunData.Range * 2; //mulitplied to exaggerate for now
        ammo = gunData.Ammo.Ammo;
        projPerShot = gunData.ProjPerShot;
        spread = gunData.Spread;

        CalculateAngles();
        
        base.Init(gunData);
    }

    //populates projSpreads array with angles for each projectile to be fired at
    protected virtual void CalculateAngles()
    {
        projSpreads = new float[projPerShot];

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
            }
            else
            {
                projSpreads[y] = -spreadPerProj * spreadCounter;
                spreadCounter++;
            }
        }
        projSpreads[projPerShot - 1] = 0f;

        //shift all angles by half spread if even number of projectiles
        if (projPerShot % 2 == 0)
        {
            float halfSpread = spreadPerProj / 2;
            for (int x = 0; x < projSpreads.Length; x++)
            {
                projSpreads[x] -= halfSpread;
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

    //instantiates the projectile prefab, giving it necessary data
    protected virtual void SpawnProjectile(Vector3 targetPosition, int projNumber)
    {
        GameObject lastProj = Instantiate(projectilePrefab, transform.position, transform.rotation);
        lastProj.GetComponent<BaseProjectile>().Setup(targetPosition, projSpreads[projNumber], projectileSpeed, sprite, range, gameObject);
    }
}
