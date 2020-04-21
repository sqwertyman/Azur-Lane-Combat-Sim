using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used for syncing up plane weapon firing times. added to the appropriate ships
public class AirstrikeLaunchInfo : MonoBehaviour
{
    private float combinedReloadTime;
    private List<float> individualTimes = new List<float>(3);
    private GameObject spawnObject;

    //gets the individual plane weapon's reload passed in, storing in it in the list, before recalculating
    public void AddPlane(GameObject plane)
    {
        WeaponController weapon = plane.GetComponent<WeaponController>();

        //add reload to list of individual reload times for each plane/mount
        for (int i = 0; i < weapon.GetNoOfMounts(); i++)
        {
            individualTimes.Add(weapon.GetReload());
        }
        
        RecalculateReload();
    }

    //recalculates the average of the individual weapon reloads, which becomes the overall airstrike reload time
    private void RecalculateReload()
    {
        float sum = 0;

        foreach (float time in individualTimes)
        {
            sum += time;
        }

        //may not be 100% correct as still using individual calculated reloads of the weapons as average. should be right if i'm not mistaken though
        combinedReloadTime = (sum / individualTimes.Count) * 2.2f;
    }
    public void SetSpawnObject(GameObject spawn)
    {
        spawnObject = spawn;
    }

    public float GetAirstrikeReload()
    {
        return combinedReloadTime;
    }

    public GameObject GetSpawnObject()
    {
        return spawnObject;
    }
}
