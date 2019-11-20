using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private int maxHealth;
    private float speed;
    private float reload;
    private int firepower;

    private int health;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool up, down, left, right;
    private GameObject enemy;
    private float moveSpeed;

    // Start is called before the first frame update
    public void Init(ShipData shipData)
    {
        gameObject.name = shipData.name;
        maxHealth = shipData.Health;
        speed = shipData.Speed;
        reload = shipData.Reload;
        firepower = shipData.Firepower;

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
}
