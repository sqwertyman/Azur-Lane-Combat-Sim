using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FleetAAController : MonoBehaviour
{
    private List<GameObject> guns = new List<GameObject>(6);
    private float fleetRange, fleetFirerate;
    private int fleetDamage;

    //set the fleet's aa guns, init other variables
    public void Init(List<GameObject> guns)
    {
        this.guns = guns;

        foreach (GameObject gun in guns)
        {
            AAController aaGun = gun.GetComponent<AAController>();

            fleetDamage += aaGun.GetDamage();
            fleetFirerate += aaGun.GetReload();
            fleetRange += aaGun.GetRange();
        }

        fleetFirerate /= guns.Count;
        fleetRange = (fleetRange * 2) / guns.Count; //*2 to get roughly right range
        if (guns.Count == 0)
            fleetDamage = 0;
        else
            fleetDamage /= guns.Count;

        StartCoroutine(FiringLoop());
    }

    //shows a circle around the ship, indicating the fleet's aa range
    private void OnDrawGizmos()
    {
#if (UNITY_EDITOR)
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(gameObject.GetComponentInParent<ShipController>().transform.position, Vector3.forward, fleetRange);
#endif
    }

    private IEnumerator FiringLoop()
    {
        for (; ; )
        {
            yield return new WaitUntil(() => PlaneInRange() == true);

            foreach (var plane in GetAllPlanesInRange())
            {
                plane.TakeDamage(fleetDamage);
                print(fleetDamage);
            }

            yield return new WaitForSeconds(fleetFirerate);
        }
    }

    //returns true if an enemy plane is within aa range. probably needs to instead return all planes in range
    public bool PlaneInRange()
    {
        var planes = GameObject.FindGameObjectsWithTag("EnemyPlane");

        Vector3 position = transform.position;
        foreach (var plane in planes)
        {
            Vector2 difference = plane.transform.position - position;
            if (difference.magnitude <= fleetRange)
                return true;
            else
                return false;
        }

        return false;
    }

    public List<PlaneController> GetAllPlanesInRange()
    {
        var planes = GameObject.FindGameObjectsWithTag("EnemyPlane");
        List<PlaneController> targets = new List<PlaneController>();

        Vector3 position = transform.position;
        foreach (var plane in planes)
        {
            Vector2 difference = plane.transform.position - position;
            if (difference.magnitude <= fleetRange)
                targets.Add(plane.GetComponent<PlaneController>());
        }

        return targets;
    }
}
