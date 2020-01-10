using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that handles a ship's weapon, e.g. DD gun or torpedo.
public class WeaponController : MonoBehaviour
{
    [SerializeField]
    protected GameObject projectilePrefab;

    protected float startDelay, fireRate, volleyTime, reloadTime, spread;
    protected Sprite sprite;
    protected int noOfShots, projPerShot, damage, finalDamage, projectileSpeed, range, angle;
    protected GameObject target;
    protected ShipController thisShip;
    protected float[] projSpreads;
    protected Color dmgNumberColour;
    protected EquipmentType gunClass;
    protected AmmoType ammo;

    public virtual void Init(WeaponData gunData)
    {
        GeneralInit(gunData);
    }

    public virtual void Init(GunData gunData)
    {
        GeneralInit(gunData);
    }

    public virtual void Init(TorpedoData gunData)
    {
        GeneralInit(gunData);
    }

    //init method for any weapon type. each specific init calls this. whole system feels messy at the moment
    private void GeneralInit(WeaponData data)
    {
        gameObject.name = data.name;
        sprite = data.Sprite;
        projectileSpeed = data.ProjectileSpeed;
        dmgNumberColour = data.DmgNumberColour;
        range = data.Range * 2; //mulitplied to exaggerate for now
        gunClass = data.Type;
        ammo = data.Ammo.Ammo;
        startDelay = data.StartDelay;
        fireRate = data.FireRate;
        projPerShot = data.ProjPerShot;
        damage = data.Damage;
        spread = data.Spread;

        thisShip = GetComponentInParent<ShipController>();

        CalculateReloadTime();
        CalculateDamage();
        CalculateAngles();

        StartCoroutine(Fire());
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected virtual IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);
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

    //calculates the weapon's damage
    protected void CalculateReloadTime()
    {
        reloadTime = fireRate * (Mathf.Sqrt(200 / (thisShip.GetFireRate() + 100)));
    }

    protected virtual void CalculateDamage()
    {
        
    }

    //returns the damage (rounded to int) of the gun to the armour type passed in (with random variation too)
    public int GetDamage(ArmourType armour)
    {
        float multiplier = Database.ArmourMultiplier(gunClass, armour, ammo);
        return ((int)((finalDamage * multiplier)) + Random.Range(-1, 3));
    }

    public Color GetDmgNumberColour()
    {
        return dmgNumberColour;
    }

    //instantiates the projectile prefab, giving it necessary data
    protected virtual void SpawnProjectile(Vector3 targetPosition, int projNumber)
    {
        GameObject lastProj = Instantiate(projectilePrefab, transform.position, transform.rotation);
        lastProj.GetComponent<BaseProjectile>().Setup(targetPosition, projSpreads[projNumber], projectileSpeed, sprite, range, gameObject);
    }
}
