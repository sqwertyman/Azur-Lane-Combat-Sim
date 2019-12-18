using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public GameObject dmgNumberPrefab;

    protected int damage, range;
    protected Rigidbody2D rb;
    protected Vector3 startPos;
    protected float distanceToTravel;

    public virtual void Setup(Vector3 targetPos, float targetSpread, int damage, int speed, Sprite sprite, int range)
    {
        
    }

    //general setup for any projectile type
    protected void GeneralSetup(Sprite sprite, int damage)
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        startPos = transform.position;

        this.damage = damage;
    }

    //when collides with something
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //if the colliding object is an enemy (needs generalising), tells it to take damage, spawns dmg number ,and destroys
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<ShipController>().TakeDamage(damage);

            SpawnDamageNumber();

            Destroy(gameObject);
        }
    }

    //spawns a damage number/indicator at pos with value damage
    protected void SpawnDamageNumber()
    {
        //mathf.clamp here to keep on screen if needed later
        GameObject dmgNumber = Instantiate(dmgNumberPrefab, transform.position, Quaternion.identity);
        dmgNumber.GetComponent<DamageNumber>().Init(damage);
    }

    public int getDamage()
    {
        return damage;
    }
}
