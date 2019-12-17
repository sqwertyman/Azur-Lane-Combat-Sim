using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subclass that handles only gun specifics
public class GunController : WeaponController
{
    public override void Init(GunData gunData)
    {
        startDelay = gunData.StartDelay;
        fireRate = gunData.FireRate;
        volleyTime = gunData.VolleyTime;
        noOfShots = gunData.NoOfShots;
        projPerShot = gunData.ProjPerShot;
        damage = gunData.Damage;
        spread = gunData.Spread;

        base.Init(gunData);
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);

        GameObject lastProj;
        Vector3 targetPos;

        for (; ;)
        {
            thisShip.FindNearestEnemy();
            target = thisShip.GetEnemy();

            for (int x = 0; x < noOfShots; x++)
            {
                for (int y = 0; y < projPerShot; y++)
                {
                    lastProj = Instantiate(projectilePrefab, transform.position, transform.rotation);

                    //fires at target, or straight ahead if no target exists
                    if (target)
                    {
                        targetPos = target.transform.position;
                    }
                    else
                    {
                        targetPos = transform.position + Vector3.right;
                    }
                    lastProj.GetComponent<BaseProjectile>().Setup(targetPos, projSpreads[y], finalDamage, projectileSpeed, sprite, despawnTime);
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
