using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public enum EquipmentType { CL, DD, Torpedo };

//handles the start of a game/battle. loads in ships and their weapons

public class GameController : MonoBehaviour
{
    public GameObject friendlySpawn, enemyObject, shipObject, gunObject, torpedoObject;
    public GameObject[] enemySpawns = new GameObject[2];
    public ShipLoadoutData[] shipLoadouts = new ShipLoadoutData[3];
    public ShipLoadoutData enemyData;

    private GameObject currentTarget;
    private List<GameObject> friendlyFleet = new List<GameObject>(3);
    private GameObject[] enemyFleet = new GameObject[2];

    void Awake()
    {
        StartGame();
    }

    void Update()
    {
        if (friendlyFleet.Count != 0)
        {
            currentTarget = friendlyFleet[0].GetComponent<ShipController>().GetEnemy();
        }
    }

    //instantiates ships
    public void StartGame()
    {
        //load enemies
        for (int x = 0 ; x < enemyFleet.Length ; x++)
        {
            enemyFleet[x] = Instantiate(enemyObject, enemySpawns[x].transform.position, new Quaternion(0, 0, 0, 0));
            enemyFleet[x].GetComponent<ShipController>().Init(enemyData);
        }

        //load friendlies
        for (int x = 0; x < shipLoadouts.Length; x++)
        {
            LoadShip(shipLoadouts[x]);
        }

        //add appropriate movement scripts
        int position = 0;
        foreach (GameObject ship in friendlyFleet)
        {
            if (position == 0)
            {
                ship.AddComponent<LeadMovement>();
            }
            else
            {
                ship.AddComponent<FollowMovement>().Init(friendlyFleet[position - 1]);
            }
            position += 1;
        }
    }

    //loads in the particular ship denoted by shipLoadout, storing it at the index shipIndex in friendlyFleet
    int LoadShip(ShipLoadoutData shipLoadout)
    {
        if (shipLoadout.Ship != null)
        {
            var tempShip = Instantiate(shipObject, friendlySpawn.transform.position, new Quaternion(0, 0, 0, 0));
            tempShip.GetComponent<ShipController>().Init(shipLoadout);

            LoadWeapon(shipLoadout.Slot1, tempShip);
            LoadWeapon(shipLoadout.Slot2, tempShip);

            friendlyFleet.Add(tempShip);

            return 1;
        }
        else
        {
            return 0;
        }
    }

    //loads in the weapon denoted by toLoad, assigning it to the ship given
    private void LoadWeapon(EquipmentData toLoad, GameObject ship)
    {
        if (toLoad != null)
        {
            if (toLoad.Type == EquipmentType.Torpedo)
            {
                var gunInst = Instantiate(torpedoObject, ship.transform.position, new Quaternion(0, 0, 0, 0));
                gunInst.transform.SetParent(ship.transform);
                gunInst.GetComponent<WeaponController>().Init(toLoad as TorpedoData);
            }
            else
            {
                var gunInst = Instantiate(gunObject, ship.transform.position, new Quaternion(0, 0, 0, 0));
                gunInst.transform.SetParent(ship.transform);
                gunInst.GetComponent<WeaponController>().Init(toLoad as GunData);
            }
        }
    }

    public GameObject GetEnemy()
    {
        return currentTarget;
    }

    public GameObject GetLeadShip()
    {
        return friendlyFleet[0];
    }
}
