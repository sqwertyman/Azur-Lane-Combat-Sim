using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;

public enum EquipmentType { CL, DD, Torpedo };

//handles the start of a game/battle. loads in ships and their weapons

public class GameController : MonoBehaviour
{
    public GameObject friendlySpawn, enemyObject, shipObject, gunObject, torpedoObject;
    public GameObject[] enemySpawns = new GameObject[2];
    public ShipLoadoutData[] shipLoadouts = new ShipLoadoutData[3];
    public ShipLoadoutData enemyData;
    public Canvas pauseCanvas, gameCanvas, gameOverCanvas;

    private GameObject currentTarget;
    private List<GameObject> friendlyFleet = new List<GameObject>(3);
    private List<GameObject> enemyFleet = new List<GameObject>(2);
    private bool paused, gameOver;
    private float fleetSpeed;

    void Awake()
    {
        Time.timeScale = 1;
        paused = false;
        gameOver = false;
        StartGame();
        EventManager.StartListening("enemy died", HandleEnemyDied);
    }

    //called if an enemy dies. the dying enemy *should* trigger the event. removes enemy from fleet list, and checks for game over
    void HandleEnemyDied(GameObject dead)
    {
        enemyFleet.Remove(dead);
        CheckGameOver();
    }

    //checks whether the game has ended, and ends it if so. will have more use later
    void CheckGameOver()
    {
        if (enemyFleet.Count == 0)
        {
            gameOver = true;
        }

        if (gameOver)
        {
            gameOverCanvas.gameObject.SetActive(true);
            gameCanvas.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    void Update()
    {
        //updates current target only if friendly ships exist, to stop errors
        if (friendlyFleet.Count != 0)
        {
            currentTarget = friendlyFleet[0].GetComponent<ShipController>().GetEnemy();
        }

        //pause/unpause game with escape
        if (Input.GetKeyDown("escape"))
        {
            SwitchPauseState();
        }
    }

    //instantiates ships
    public void StartGame()
    {
        //load enemies
        for (int x = 0 ; x < enemyFleet.Capacity ; x++)
        {
            var tempEnemy = Instantiate(enemyObject, enemySpawns[x].transform.position, Quaternion.identity);
            tempEnemy.GetComponent<ShipController>().Init(enemyData);

            enemyFleet.Add(tempEnemy);
        }

        //load friendlies
        for (int x = 0; x < shipLoadouts.Length; x++)
        {
            LoadShip(shipLoadouts[x]);
        }

        //calculate average speed of fleet
        for (int x = 0; x < friendlyFleet.Count; x++)
        {
            fleetSpeed += friendlyFleet[x].GetComponent<ShipController>().GetSpeed();
        }
        fleetSpeed /= (friendlyFleet.Count * 10);   //speed divided by 10 currently to keep roughly accurate

        //add appropriate movement scripts
        int position = 0;
        foreach (GameObject ship in friendlyFleet)
        {
            if (position == 0)
            {
                ship.AddComponent<LeadMovement>().Init(fleetSpeed);
            }
            else
            {
                ship.AddComponent<FollowMovement>().Init(friendlyFleet[position - 1], fleetSpeed);
            }
            position += 1;
        }
    }

    //loads in the particular ship denoted by shipLoadout, storing it at the index shipIndex in friendlyFleet
    int LoadShip(ShipLoadoutData shipLoadout)
    {
        if (shipLoadout.Ship != null)
        {
            var tempShip = Instantiate(shipObject, friendlySpawn.transform.position, Quaternion.identity);
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
                var gunInst = Instantiate(torpedoObject, ship.transform.position, Quaternion.identity);
                gunInst.transform.SetParent(ship.transform);
                gunInst.GetComponent<WeaponController>().Init(toLoad as TorpedoData);
            }
            else
            {
                var gunInst = Instantiate(gunObject, ship.transform.position, Quaternion.identity);
                gunInst.transform.SetParent(ship.transform);
                gunInst.GetComponent<WeaponController>().Init(toLoad as GunData);
            }
        }
    }

    public void SwitchPauseState()
    {
        paused = !paused;
        pauseCanvas.gameObject.SetActive(paused);
        gameCanvas.gameObject.SetActive(!paused);
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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

    public float GetFleetSpeed()
    {
        return fleetSpeed;
    }
}
