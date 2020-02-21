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
    protected GameObject source;
    protected AudioSource audioSource;
    protected AmmoData ammoData;
    
    public virtual void Setup(Vector3 targetPos, float targetSpread, AmmoData ammoData, int range, GameObject source)
    {

    }

    //general setup for any projectile type
    protected void GeneralSetup(AmmoData ammoData,  GameObject source)
    {
        this.ammoData = ammoData;

        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = ammoData.Sprite;
        audioSource = GetComponent<AudioSource>();
        this.source = source;
        startPos = transform.position;
    }

    //spawns a damage number/indicator at pos with value damage
    protected void SpawnDamageNumber(ArmourType armour)
    {
        //mathf.clamp here to keep on screen if needed later
        GameObject dmgNumber = Instantiate(dmgNumberPrefab, transform.position, Quaternion.identity);
        dmgNumber.GetComponent<DamageNumber>().Init(source.GetComponent<WeaponController>().GetDamage(armour), ammoData.DmgNumberColour);
    }

    //for when the projectile needs to die. either the hit or miss audioclip is passed in to be played
    protected virtual void Despawn(AudioClip sound)
    {
        //play sound
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(sound);

        //deactivate components etc
        gameObject.GetComponent<Renderer>().enabled = false;
        
        //destroy after sound(s) will have finished. both included here for splash projectiles
        Destroy(gameObject, hitSound.length + missSound.length);
    }

    //tells collider's ship to take damage
    protected void DealDamage(Collider2D collision)
    {
        var ship = collision.GetComponent<ShipController>();

        ship.TakeDamage(source);
        SpawnDamageNumber(ship.GetArmour());
    }
}
