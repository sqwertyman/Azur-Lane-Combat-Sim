using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public GameObject dmgNumberPrefab;

    protected int damage;
    protected Rigidbody2D rb;

    public virtual void Setup(Vector3 targetPos, float targetSpread, int damage, int speed, Sprite sprite, int despawnTime)
    {
        

    }

    //when collides with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the colliding object is an enemy (needs generalising), tells it to take damage, spawns dmg number ,and destroys
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<ShipController>().TakeDamage(damage);

            SpawnDamageNumber(collision.transform.position);

            Destroy(gameObject);
        }
    }

    //spawns a damage number/indicator at pos with value damage
    void SpawnDamageNumber(Vector2 pos)
    {
        //mathf.clamp here to keep on screen if needed later
        GameObject dmgNumber = Instantiate(dmgNumberPrefab, pos, Quaternion.identity);
        dmgNumber.GetComponent<DamageNumber>().Init(damage);
    }

    public int getDamage()
    {
        return damage;
    }
}
