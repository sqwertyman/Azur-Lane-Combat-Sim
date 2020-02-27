using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScattershotGunController : GunController
{
    private Vector3 targetDirection;

    public override void Init()
    {
        //base init first as targettag needs to be set
        base.Init();

        //select direction to fire based on target
        if (targetTag == "Friendly")
            targetDirection = -Vector3.right;
        else
            targetDirection = Vector3.right;
    }

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
                FireProjectile(transform.position + targetDirection, x);
            }
            yield return new WaitForSeconds(reloadTime);
        }
    }
}
