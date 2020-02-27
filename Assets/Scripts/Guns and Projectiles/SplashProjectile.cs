using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashProjectile : BaseProjectile
{
    public float arcHeight = 3;

    private Vector3 targetPos, nextPos;
    private bool atTarget;
    private CircleCollider2D splashCollider;
    
    public override void Setup(Vector3 targetPos, float targetSpread, AmmoData ammoData, int range, GameObject source, string targetTag)
    {
        base.GeneralSetup(ammoData, source, targetTag);

        //random spread
        Vector3 spread = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * targetSpread;

        this.targetPos = targetPos + spread;
        
        Vector3 heading = targetPos - transform.position;
        distanceToTravel = heading.magnitude;

        atTarget = false;

        splashCollider = GetComponent<CircleCollider2D>();
        splashCollider.radius = ammoData.SplashRange / 2;
        splashCollider.enabled = false;
    }
    
    //when collides with something
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //if the colliding object is an enemy (needs generalising), tells it to take damage, spawns dmg number, and destroys
        if (collision.tag == targetTag)
        {
            DealDamage(collision);

            //separately play effects for hit here (as both hit an miss need to play, and a hit for each enemy)
            PlayEffects(true);
        }
    }

    //arcs the projectile over time
    private void Update()
    {
        if (!atTarget)
        {
            float nextX = Mathf.MoveTowards(transform.position.x, targetPos.x, ammoData.ProjectileSpeed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - startPos.x) / distanceToTravel);
            float arc = arcHeight * (nextX - startPos.x) * (nextX - targetPos.x) / (-0.25f * distanceToTravel * distanceToTravel);
            nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

            //stop loop and destroy if reached destination. water splash/miss sound still used
            //have to check this here specifically with current implementation
            if (nextPos == targetPos || nextPos == transform.position)
            {
                atTarget = true;
                Despawn(false);
            }

            transform.right = nextPos - transform.position;
            transform.position = nextPos;
        }
    }

    //re-enables collider for aoe damage
    protected override void Despawn(bool hit)
    {
        GetComponent<Collider2D>().enabled = true;
        
        base.Despawn(hit);
    }
}
