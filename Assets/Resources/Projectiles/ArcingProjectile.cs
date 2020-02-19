using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcingProjectile : BaseProjectile
{
    public float arcHeight = 3;

    private Vector3 targetPos, nextPos;
    private int speed;
    private bool atTarget;
    
    public override void Setup(Vector3 targetPos, float targetSpread, int speed, Sprite sprite, int range, GameObject source)
    {
        base.GeneralSetup(sprite, source);

        //random spread. works for now
        Vector3 spread = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * targetSpread;

        this.targetPos = targetPos + spread;
        this.speed = speed;

        Vector3 heading = targetPos - transform.position;
        distanceToTravel = heading.magnitude;

        atTarget = false;
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

            //destroy if reached destination
            //if (nextPos == targetPos || nextPos == transform.position)
            //{
            //    Despawn(missSound);
            //}

            transform.right = nextPos - transform.position;
            transform.position = nextPos;
        }
    }


    protected override bool ReachedMaxRange()
    {
        if (transform.position == targetPos)
        {
            atTarget = true;
            return true;
        }
        else
            return false;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //}
}
