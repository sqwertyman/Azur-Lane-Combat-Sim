using System.Collections;
using System.Collections.Generic;
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
        fleetRange = (fleetRange * 2) / guns.Count;

        StartCoroutine(FiringLoop());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.GetComponentInParent<ShipController>().transform.position, fleetRange);
    }

    private IEnumerator FiringLoop()
    {
        for (; ; )
        {
            yield return new WaitUntil(() => PlaneInRange() == true);

            print("do dmg");

            yield return new WaitForSeconds(fleetFirerate);
        }
    }

    public bool PlaneInRange()
    {
        var targets = GameObject.FindGameObjectsWithTag("EnemyPlane");

        Vector3 position = transform.position;
        foreach (var thisEnemy in targets)
        {
            Vector2 difference = thisEnemy.transform.position - position;
            if (difference.magnitude <= fleetRange)
                return true;
            else
                return false;
        }

        return false;
    }
}
