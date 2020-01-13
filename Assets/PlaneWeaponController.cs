using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWeaponController : WeaponController
{
    public GameObject planePrefab;

    private int speed;

    public override void Init(PlaneWeaponData planeData)
    {
        speed = planeData.Speed;

        base.Init(planeData);
    }

    protected override IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);
        yield return new WaitForSeconds(reloadTime);

        for (; ; )
        {

            SpawnPlane();
            yield return new WaitForSeconds(reloadTime);
        }
    }

    private void SpawnPlane()
    {
        GameObject inst = Instantiate(planePrefab, transform.position, transform.rotation);
        inst.GetComponent<PlaneController>().Init(speed);
    }

    protected override void CalculateDamage()
    {
        finalDamage = 1000;
    }

    //returns the damage (rounded to int) of the gun to the armour type passed in (with random variation too)
    public override int GetDamage(ArmourType armour)
    {
        float multiplier = Database.ArmourMultiplier(gunClass, armour, AmmoType.AirTorpedo);
        return ((int)((finalDamage * multiplier)) + Random.Range(-1, 3));
    }
}
