using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGunController : ProjectileWeaponController
{
    public override void Init(GunData gunData)
    {
        volleyTime = gunData.VolleyTime;

        base.Init(gunData);
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator FiringLoop()
    {
        yield return new WaitForSeconds(startDelay);
        yield return new WaitForSeconds(reloadTime);

        Vector3 targetPos;

        for (; ; )
        {
            thisShip.FindNearestEnemy();
            target = thisShip.GetEnemy();

            CalculateAngles();

            //fires at target, or straight ahead if no target exists
            if (target)
            {
                targetPos = target.transform.position;
            }
            else
            {
                targetPos = transform.position + Vector3.right;
            }

            for (int y = 0; y < projPerShot; y++)
            {
                FireProjectile(targetPos, y);
            }

            yield return new WaitForSeconds(reloadTime);
        }
    }

    //calculates the final damage numbers used, based on firepower, damage, etc
    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetFirepower()) / 100));
    }

    //gives each projectile a random spread based on the gun's spread angle
    protected override void CalculateAngles()
    {
        projSpreads = new float[projPerShot];
        projOffsets = new float[projPerShot];

        for (int x = 0; x < projPerShot; x++)
        {
            projSpreads[x] = Random.Range(-spread, spread);
            projOffsets[x] = Random.Range(-projectileSpacing, projectileSpacing);
        }
    }
}
