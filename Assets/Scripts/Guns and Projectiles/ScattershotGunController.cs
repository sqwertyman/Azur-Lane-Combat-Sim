using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScattershotGunController : GunController
{
    //fires straight ahead
    protected override IEnumerator FiringLoop()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(preFireTime);

            //waits until nearest enemy is within the gun's range
            yield return new WaitUntil(() => DistanceToNearest() <= range);

            //need nested for >2 waves?
            for (int x = 0; x < projPerShot; x++)
            {
                FireProjectile(transform.position + targetDirection, x);
            }

            yield return new WaitForSeconds(postFireTime);

            yield return new WaitForSeconds(reloadTime);
        }
    }
}
