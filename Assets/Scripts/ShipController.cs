using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
    public Image healthBar;
    public Color healthBarColour;

    [Range(0.0f, 1.0f)]
    public float hitFlashIntensity;
    public float hitFlashTime;

    private int maxHealth, firepower, health, torpedo, aviation, accuracy, evasion, luck;
    private int[] slotMounts = new int[3];
    private int[] slotEfficiencies = new int[3];
    private float speed, reload;
    private GameObject target;
    private ArmourType armour;
    private string targetTag;

    //sets ship's stats from loadoutData (includes its gun's stats too)
    public void Init(ShipLoadoutData loadoutData, string targetTag)
    {
        ShipData ship = loadoutData.Ship;

        this.targetTag = targetTag;

        gameObject.GetComponent<SpriteRenderer>().sprite = ship.Sprite;
        healthBar.color = healthBarColour;
        gameObject.name = ship.name;
        maxHealth = ship.Health;
        speed = ship.Speed;
        reload = ship.Reload;
        firepower = ship.Firepower;
        torpedo = ship.Torpedo;
        armour = ship.Armour;
        aviation = ship.Aviation;
        accuracy = ship.Accuracy;
        evasion = ship.Evasion;
        luck = ship.Luck;
        slotMounts[0] = ship.Slot1Mounts;
        slotMounts[1] = ship.Slot2Mounts;
        slotMounts[2] = ship.Slot3Mounts;
        slotEfficiencies[0] = ship.Slot1Efficiency;
        slotEfficiencies[1] = ship.Slot2Efficiency;
        slotEfficiencies[2] = ship.Slot3Efficiency;

        if (loadoutData.Slot1)
        {
            firepower += loadoutData.Slot1.Firepower;
            torpedo += loadoutData.Slot1.Torpedo;
        }
        if (loadoutData.Slot2)
        {
            firepower += loadoutData.Slot2.Firepower;
            torpedo += loadoutData.Slot2.Torpedo;
        }
        if (loadoutData.Slot3)
        {
            firepower += loadoutData.Slot3.Firepower;
            torpedo += loadoutData.Slot3.Torpedo;
        }

        health = maxHealth;
    }

    private void Update()
    {
        //triggers appropriate event and destroys the gameobject, if health reaches 0 (dead)
        if (health <= 0)
        {
            EventManager.TriggerEvent("ship died", gameObject);
            Destroy(gameObject);
        }
    }

    //finds the nearest enemy/target to the ship. used for targetting
    public void FindNearestEnemy()
    {
        var targets = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject nearestTarget = null;
        if (targets.Length != 0)
        {
            float nearestDistance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (var thisEnemy in targets)
            {
                Vector2 difference = thisEnemy.transform.position - position;
                float thisDistance = difference.sqrMagnitude;
                if (thisDistance < nearestDistance)
                {
                    nearestTarget = thisEnemy;
                    nearestDistance = thisDistance;
                }
            }
        }
        target = nearestTarget;
    }

    //called to make the ship take damage, and updates healthbar. updates ref struct holding damage dealt (0 if evaded)
    public void TakeDamage(GameObject source, ref DamageStruct damageInfo)
    {
        ShipController attacker = source.GetComponentInParent<ShipController>();

        //evasion chance
        float hitChance = 0.1f + (attacker.GetAccuracy() / (attacker.GetAccuracy() + evasion + 2f)) + ((attacker.GetLuck() - luck) / 1000f);
        //crit chance
        float critChance = 0.05f + (attacker.GetAccuracy() / (attacker.GetAccuracy() + evasion + 2000f)) + ((attacker.GetLuck() - luck) / 5000f);

        //check for evade
        if (Random.Range(0f, 1f) <= hitChance)
        {
            //restart visual effect coroutine
            StopCoroutine("FlashSprite");
            StartCoroutine("FlashSprite");

            damageInfo.damage = source.GetComponent<WeaponController>().GetDamage(armour);

            //crit multiplies by 1.5
            if (Random.Range(0f, 1f) <= critChance)
            {
                damageInfo.damage = (int)(damageInfo.damage * 1.5f);
                damageInfo.crit = true;
            }
            
            health -= damageInfo.damage;
            healthBar.fillAmount = (float)health / maxHealth;
        }
    }

    //makes the sprite flash as a visual effect
    private IEnumerator FlashSprite()
    {
        GetComponent<Renderer>().material.SetFloat("_FlashAmount", hitFlashIntensity);
        yield return new WaitForSeconds(hitFlashTime);
        GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0);
    }

    public int GetHealth()
    {
        return health;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public float GetFireRate()
    {
        return reload;
    }

    public int GetFirepower()
    {
        return firepower;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetTorpedo()
    {
        return torpedo;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public ArmourType GetArmour()
    {
        return armour;
    }

    public int GetAviation()
    {
        return aviation;
    }

    public string GetTargetTag()
    {
        return targetTag;
    }

    public int GetAccuracy()
    {
        return accuracy;
    }

    public int GetLuck()
    {
        return luck;
    }

    public int GetSlotMounts(int slotNo)
    {
        //-1 as index start from 0
        return slotMounts[slotNo - 1];
    }

    public int GetSlotEfficiency(int slotNo)
    {
        //-1 as index start from 0
        return slotEfficiencies[slotNo - 1];
    }
}
