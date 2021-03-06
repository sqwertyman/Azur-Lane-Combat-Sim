﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public GameObject dmgNumberPrefab, missEffectPrefab, hitEffectPrefab;
    public AudioClip hitSound;
    public AudioClip missSound;

    protected int range;
    protected Rigidbody2D rb;
    protected Vector3 startPos;
    protected float distanceToTravel;
    protected GameObject source;
    protected AudioSource audioSource;
    protected AmmoData ammoData;
    protected string targetTag;
    
    public virtual void Setup(Vector3 targetPos, float targetSpread, AmmoData ammoData, int range, GameObject source, string targetTag)
    {

    }

    //general setup for any projectile type
    protected void GeneralSetup(AmmoData ammoData,  GameObject source, string targetTag)
    {
        this.ammoData = ammoData;
        this.targetTag = targetTag;

        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = ammoData.Sprite;
        audioSource = GetComponent<AudioSource>();
        this.source = source;
        startPos = transform.position;
    }

    //spawns a damage number/indicator at pos with value damage held in the struct
    protected void SpawnDamageNumber(DamageStruct damageInfo)
    {
        //mathf.clamp here to keep on screen if needed later
        GameObject dmgNumber = Instantiate(dmgNumberPrefab, transform.position, Quaternion.identity);
        dmgNumber.GetComponent<DamageNumber>().Init(damageInfo, ammoData.DmgNumberColour);
    }

    //for when the projectile needs to die. either the hit or miss audioclip is passed in to be played
    protected virtual void Despawn(bool hit)
    {
        //play appropriate effects
        PlayEffects(hit);

        //deactivate components etc
        gameObject.GetComponent<Renderer>().enabled = false;
        
        //destroy after sound(s) will have finished. both included here for splash projectiles
        Destroy(gameObject, hitSound.length + missSound.length);
    }

    //tells collider's ship to take damage, gets damage dealt back through ref struct
    protected void DealDamage(Collider2D collision)
    {
        var ship = collision.GetComponent<ShipController>();

        DamageStruct damageInfo = new DamageStruct(0, false);
        ship.TakeDamage(source, ref damageInfo);
        SpawnDamageNumber(damageInfo);
    }

    //takes a bool denoting whether the proj has hit or missed, and plays the appropriate visual and sound effects
    protected void PlayEffects(bool hit)
    {
        GameObject effect;
        AudioClip sound;

        if (hit)
        {
            effect = hitEffectPrefab;
            sound = hitSound;
        }
        else
        {
            effect = missEffectPrefab;
            sound = missSound;
        }
        
        //play effect
        Instantiate(effect, transform.position, effect.transform.rotation).GetComponent<ParticleSystem>().Play();

        //play sound
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(sound);
    }
}
