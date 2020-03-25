using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public GameObject projectilePrefab;

    private int noOfProj;
    private Rigidbody2D rb;
    private GameObject source;
    private AmmoData ammoData;
    private string targetTag;
    private Vector3 targetDirection;

    public void Init(int speed, int noOfProj, Sprite planeSprite, AmmoData ammoData, GameObject source, string targetTag)
    {
        this.noOfProj = noOfProj;
        this.source = source;
        this.ammoData = ammoData;
        this.targetTag = targetTag;

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
}
