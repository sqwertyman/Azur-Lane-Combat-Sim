using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subclass that handles only torpedo specifics
public class TorpedoController : ProjectileWeaponController
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

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator FiringLoop()
    {
        for (; ; )
        {
            //thisShip.FindNearestEnemy();

            yield return new WaitForSeconds(preFireTime);

            for (int x = 0; x < projPerShot; x++)
            {
                FireProjectile(transform.position + targetDirection, x);
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
