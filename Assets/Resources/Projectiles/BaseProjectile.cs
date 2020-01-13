using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public GameObject dmgNumberPrefab;

    protected int range;
    protected Rigidbody2D rb;
    protected Vector3 startPos;
    protected float distanceToTravel;
    protected Color dmgNumberColour;
    protected GameObject source;
    
    public virtual void Setup(Vector3 targetPos, float targetSpread, int speed, Sprite sprite, int range, GameObject source)
    {

    }

    //general setup for any projectile type
    protected void GeneralSetup(Sprite sprite,  GameObject source)
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.source = source;
        startPos = transform.position;
    }

    //when collides with something
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //if the colliding object is an enemy (needs generalising), tells it to take damage, spawns dmg number ,and destroys
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<ShipController>().TakeDamage(source);

            SpawnDamageNumber(collision.GetComponent<ShipController>().GetArmour());

            Destroy(gameObject);
        }
    }

    //spawns a damage number/indicator at pos with value damage
    protected void SpawnDamageNumber(ArmourType armour)
    {
        //mathf.clamp here to keep on screen if needed later
        GameObject dmgNumber = Instantiate(dmgNumberPrefab, transform.position, Quaternion.identity);
        dmgNumber.GetComponent<DamageNumber>().Init(source.GetComponent<ProjectileWeaponController>().GetDamage(armour), source.GetComponent<ProjectileWeaponController>().GetDmgNumberColour());
    }
}
