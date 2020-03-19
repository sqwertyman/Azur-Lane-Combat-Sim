﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScattershotGunController : GunController
{
    //fires straight ahead
    protected override IEnumerator FiringLoop()
    {
        for (; ; )
        {
            //waits until nearest enemy is within the gun's range
            yield return new WaitUntil(() => DistanceToNearest() <= firingRange);

            for (int x = 0; x < noOfMounts; x++)
            {
                yield return new WaitForSeconds(preFireTime);

                //need nested for >2 waves?
                for (int y = 0; y < projPerShot; y++)
                {
                    FireProjectile(transform.position + targetDirection, y);
                }
                yield return new WaitForSeconds(postFireTime);
            }

            yield return new WaitForSeconds(reloadTime);
        }
    }
}
