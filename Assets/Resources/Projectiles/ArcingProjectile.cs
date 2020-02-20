using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcingProjectile : BaseProjectile
{
    public float arcHeight = 3;
    public float splashRange;
    
    private Vector3 targetPos, nextPos;
    private int speed;
    private bool atTarget;
    private CircleCollider2D splashCollider;
    
    public override void Setup(Vector3 targetPos, float targetSpread, int speed, Sprite sprite, int range, GameObject source)
    {
        base.GeneralSetup(sprite, source);

        //random spread
        Vector3 spread = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * targetSpread;

        this.targetPos = targetPos + spread;
        this.speed = speed;

        Vector3 heading = targetPos - transform.position;
        distanceToTravel = heading.magnitude;

        atTarget = false;

        splashCollider = GetComponent<CircleCollider2D>();
        splashCollider.radius = splashRange / 2;
        splashCollider.enabled = false;
    }
    
    //when collides with something
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //if the colliding object is an enemy (needs generalising), tells it to take damage, spawns dmg number, and destroys
        if (collision.tag == "Enemy")
        {
            DealDamage(collision);
        }
    }

    //arcs the projectile over time
    private void Update()
    {
        if (!atTarget)
        {
            float nextX = Mathf.MoveTowards(transform.position.x, targetPos.x, speed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - startPos.x) / distanceToTravel);
            float arc = arcHeight * (nextX - startPos.x) * (nextX - targetPos.x) / (-0.25f * distanceToTravel * distanceToTravel);
            nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

            //stop loop and destroy if reached destination. water splash/miss sound still used
            //have to check this here specifically with current implementation
            if (nextPos == targetPos || nextPos == transform.position)
            {
                atTarget = true;
                Despawn(missSound);
            }

            transform.right = nextPos - transform.position;
            transform.position = nextPos;
        }
    }

    //re-enables collider for aoe damage
    protected override void Despawn(AudioClip sound)
    {
        GetComponent<Collider2D>().enabled = true;
        
        base.Despawn(sound);
    }
}
