using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public enum EquipmentType { CL, DD, Torpedo };

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject shipObject, gunObject, torpedoObject;
    public ShipLoadoutData[] shipLoadouts = new ShipLoadoutData[3];

    private GameObject friendlyInst;
    private GameObject enemyTest, lastInst;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (friendlyInst)
        {
            enemyTest = friendlyInst.GetComponent<ShipController>().GetEnemy();
        }
    }

    public GameObject getEnemy()
    {
        return enemyTest;
    }

    public void StartGame()
    {
        Instantiate(enemy, new Vector3(12, 4, 0), new Quaternion(0, 0, 0, 0));
        Instantiate(enemy, new Vector3(12, -4, 0), new Quaternion(0, 0, 0, 0));

        foreach (ShipLoadoutData shipLoadout in shipLoadouts)
        {
            if (LoadShip(shipLoadout) == 1)
            {
                if (lastInst)
                {
                    friendlyInst.AddComponent<FollowMovement>().Init(lastInst);
                    lastInst = friendlyInst;
                }
                else
                {
                    friendlyInst.AddComponent<LeadMovement>();
                    lastInst = friendlyInst;
                }
            }
        }
    }

    int LoadShip(ShipLoadoutData shipLoadout)
    {
        if (shipLoadout.Ship != null)
        {
            ShipData ship = shipLoadout.Ship;

            friendlyInst = Instantiate(shipObject, new Vector3(-4, 0, 0), new Quaternion(0, 0, 0, 0));
            friendlyInst.GetComponent<ShipController>().Init(ship);

            LoadGun(shipLoadout.Slot1);
            LoadGun(shipLoadout.Slot2);

            return 1;
        }
        else
        {
            print("no ship");
            return 0;
        }
    }

    private void LoadGun(EquipmentData gunLoad)
    {
        if (gunLoad != null)
        {
            if (gunLoad.Type == EquipmentType.Torpedo)
            {
                var gunInst = Instantiate(torpedoObject, friendlyInst.transform.position, new Quaternion(0, 0, 0, 0));
                gunInst.transform.SetParent(friendlyInst.transform);
                gunInst.GetComponent<WeaponController>().Init(gunLoad);
            }
            else
            {
                var gunInst = Instantiate(gunObject, friendlyInst.transform.position, new Quaternion(0, 0, 0, 0));
                gunInst.transform.SetParent(friendlyInst.transform);
                gunInst.GetComponent<WeaponController>().Init(gunLoad as GunData);
            }
        }
    }
}
