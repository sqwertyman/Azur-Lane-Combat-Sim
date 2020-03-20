using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subclass that handles only gun specifics
public class GunController : ProjectileWeaponController
{
    protected int angle;

    public override void Init()
    {
        GunData tempData = weaponData as GunData;
        volleyTime = tempData.VolleyTime;
        noOfShots = tempData.NoOfShots;
        angle = tempData.Angle;

        base.Init();
    }

    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetFirepower()) / 100));
    }
}
