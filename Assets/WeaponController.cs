using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected float startDelay, fireRate, volleyTime;
    protected Projectile projectile;
    protected int noOfShots, projPerShot, damage, spread;

    protected GameObject target;
    protected ShipController thisShip;
    protected float reloadTime;
    protected int finalDamage;
    protected float[] projSpreads;

    public virtual void Init(EquipmentData gunData)
    {
        
    }

    public virtual void Init(GunData gunData)
    {
        
    }

    public virtual void Init(TorpedoData gunData)
    {
        
    }

    //fires pattern at regular intervals, based on startDelay, fireRate, etc.
    protected virtual IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);
    }

    //populates projSpreads array with angles for each projectile to be fired at
    protected void CalculateAngles()
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

    protected void CalculateReloadTime()
    {
        reloadTime = fireRate * (Mathf.Sqrt(200 / (thisShip.GetFireRate() + 100)));
    }

    protected virtual void CalculateDamage()
    {
        
    }
}
