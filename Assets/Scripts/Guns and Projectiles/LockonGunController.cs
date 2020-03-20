using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonGunController : GunController
{
    //fires pattern at regular intervals, based on startDelay, fireRate, etc. fires towards target
    protected override IEnumerator FiringLoop()
    {
        Vector3 targetPos;
        bool useOffset;

        for (; ; )
        {
            //waits until nearest enemy is within the gun's range
            yield return new WaitUntil(() => DistanceToNearest() <= firingRange);

            for (int x = 0; x < noOfMounts; x++)
            {
                yield return new WaitForSeconds(preFireTime);

                //target enemy position, or straight ahead if no target or out of firing angle
                float targetAngle = Vector2.Angle(targetDirection, target.transform.position - transform.position);
                if (target && targetAngle <= (angle / 2))
                {
                    targetPos = target.transform.position;
                    useOffset = false;
                }
                else
                {
                    targetPos = transform.position + targetDirection;
                    useOffset = true;
                }

                //nested for loops to instantiate projectiles for each shell of each wave, and play sfx
                for (int y = 0; y < noOfShots; y++)
                {
                    for (int z = 0; z < projPerShot; z++)
                    {
                        FireProjectile(targetPos, z, useOffset);
                    }
                    yield return new WaitForSeconds(volleyTime);
                }
                yield return new WaitForSeconds(postFireTime);
            }

            yield return new WaitForSeconds(reloadTime);
        }
    }
}
