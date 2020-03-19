using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWeaponController : WeaponController
{
    public GameObject planePrefab;

    private int speed;
    private int noOfProj;
    private Sprite planeSprite;

    public override void Init()
    {
        PlaneWeaponData newTempData = weaponData as PlaneWeaponData;
        speed = newTempData.Speed;
        noOfProj = newTempData.NoOfProj;
        planeSprite = newTempData.PlaneSprite;

        base.Init();
    }

    //spawns a plane once every reloadtime
    protected override IEnumerator FiringLoop()
    {

        for (; ; )
        {
            yield return new WaitForSeconds(reloadTime);

            for (int x = 0; x < noOfMounts; x++)
            {                
                yield return new WaitForSeconds(preFireTime);
                SpawnPlane();
                yield return new WaitForSeconds(postFireTime);
            }

        }
    }

    //spawns a single plane, and sets it up with its init method. hardcoded randomise for now as don't know specifics
    private void SpawnPlane()
    {
        GameObject inst = Instantiate(planePrefab, new Vector3(Random.Range(-140, -130), Random.Range(-40,40),-1), transform.rotation);
        inst.GetComponent<PlaneController>().Init(speed, noOfProj, planeSprite, ammoData, gameObject, targetTag);
    }


    protected override void CalculateDamage()
    {
        finalDamage = (damage * ((100 + thisShip.GetAviation()) / 100));
    }

    //returns the damage (rounded to int) of the gun to the armour type passed in
    public override int GetDamage(ArmourType armour)
    {
        float multiplier = Database.ArmourMultiplier(gunClass, armour, ammo);
        return ((int)((finalDamage * multiplier)));
    }
}
