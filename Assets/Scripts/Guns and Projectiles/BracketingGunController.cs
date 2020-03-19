using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketingGunController : GunController
{
    public float individualProjDelay;

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator FiringLoop()
    {
        Vector3 targetPos;

        for (; ; )
        {
            yield return new WaitForSeconds(reloadTime);

            for (int x = 0; x < noOfMounts; x++)
            {
                yield return new WaitForSeconds(preFireTime);

                thisShip.FindNearestEnemy();
                target = thisShip.GetTarget();

                RecalculateAngles();

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
                    yield return new WaitForSeconds(individualProjDelay);
                }

                yield return new WaitForSeconds(postFireTime);
            }
        }
    }

    //gives each projectile a random spread based on the gun's spread angle
    protected override void CalculateAngles()
    {
        projSpreads = new float[projPerShot];
        projOffsets = new float[projPerShot];

        //these always the same for now so init them all first
        for (int x = 0; x < projPerShot; x++)
        {
            projSpreads[x] = spread;
        }

        RecalculateAngles();
    }

    //recalc the random spreads
    private void RecalculateAngles()
    {
        for (int x = 0; x < projPerShot; x++)
        {
            //projSpreads[x] = spread;
            projOffsets[x] = Random.Range(-projectileSpacing, projectileSpacing);
        }
    }
}
