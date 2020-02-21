using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScattershotGunController : GunController
{
    //fires straight ahead
    protected override IEnumerator FiringLoop()
    {
        yield return new WaitForSeconds(startDelay);

        for (; ; )
        {
            //waits until nearest enemy is within the gun's range
            yield return new WaitUntil(() => DistanceToNearest() <= range);

            for (int x = 0; x < projPerShot; x++)
            {
                FireProjectile(transform.position + Vector3.right, x);
            }
            yield return new WaitForSeconds(reloadTime);
        }
    }
}
