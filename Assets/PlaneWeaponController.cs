using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWeaponController : WeaponController
{
    public GameObject planePrefab;

    private int speed;
    private int noOfProj;
    private Sprite planeSprite;

    public override void Init(PlaneWeaponData planeData)
    {
        speed = planeData.Speed;
        noOfProj = planeData.NoOfProj;
        planeSprite = planeData.PlaneSprite;

        base.Init(planeData);
    }

    //spawns a plane once every reloadtime
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

    //spawns a single plane, and sets it up with its init method
    private void SpawnPlane()
    {
        GameObject inst = Instantiate(planePrefab, new Vector3(Random.Range(-140, -130), Random.Range(-40,40),-1), transform.rotation);
        inst.GetComponent<PlaneController>().Init(speed, noOfProj, planeSprite, sprite, gameObject);
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
