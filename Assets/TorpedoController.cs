using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoController : MonoBehaviour
{
    private float startDelay, fireRate;
    private Projectile projectile;
    private int projPerShot, damage, spread;

    private ShipController thisShip;
    private float reloadTime;
    private int finalDamage;
    private float[] projSpreads;

    public void Init(GunData gunData)
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
    protected virtual IEnumerator Fire()
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

    //populates projSpreads array with angles for each projectile to be fired at
    void CalculateAngles()
    {
        projSpreads = new float[projPerShot];
        //calc spread between projectiles, or set to 0 if only 1 projectile
        float spreadPerProj = 0;
        if (projPerShot > 1)
        {
            spreadPerProj = spread / (projPerShot - 1);
        }

        int spreadCounter = 1;

        for (int y = 0; y < projPerShot; y++)
        {
            if (y % 2 == 0)
            {
                projSpreads[y] = spreadPerProj * spreadCounter;
            }
            else
            {
                projSpreads[y] = -spreadPerProj * spreadCounter;
                spreadCounter++;
            }
        }
        projSpreads[projPerShot - 1] = 0f;

        //shift all angles by half spread if even number of projectiles
        if (projPerShot % 2 == 0)
        {
            float halfSpread = spreadPerProj / 2;
            for (int x = 0; x < projSpreads.Length; x++)
            {
                projSpreads[x] -= halfSpread;
            }
        }
    }

    void CalculateReloadTime()
    {
        reloadTime = fireRate * (Mathf.Sqrt(200 / (thisShip.GetFireRate() + 100)));
    }

    void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetFirepower()) / 100)) + Random.Range(-1, 3);
    }
}
