﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
    private int maxHealth, firepower, health, torpedo;
    private float speed, reload;
    private GameObject enemy;

    //sets ship's stats from loadoutData (includes its gun's stats too)
    public void Init(ShipLoadoutData loadoutData)
    {
        ShipData ship = loadoutData.Ship;

        gameObject.name = ship.name;
        maxHealth = ship.Health;
        speed = ship.Speed;
        reload = ship.Reload;
        firepower = ship.Firepower;
        torpedo = ship.Torpedo;
        if (loadoutData.Slot1)
        {
            firepower += loadoutData.Slot1.Firepower;
            torpedo += loadoutData.Slot1.Torpedo;
        }
        if (loadoutData.Slot2)
        {
            firepower += loadoutData.Slot2.Firepower;
            torpedo += loadoutData.Slot2.Torpedo;
        }
        health = maxHealth;
    }

    private void Update()
    {
        //triggers appropriate event and destroys the gameobject, if health reaches 0 (dead)
        if (health <= 0)
        {
            EventManager.TriggerEvent("enemy died", gameObject);
            Destroy(gameObject);
        }
    }

    //finds the nearest enemy to the ship. used for targetting
    public void FindNearestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        if (enemies.Length != 0)
        {
            float nearestDistance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (var thisEnemy in enemies)
            {
                Vector2 difference = thisEnemy.transform.position - position;
                float thisDistance = difference.sqrMagnitude;
                if (thisDistance < nearestDistance)
                {
                    nearestEnemy = thisEnemy;
                    nearestDistance = thisDistance;
                }
            }
        }
        enemy = nearestEnemy;
    }

    //called to make the ship take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public int GetHealth()
    {
        return health;
    }

    public GameObject GetEnemy()
    {
        return enemy;
    }

    public float GetFireRate()
    {
        return reload;
    }

    public int GetFirepower()
    {
        return firepower;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetTorpedo()
    {
        return torpedo;
    }
}
