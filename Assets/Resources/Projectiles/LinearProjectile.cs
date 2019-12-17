using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectile : BaseProjectile
{
    public override void Setup(Vector3 targetPos, float targetSpread, int damage, int speed, Sprite sprite, int despawnTime)
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        this.damage = damage;
        Vector3 heading = targetPos - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        transform.right = direction;
        transform.Rotate(0, 0, targetSpread);
        rb.velocity = transform.right * speed;

        Destroy(gameObject, despawnTime);
    }
}
