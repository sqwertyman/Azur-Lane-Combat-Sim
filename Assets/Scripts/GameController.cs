﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;

//handles the start of a game/battle. loads in ships and their weapons

public class GameController : MonoBehaviour
{
    public GameObject vanguardSpawn, enemyObject, shipObject, lockonGunObject, bracketingGunObject, scattershotGunObject, torpedoObject, planeObject;
    public GameObject[] enemySpawns = new GameObject[2];
    public GameObject[] mainSpawns = new GameObject[3];
    public ShipLoadoutData[] vanguardLoadouts = new ShipLoadoutData[3];
    public ShipLoadoutData[] mainLoadouts = new ShipLoadoutData[3];
    public ShipLoadoutData[] enemyLoadouts = new ShipLoadoutData[2];
    public Canvas pauseCanvas, gameCanvas, gameOverCanvas;

    private GameObject currentTarget;
    private List<GameObject> vanguardFleet = new List<GameObject>(3);
    private List<GameObject> mainFleet = new List<GameObject>(3);
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
            LoadShip(vanguardLoadouts[x], vanguardSpawn.transform.position, vanguardFleet, shipObject, "Enemy");
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
    }

    //loads in the particular ship denoted by shipLoadout, storing it at the index shipIndex in friendlyFleet
    int LoadShip(ShipLoadoutData shipLoadout, Vector3 spawnPos, List<GameObject> fleetList, GameObject prefab, string targetTag)
    {
        if (shipLoadout.Ship != null)
        {
            var tempShip = Instantiate(prefab, spawnPos, Quaternion.identity);
            tempShip.GetComponent<ShipController>().Init(shipLoadout, targetTag);

            LoadWeapon(shipLoadout.Slot1, tempShip, shipLoadout.Ship.NoOfSlot1);
            LoadWeapon(shipLoadout.Slot2, tempShip, shipLoadout.Ship.NoOfSlot2);

            fleetList.Add(tempShip);

            return 1;
        }
        else
        {
            return 0;
        }
    }

    //loads in the weapon denoted by toLoad, assigning it to the ship given
    private void LoadWeapon(WeaponData toLoad, GameObject ship, int noToLoad)
    {
        GameObject prefab;

        if (toLoad != null)
        {
            //select correct prefab object based on weapon's type
            if (toLoad.Type == EquipmentType.Torpedo)
                prefab = torpedoObject;
            else if (toLoad.Type == EquipmentType.Plane)
                prefab = planeObject;
            else
            {
                if ((toLoad as GunData).FiringType == FiringType.Bracketing)
                    prefab = bracketingGunObject;
                else if ((toLoad as GunData).FiringType == FiringType.Scattershot)
                    prefab = scattershotGunObject;
                else
                    prefab = lockonGunObject;
            }

            for (int i = 0; i < noToLoad; i++)
            {
                var gunInst = Instantiate(prefab, ship.transform.position, Quaternion.identity, ship.transform);
                gunInst.GetComponent<WeaponController>().SetWeaponData(toLoad);
                gunInst.GetComponent<WeaponController>().Init();
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
        return vanguardFleet[0];
    }

    public float GetFleetSpeed()
    {
        return fleetSpeed;
    }
}
