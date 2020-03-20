using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subclass that handles only torpedo specifics
public class TorpedoController : ProjectileWeaponController
{
    public override void Init()
    {
        //base init first as targettag needs to be set
        base.Init();
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator FiringLoop()
    {
        for (; ; )
        {
            //thisShip.FindNearestEnemy();
            
            yield return new WaitForSeconds(preFireTime);

            for (int y = 0; y < projPerShot; y++)
            {
                FireProjectile(transform.position + targetDirection, y, true);
            }

            yield return new WaitForSeconds(postFireTime);

            yield return new WaitForSeconds(reloadTime);
        }
    }

    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetTorpedo()) / 100));
    }
}
