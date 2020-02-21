using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subclass that handles only gun specifics
public class GunController : ProjectileWeaponController
{
    protected int angle;

    public override void Init(GunData gunData)
    {
        volleyTime = gunData.VolleyTime;
        noOfShots = gunData.NoOfShots;
        angle = gunData.Angle;
        
        base.Init(gunData);
    }

    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetFirepower()) / 100));
    }
}
