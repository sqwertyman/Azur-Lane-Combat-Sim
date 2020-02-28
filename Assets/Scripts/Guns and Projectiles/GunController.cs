using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subclass that handles only gun specifics
public class GunController : ProjectileWeaponController
{
    protected int angle;
    protected Vector3 targetDirection;

    public override void Init()
    {
        GunData tempData = weaponData as GunData;
        volleyTime = tempData.VolleyTime;
        noOfShots = tempData.NoOfShots;
        angle = tempData.Angle;

        base.Init();

        //select direction to fire based on target
        if (targetTag == "Friendly")
            targetDirection = -Vector3.right;
        else
            targetDirection = Vector3.right;
    }

    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetFirepower()) / 100));
    }
}
