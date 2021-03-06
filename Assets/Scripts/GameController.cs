﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;

//handles the start of a game/battle. loads in ships and their weapons

public class GameController : MonoBehaviour
{
    public GameObject enemyObject, shipObject, lockonGunObject, bracketingGunObject, scattershotGunObject, torpedoObject, antiAirObject, planeObject, friendlyPlaneSpawn, enemyPlaneSpawn;
    public GameObject[] enemySpawns = new GameObject[2];
    public GameObject[] mainSpawns = new GameObject[3];
    public GameObject[] vanguardSpawns = new GameObject[3];
    public ShipLoadoutData[] vanguardLoadouts = new ShipLoadoutData[3];
    public ShipLoadoutData[] mainLoadouts = new ShipLoadoutData[3];
    public ShipLoadoutData[] enemyLoadouts = new ShipLoadoutData[2];
    public Canvas pauseCanvas, gameCanvas, gameOverCanvas;

    private GameObject currentTarget;
    private List<GameObject> vanguardFleet = new List<GameObject>(3);
    private List<GameObject> mainFleet = new List<GameObject>(3);
    private List<GameObject> enemyFleet = new List<GameObject>(2);
    private bool paused, victory, defeat;
    private float fleetSpeed;
    private List<GameObject> fleetAAGuns = new List<GameObject>(6);

    void Awake()
    {
        Time.timeScale = 1;
        paused = false;
        victory = false;
        defeat = false;
        StartGame();
        EventManager.StartListening("ship died", ShipDied);
    }

    void Update()
    {
        //updates current target only if friendly ships exist, to stop errors
        if (vanguardFleet.Count != 0)
        {
            currentTarget = vanguardFleet[0].GetComponent<ShipController>().GetTarget();
        }

        //pause/unpause game with escape
        if (Input.GetKeyDown("escape"))
        {
            SwitchPauseState();
        }
    }

    //called if an ship dies. the dying ship *should* trigger the event. removes ship from fleet list, and checks for game over
    void ShipDied(GameObject dead)
    {
        //try to remove dead ship from all fleet lists (nothing will happen if it doesn't exist in the list)
        enemyFleet.Remove(dead);
        vanguardFleet.Remove(dead);
        mainFleet.Remove(dead);

        CheckGameOver();
    }

    //checks whether the game has ended in victory of defeat, and ends it if so. will have more use later
    void CheckGameOver()
    {
        if (enemyFleet.Count == 0)
            victory = true;
        else if (vanguardFleet.Count == 0 || mainFleet.Count == 0)
            defeat = true;

        if (victory || defeat)
        {
            gameOverCanvas.GetComponent<GameOverController>().Init(victory);
            gameOverCanvas.gameObject.SetActive(true);
            gameCanvas.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    //instantiates ships
    public void StartGame()
    {
        //load enemies
        for (int x = 0 ; x < enemyFleet.Capacity ; x++)
        {
            LoadShip(enemyLoadouts[x], enemySpawns[x].transform.position, enemyFleet, enemyObject, "Friendly");
        }

        //load vanguard ships
        for (int x = 0; x < vanguardLoadouts.Length; x++)
        {
            LoadShip(vanguardLoadouts[x], vanguardSpawns[x].transform.position, vanguardFleet, shipObject, "Enemy");
        }
        //load main ships
        for (int x = 0; x < mainLoadouts.Length; x++)
        {
            LoadShip(mainLoadouts[x], mainSpawns[x].transform.position, mainFleet, shipObject, "Enemy");
        }

        //calculate average speed of fleet
        for (int x = 0; x < vanguardFleet.Count; x++)
        {
            fleetSpeed += vanguardFleet[x].GetComponent<ShipController>().GetSpeed();
        }
        fleetSpeed /= vanguardFleet.Count;

        //add appropriate movement scripts
        int position = 0;
        foreach (GameObject ship in vanguardFleet)
        {
            if (position == 0)
            {
                ship.AddComponent<LeadMovement>().Init(fleetSpeed);
            }
            else
            {
                ship.AddComponent<FollowMovement>().Init(vanguardFleet[position - 1], fleetSpeed);
            }
            position += 1;
        }

        vanguardFleet[0].AddComponent<FleetAAController>().Init(fleetAAGuns);
    }

    //loads in the particular ship denoted by shipLoadout, storing it at the index shipIndex in friendlyFleet
    int LoadShip(ShipLoadoutData shipLoadout, Vector3 spawnPos, List<GameObject> fleetList, GameObject prefab, string targetTag)
    {
        if (shipLoadout.Ship != null)
        {
            var ship = Instantiate(prefab, spawnPos, Quaternion.identity);
            ship.GetComponent<ShipController>().Init(shipLoadout, targetTag);

            fleetList.Add(ship);

            //temporary array to store the weapons loaded
            GameObject[] weapons = new GameObject[3];
            weapons[0] = LoadWeapon(shipLoadout.Slot1, ship, 1, targetTag);
            weapons[1] = LoadWeapon(shipLoadout.Slot2, ship, 2, targetTag);
            weapons[2] = LoadWeapon(shipLoadout.Slot3, ship, 3, targetTag);

            //gives ship references to its guns
            ship.GetComponent<ShipController>().SetGuns(weapons);

            return 1;
        }
        else
        {
            return 0;
        }
    }

    //loads in the weapon denoted by toLoad, assigning it to the ship given. returns the weapon gameobject
    private GameObject LoadWeapon(WeaponData toLoad, GameObject ship, int slotNumber, string targetTag)
    {
        GameObject prefab;

        if (toLoad != null)
        {
            //select correct prefab object based on weapon's type
            if (toLoad.Type == EquipmentType.Torpedo)
                prefab = torpedoObject;
            else if (toLoad.Type == EquipmentType.TorpedoBomber)
                prefab = planeObject;
            else if (toLoad.Type == EquipmentType.AA)
                prefab = antiAirObject;
            else
            {
                if ((toLoad as GunData).FiringType == FiringType.Bracketing)
                    prefab = bracketingGunObject;
                else if ((toLoad as GunData).FiringType == FiringType.Scattershot)
                    prefab = scattershotGunObject;
                else
                    prefab = lockonGunObject;
            }

            //instantiate object and set up its data
            var weaponInst = Instantiate(prefab, ship.transform.position, Quaternion.identity, ship.transform);
            weaponInst.GetComponent<WeaponController>().SetWeaponData(toLoad, slotNumber);
            weaponInst.GetComponent<WeaponController>().Init();

            //add aa gun to fleet's list of them
            if (prefab == antiAirObject)
            {
                //weaponInst.name = toLoad.name;
                fleetAAGuns.Add(weaponInst);
                

            }

            //only if its a plane weapon, gives the ship an extra component for syncing airstrike timings etc
            if (prefab == planeObject)
            {
                if (!ship.TryGetComponent(out AirstrikeLaunchInfo airstrikeInfo))
                {
                    airstrikeInfo = ship.AddComponent<AirstrikeLaunchInfo>();
                    if (targetTag == "Friendly")
                        airstrikeInfo.SetSpawnObject(enemyPlaneSpawn);
                    else
                        airstrikeInfo.SetSpawnObject(friendlyPlaneSpawn);
                }
                airstrikeInfo.AddPlane(weaponInst);
            }

            return weaponInst;
        }
        else
            return null;
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
        return vanguardFleet[0];
    }

    public float GetFleetSpeed()
    {
        return fleetSpeed;
    }
}
