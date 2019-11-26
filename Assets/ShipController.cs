using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private int maxHealth, firepower, health, torpedo;
    private float speed, reload;

    private GameObject enemy;
    private float moveSpeed;

    // Start is called before the first frame update
    public void Init(ShipLoadoutData loadoutData)
    {
        ShipData ship = loadoutData.Ship;

        gameObject.name = ship.name;
        maxHealth = ship.Health;
        speed = ship.Speed;
        reload = ship.Reload;
        firepower = ship.Firepower + loadoutData.Slot1.Firepower + loadoutData.Slot2.Firepower;
        torpedo = ship.Torpedo + loadoutData.Slot1.Torpedo + loadoutData.Slot2.Torpedo;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.CompareTag("Enemy") && collision.CompareTag("FriendlyProjectile"))
        {
            health -= collision.GetComponent<Projectile>().getDamage();
            Destroy(collision.gameObject);
        }
    }

    void FindNearestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
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
        enemy = nearestEnemy;
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
