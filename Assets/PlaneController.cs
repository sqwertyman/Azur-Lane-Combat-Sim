using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public GameObject projectilePrefab;

    private int speed, noOfProj;
    private Rigidbody2D rb;
    private GameObject source;
    private Sprite projectileSprite;

    public void Init(int speed, int noOfProj, Sprite planeSprite, Sprite projectileSprite, GameObject source)
    {
        this.speed = speed;
        this.noOfProj = noOfProj;
        this.source = source;
        this.projectileSprite = projectileSprite;

        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = planeSprite;

        rb.velocity = transform.right * speed;

        StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        yield return new WaitUntil(() => CloseToCenter());

        for (int i = 0; i < noOfProj; i++)
        {
            Fire();
        }

        yield return new WaitUntil(() => AtEdgeOfScreen());

        Destroy(gameObject);
    }

    //launches a single torp from the plane
    private void Fire()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-10, -20));
        GameObject inst = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        inst.GetComponent<BaseProjectile>().Setup(spawnPos + Vector3.right, 0, 30, projectileSprite, 100, source);
    }

    private bool CloseToCenter()
    {
        if (transform.position.x > 0)
            return true;
        else
            return false;
    }

    private bool AtEdgeOfScreen()
    {
        if (transform.position.x > 140)
            return true;
        else
            return false;
    }
}
