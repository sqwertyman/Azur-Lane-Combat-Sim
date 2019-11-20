using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadMovement : MonoBehaviour
{
    private float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool up, down, left, right;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = GetComponent<ShipController>().GetSpeed() / 10;
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
        InvokeRepeating("Move", 0.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //up = Input.GetKey("w");
        //down = Input.GetKey("s");
        //left = Input.GetKey("a");
        //right = Input.GetKey("d");
    }

    void Move()
    {
        velocity = new Vector2(Random.Range(-moveSpeed, moveSpeed), Random.Range(-moveSpeed, moveSpeed));
        rb.velocity = velocity;
    }

    void ManualMove()
    {
        var newVelocity = Vector2.zero;
        if (up)
        {
            newVelocity += (Vector2.up * moveSpeed);
        }
        if (down)
        {
            newVelocity += (Vector2.down * moveSpeed);
        }
        if (left)
        {
            newVelocity += (Vector2.left * moveSpeed);
        }
        if (right)
        {
            newVelocity += (Vector2.right * moveSpeed);
        }

        rb.velocity = newVelocity;
    }

}
