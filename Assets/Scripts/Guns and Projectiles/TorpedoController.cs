using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        cooldownBar.fillAmount = 1;

        for (; ; )
        {
            //thisShip.FindNearestEnemy();
            
            yield return new WaitForSeconds(preFireTime);

            for (int y = 0; y < projPerShot; y++)
            {
                FireProjectile(transform.position + targetDirection, y, true);
            }

            yield return new WaitForSeconds(postFireTime);

            StartCoroutine(CooldownBarFill(reloadTime));
            yield return new WaitForSeconds(reloadTime);
        }
    }  

    protected override void CalculateDamage()
    {
        finalDamage = damage * (efficiency / 100f) * ((100 + thisShip.GetTorpedo()) / 100f);
    }
}
