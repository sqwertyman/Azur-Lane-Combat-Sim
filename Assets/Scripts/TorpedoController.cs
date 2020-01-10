using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subclass that handles only torpedo specifics
public class TorpedoController : WeaponController
{
    public override void Init(TorpedoData torpData)
    {
        base.Init(torpData);
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);

        for (; ; )
        {
            //thisShip.FindNearestEnemy();

            for (int x = 0; x < projPerShot; x++)
            {
                SpawnProjectile(transform.position + Vector3.right, x);
            }
            yield return new WaitForSeconds(reloadTime);
        }
    }

    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetTorpedo()) / 100));
    }
}
