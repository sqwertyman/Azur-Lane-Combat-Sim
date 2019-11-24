using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoController : WeaponController
{
    public override void Init(TorpedoData gunData)
    {
        gameObject.name = gunData.name;
        startDelay = gunData.StartDelay;
        fireRate = gunData.FireRate;
        projectile = gunData.Projectile;
        projPerShot = gunData.ProjPerShot;
        damage = gunData.Damage;
        spread = gunData.Spread;

        thisShip = GetComponentInParent<ShipController>();

        CalculateReloadTime();
        CalculateDamage();
        CalculateAngles();

        StartCoroutine(Fire());
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);

        Projectile lastProj;

        for (; ; )
        {
            for (int x = 0; x < projPerShot; x++)
            {
                lastProj = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
                lastProj.Setup(transform.position + Vector3.right, projSpreads[x], finalDamage);
            }
            yield return new WaitForSeconds(reloadTime);
        }
    }
}
