using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public GameObject dmgNumberPrefab;
    public AudioClip hitSound;
    public AudioClip missSound;

    protected int range;
    protected Rigidbody2D rb;
    protected Vector3 startPos;
    protected float distanceToTravel;
    protected Color dmgNumberColour;
    protected GameObject source;
    protected AudioSource audioSource;
    
    public virtual void Setup(Vector3 targetPos, float targetSpread, int speed, Sprite sprite, int range, GameObject source)
    {

    }

    //general setup for any projectile type
    protected void GeneralSetup(Sprite sprite,  GameObject source)
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        audioSource = GetComponent<AudioSource>();
        this.source = source;
        startPos = transform.position;

        StartCoroutine(LifeLoop());
    }

    //when collides with something
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //if the colliding object is an enemy (needs generalising), tells it to take damage, spawns dmg number ,and destroys
        if (collision.tag == "Enemy")
        {
            //stopping here currently to stop both miss and hit scenarios happening (temp and messy)
            StopCoroutine(LifeLoop());

            collision.GetComponent<ShipController>().TakeDamage(source);

            SpawnDamageNumber(collision.GetComponent<ShipController>().GetArmour());

            Despawn(hitSound);
        }
    }

    //spawns a damage number/indicator at pos with value damage
    protected void SpawnDamageNumber(ArmourType armour)
    {
        //mathf.clamp here to keep on screen if needed later
        GameObject dmgNumber = Instantiate(dmgNumberPrefab, transform.position, Quaternion.identity);
        dmgNumber.GetComponent<DamageNumber>().Init(source.GetComponent<WeaponController>().GetDamage(armour), source.GetComponent<WeaponController>().GetDmgNumberColour());
    }

    //for when the projectile needs to die. either the hit or miss audioclip is passed in to be played
    protected void Despawn(AudioClip sound)
    {
        //play sound
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(sound);

        //deactivate necessary components etc
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;

        //destroy after sound will have finished
        Destroy(gameObject, sound.length);
    }

    //coroutine to kill projectile at end of its travel
    protected IEnumerator LifeLoop()
    {
        yield return new WaitUntil(() => ReachedMaxRange());
        print("miss");
        Despawn(missSound);
    }

    //implemented by subclasses, and used by coroutine to check if projectile is at its max range
    protected virtual bool ReachedMaxRange()
    {
        return false;
    }
}
