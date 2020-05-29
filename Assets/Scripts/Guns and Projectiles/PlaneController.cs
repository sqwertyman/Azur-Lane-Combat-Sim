using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Image healthBar;
    public Color healthBarColour;

    private int health, maxHealth, noOfProj;
    private Rigidbody2D rb;
    private GameObject source;
    private AmmoData ammoData;
    private string targetTag;
    private Vector3 targetDirection;

    public void Init(int health, int speed, int noOfProj, Sprite planeSprite, AmmoData ammoData, GameObject source, string targetTag)
    {
        this.noOfProj = noOfProj;
        this.source = source;
        this.ammoData = ammoData;
        this.targetTag = targetTag;
        maxHealth = health;

        this.health = maxHealth;
        healthBar.color = healthBarColour;

        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = planeSprite;

        // select direction to fire based on target, and flip sprite if necessary
        if (targetTag == "Friendly")
        {
            targetDirection = -Vector3.right;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
            targetDirection = Vector3.right;

        rb.velocity = targetDirection * speed;

        StartCoroutine(Fly());
    }

    //sequence of events for a plane's lifetime
    private IEnumerator Fly()
    {
        //torps are dropped 2 secs after plane launched
        yield return new WaitForSeconds(2);

        for (int i = 0; i < noOfProj; i++)
        {
            Fire();
        }
    }

    //launches a single torp from the plane. currently they have range of 200, may change in future
    private void Fire()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-10, -20));
        GameObject inst = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        inst.GetComponent<BaseProjectile>().Setup(spawnPos + targetDirection, 0, ammoData, 200, source, targetTag);
    }

    private bool AtEdgeOfScreen()
    {
        if (transform.position.x > 140)
            return true;
        else
            return false;
    }

    //destroy plane when it reaches edge of play area. this is where to implement kamikaze damage if/when wanted
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }

    //called to make the ship take damage, and updates healthbar. updates ref struct holding damage dealt (0 if evaded)
    public void TakeDamage(int damage)
    {
        ShipController attacker = source.GetComponentInParent<ShipController>();

        //evasion chance
        //float hitChance = 0.1f + (attacker.GetAccuracy() / (attacker.GetAccuracy() + evasion + 2f)) + ((attacker.GetLuck() - luck) / 1000f);
        //crit chance
        //float critChance = 0.05f + (attacker.GetAccuracy() / (attacker.GetAccuracy() + evasion + 2000f)) + ((attacker.GetLuck() - luck) / 5000f);

        //check for evade
        //if (Random.Range(0f, 1f) <= hitChance)
        //{
        //    //restart visual effect coroutine
        //    StopCoroutine("FlashSprite");
        //    StartCoroutine("FlashSprite");

        //    damageInfo.damage = source.GetComponent<WeaponController>().GetDamage(armour);

        //    //crit multiplies by 1.5
        //    if (Random.Range(0f, 1f) <= critChance)
        //    {
        //        damageInfo.damage = (int)(damageInfo.damage * 1.5f);
        //        damageInfo.crit = true;
        //    }

        //    health -= damageInfo.damage;
        //    healthBar.fillAmount = (float)health / maxHealth;
        //}

        health -= damage;
        healthBar.fillAmount = (float)health / maxHealth;

        if (health <= 0)
            Destroy(gameObject);
    }
}
