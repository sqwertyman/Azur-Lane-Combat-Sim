using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : WeaponController
{
    public override void Init(GunData gunData)
    {
        gameObject.name = gunData.name;
        startDelay = gunData.StartDelay;
        fireRate = gunData.FireRate;
        volleyTime = gunData.VolleyTime;
        projectile = gunData.Projectile;
        noOfShots = gunData.NoOfShots;
        projPerShot = gunData.ProjPerShot;
        damage = gunData.Damage;
        spread = gunData.Spread;

        thisShip = GetComponentInParent<ShipController>();

        CalculateReloadTime();
        CalculateDamage();
        CalculateAngles();

        StartCoroutine(Fire());
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);

        Projectile lastProj;

        for (; ;)
        {
            target = thisShip.GetEnemy();

            for (int x = 0; x < noOfShots; x++)
            {
                for (int y = 0; y < projPerShot; y++)
                {
                    lastProj = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
                    lastProj.Setup(target.transform.position, projSpreads[y], finalDamage);
                }
                yield return new WaitForSeconds(volleyTime);
            }
            yield return new WaitForSeconds(reloadTime);
        }
    }

    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetFirepower()) / 100)) + Random.Range(-1, 3);
    }
}
