using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectile : BaseProjectile
{
    public override void Setup(Vector3 targetPos, float targetSpread, int damage, int speed, Sprite sprite, int range, Color dmgNumberColour)
    {
        base.GeneralSetup(sprite, damage, dmgNumberColour);

        this.range = range;

        Vector3 heading = targetPos - transform.position;
        distanceToTravel = heading.magnitude;
        Vector3 direction = heading / distanceToTravel;
        transform.right = direction;
        transform.Rotate(0, 0, targetSpread);
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        //destroy projectile if it reaches max range
        if ((transform.position - startPos).magnitude > range)
        {
            Destroy(gameObject);
        }
    }
}
