using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAController : WeaponController
{
    protected int range;

    public override void Init()
    {
        AntiAirData newTempData = weaponData as AntiAirData;
        range = newTempData.Range;

        base.Init();
    }

    protected override void CalculateDamage()
    {
        finalDamage = damage * (efficiency / 100f) * ((100 + thisShip.GetAntiAir()) / 100f);
    }

    public override int GetDamage()
    {
        return (int)finalDamage;
    }

    public int GetRange()
    {
        return range;
    }
}
