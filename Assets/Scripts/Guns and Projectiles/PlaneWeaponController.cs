using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWeaponController : WeaponController
{
    public GameObject planePrefab;

    private int speed;
    private int noOfProj;
    private Sprite planeSprite;
    private float airstrikeReloadTime;
    private AirstrikeLaunchInfo airstrikeInfo;
    private Bounds spawnArea;

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
        airstrikeInfo = GetComponentInParent<AirstrikeLaunchInfo>();
        airstrikeReloadTime = airstrikeInfo.GetAirstrikeReload();
        spawnArea = airstrikeInfo.GetSpawnObject().GetComponent<Collider2D>().bounds;

        for (; ; )
        {
            yield return new WaitForSeconds(airstrikeReloadTime);

            for (int x = 0; x < noOfMounts; x++)
            {                
                yield return new WaitForSeconds(preFireTime);
                SpawnPlane();
                yield return new WaitForSeconds(postFireTime);
            }

        }
    }

    //spawns a single plane, and sets it up with its init method
    private void SpawnPlane()
    {
        GameObject inst = Instantiate(planePrefab, RandomPointInBounds(), transform.rotation);
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

    //returns a random point within the spawn area bounds, always with z of -1 though
    public Vector3 RandomPointInBounds()
    {
        return new Vector3(
            Random.Range(spawnArea.min.x, spawnArea.max.x),
            Random.Range(spawnArea.min.y, spawnArea.max.y),
            -1);
    }
}
