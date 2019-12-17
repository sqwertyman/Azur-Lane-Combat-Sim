using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subclass that handles only torpedo specifics
public class TorpedoController : WeaponController
{
    public override void Init(TorpedoData torpData)
    {
        startDelay = torpData.StartDelay;
        fireRate = torpData.FireRate;
        projPerShot = torpData.ProjPerShot;
        damage = torpData.Damage;
        spread = torpData.Spread;

        base.Init(torpData);
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected override IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);

        GameObject lastProj;

        for (; ; )
        {
            thisShip.FindNearestEnemy();

            for (int x = 0; x < projPerShot; x++)
            {
                lastProj = Instantiate(projectilePrefab, transform.position, transform.rotation);
                lastProj.GetComponent<LinearProjectile>().Setup(transform.position + Vector3.right, projSpreads[x], finalDamage, projectileSpeed, sprite, despawnTime);
            }
            yield return new WaitForSeconds(reloadTime);
        }
    }

    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetTorpedo()) / 100));
    }
}
