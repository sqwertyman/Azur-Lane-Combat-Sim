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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 heading = target - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        transform.right = direction;
        transform.Rotate(0, 0, spread);
        rb.velocity = transform.right * speed;

        

        Destroy(gameObject, despawnTime);
    }

    public void Setup(GameObject targetObject, int damage)
    {
        target = targetObject.transform.position;
        this.damage = damage;
    }
    
    public void Setup(GameObject targetObject, float targetSpread, int damage)
    {
        spread = targetSpread;
        target = targetObject.transform.position;
        this.damage = damage;
    }

    public int getDamage()
    {
        return damage;
    }
}
