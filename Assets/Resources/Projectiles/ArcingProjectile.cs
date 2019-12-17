using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcingProjectile : BaseProjectile
{
    public float arcHeight = 3;

    private Vector3 startPos, targetPos;
    private int speed;
    private float distance;
    
    public override void Setup(Vector3 targetPos, float targetSpread, int damage, int speed, Sprite sprite, int despawnTime)
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        startPos = transform.position;

        //random spread. works for now
        Vector3 spread = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)) * (targetSpread / 5);

        this.damage = damage;
        this.targetPos = targetPos + spread;
        this.speed = speed;

        Vector3 heading = targetPos - transform.position;
        distance = heading.magnitude;

        Destroy(gameObject, despawnTime);
    }

    //arcs the projectile over time
    private void Update()
    {
        float nextX = Mathf.MoveTowards(transform.position.x, targetPos.x, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - startPos.x) / distance);
        float arc = arcHeight * (nextX - startPos.x) * (nextX - targetPos.x) / (-0.25f * distance * distance);
        Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);
        
        transform.right = nextPos - transform.position;
        transform.position = nextPos;

        if (nextPos == targetPos)
        {
            print("missed");
            Destroy(gameObject);
        }
    }
}
