using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public enum GunType { main, aux };

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject gunObject, shipObject;
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

            if (shipLoadout.MainGun != null)
            {
                GunData mainGun = shipLoadout.MainGun;

                var gunInst = Instantiate(gunObject, friendlyInst.transform.position, new Quaternion(0, 0, 0, 0));
                gunInst.transform.SetParent(friendlyInst.transform);
                gunInst.GetComponent<GunController>().Init(mainGun);
            }

            if (shipLoadout.AuxGun != null)
            {
                GunData auxGun = shipLoadout.AuxGun;
                
                var gunInst = Instantiate(gunObject, friendlyInst.transform.position, new Quaternion(0, 0, 0, 0));
                gunInst.transform.SetParent(friendlyInst.transform);
                gunInst.GetComponent<GunController>().Init(auxGun);
            }

            return 1;
        }
        else
        {
            print("no ship");
            return 0;
        }
    }
}
