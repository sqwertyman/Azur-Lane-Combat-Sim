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
        angle = gunData.Angle;

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
            //waits until nearest enemy is within the gun's range
            yield return new WaitUntil(() => DistanceToNearest() <= range);

            //target enemy position, or straight ahead if no target or out of firing angle
            float targetAngle = Vector2.Angle(Vector2.right, target.transform.position - transform.position);
            if (target && targetAngle <= (angle / 2))
            {
                targetPos = target.transform.position;
            }
            else
            {
                targetPos = transform.position + Vector3.right;
            }

            //nested for loops to instantiate projectiles for each shell of each wave
            for (int x = 0; x < noOfShots; x++)
            {
                for (int y = 0; y < projPerShot; y++)
                {
                    lastProj = Instantiate(projectilePrefab, transform.position, transform.rotation);
                    lastProj.GetComponent<BaseProjectile>().Setup(targetPos, projSpreads[y], finalDamage, projectileSpeed, sprite, range);
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
