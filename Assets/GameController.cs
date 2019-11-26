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

    private GameObject friendlyInst, enemyTest, lastInst, leadShip;

    // Start is called before the first frame update
    void Awake()
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

    public void StartGame()
    {
        Instantiate(enemy, new Vector3(10, 4, 0), new Quaternion(0, 0, 0, 0));
        Instantiate(enemy, new Vector3(10, -4, 0), new Quaternion(0, 0, 0, 0));

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
                    leadShip = friendlyInst;
                }
            }
        }
    }

    int LoadShip(ShipLoadoutData shipLoadout)
    {
        if (shipLoadout.Ship != null)
        {
            friendlyInst = Instantiate(shipObject, new Vector3(-4, 0, 0), new Quaternion(0, 0, 0, 0));
            friendlyInst.GetComponent<ShipController>().Init(shipLoadout);

            LoadWeapon(shipLoadout.Slot1);
            LoadWeapon(shipLoadout.Slot2);

            return 1;
        }
        else
        {
            print("no ship");
            return 0;
        }
    }

    private void LoadWeapon(EquipmentData toLoad)
    {
        if (toLoad != null)
        {
            if (toLoad.Type == EquipmentType.Torpedo)
            {
                var gunInst = Instantiate(torpedoObject, friendlyInst.transform.position, new Quaternion(0, 0, 0, 0));
                gunInst.transform.SetParent(friendlyInst.transform);
                gunInst.GetComponent<WeaponController>().Init(toLoad as TorpedoData);
            }
            else
            {
                var gunInst = Instantiate(gunObject, friendlyInst.transform.position, new Quaternion(0, 0, 0, 0));
                gunInst.transform.SetParent(friendlyInst.transform);
                gunInst.GetComponent<WeaponController>().Init(toLoad as GunData);
            }
        }
    }
    public GameObject GetEnemy()
    {
        return enemyTest;
    }

    public GameObject GetLeadShip()
    {
        return leadShip;
    }
}
