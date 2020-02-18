using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subclass that handles only gun specifics
public class GunController : ProjectileWeaponController
{
    private int angle;

    public override void Init(GunData gunData)
    {
        volleyTime = gunData.VolleyTime;
        noOfShots = gunData.NoOfShots;
        angle = gunData.Angle;
        
        base.Init(gunData);
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator FiringLoop()
    {
        yield return new WaitForSeconds(startDelay);

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

            //nested for loops to instantiate projectiles for each shell of each wave, and play sfx
            for (int x = 0; x < noOfShots; x++)
            {
                for (int y = 0; y < projPerShot; y++)
                {
                    FireProjectile(targetPos, y);
                }
                yield return new WaitForSeconds(volleyTime);
            }
            yield return new WaitForSeconds(reloadTime);
        }
    }

    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetFirepower()) / 100));
    }
}
