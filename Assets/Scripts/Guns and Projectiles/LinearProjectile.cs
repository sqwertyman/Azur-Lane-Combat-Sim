using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectile : BaseProjectile
{
    bool alreadyHit;

    public override void Setup(Vector3 targetPos, float targetSpread, AmmoData ammoData, int range, GameObject source, string targetTag)
    {
        base.GeneralSetup(ammoData, source, targetTag);

        this.range = range;
        
        Vector3 heading = targetPos - transform.position;
        distanceToTravel = heading.magnitude;
        Vector3 direction = heading / distanceToTravel;
        transform.right = direction;
        transform.Rotate(0, 0, targetSpread);
        rb.velocity = transform.right * ammoData.ProjectileSpeed;

        alreadyHit = false;

        StartCoroutine(LifeLoop());
    }

    //when collides with something
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //if the colliding object is an enemy (needs generalising), tells it to take damage, spawns dmg number, and destroys
        if (collision.tag == targetTag && !alreadyHit)
        {
            alreadyHit = true;

            DealDamage(collision);

            Despawn(true);
        }
    }

    //used by coroutine to check if projectile is at its max range
    protected bool ReachedMaxRange()
    {
        if ((transform.position - startPos).magnitude > range)
            return true;
        else
            return false;
    }

    //coroutine to kill projectile at end of its travel
    protected IEnumerator LifeLoop()
    {
        yield return new WaitUntil(() => ReachedMaxRange());
        
        Despawn(false);
    }

    //disables more components etc so only one impact happens
    protected override void Despawn(bool hit)
    {
        //gameObject.GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;

        base.Despawn(hit);
    }
}
