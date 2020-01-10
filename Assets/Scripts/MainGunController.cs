using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGunController : WeaponController
{
    public override void Init(GunData gunData)
    {
        startDelay = gunData.StartDelay;
        fireRate = gunData.FireRate;
        volleyTime = gunData.VolleyTime;
        projPerShot = gunData.ProjPerShot;
        damage = gunData.Damage;
        spread = gunData.Spread;

        projSpreads = new float[projPerShot];

        base.Init(gunData);
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);
        yield return new WaitForSeconds(reloadTime);

        GameObject lastProj;
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
                lastProj = Instantiate(projectilePrefab, transform.position, transform.rotation);
                lastProj.GetComponent<BaseProjectile>().Setup(targetPos, projSpreads[y], finalDamage, projectileSpeed, sprite, range, dmgNumberColour);
            }

            yield return new WaitForSeconds(reloadTime);
        }
    }

    //calculates the final damage numbers used, based on firepower, damage, etc
    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetFirepower()) / 100)) + Random.Range(-1, 3);
    }

    //gives each projectile a random spread based on the gun's spread angle
    protected override void CalculateAngles()
    {
        for (int x = 0; x < projPerShot; x++)
        {
            projSpreads[x] = Random.Range(-spread, spread);
        }
    }
}
