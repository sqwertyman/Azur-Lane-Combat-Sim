using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoController : WeaponController
{
    public override void Init(TorpedoData torpData)
    {
        gameObject.name = torpData.name;
        startDelay = torpData.StartDelay;
        fireRate = torpData.FireRate;
        projectile = torpData.Projectile;
        projPerShot = torpData.ProjPerShot;
        damage = torpData.Damage;
        spread = torpData.Spread;

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

    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetTorpedo()) / 100));
    }
}
