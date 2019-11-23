using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed, despawnTime;

    private int damage;
    private float spread;
    private Rigidbody2D rb;
    private Vector3 target;
    
    public void Setup(Vector2 targetPos, float targetSpread, int damage)
    {
        rb = GetComponent<Rigidbody2D>();
        spread = targetSpread;
        target = targetPos;
        this.damage = damage;
        Vector3 heading = target - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        print("target " + target);
        print("heading " + heading);
        print("distance " + distance);
        print("direction " + direction);
        transform.right = direction;
        transform.Rotate(0, 0, spread);
        rb.velocity = transform.right * speed;

        Destroy(gameObject, despawnTime);
    }

    public int getDamage()
    {
        return damage;
    }
}
