using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadMovement : MonoBehaviour
{
    private float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool up, down, left, right;
    private Vector2 centerPos;
    
    void Start()
    {
        centerPos = new Vector2(-2, 0); //need to not hard code
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
    }

    public void Init(float fleetSpeed)
    {
        moveSpeed = fleetSpeed;
        InvokeRepeating("Move", 0.5f, 2f);
    }

    void Update()
    {
        //up = Input.GetKey("w");
        //down = Input.GetKey("s");
        //left = Input.GetKey("a");
        //right = Input.GetKey("d");
        //ManualMove();
    }

    //regularly called to randomly move the ship
    void Move()
    {
        var tempX = centerPos.x - rb.transform.position.x;
        var tempY = centerPos.y - rb.transform.position.y;

        velocity = new Vector2(CalculateRandomMovement(tempX), CalculateRandomMovement(tempY));
        
        rb.velocity = velocity;
    }

    //used by PushZone to move the ship out of the enemy's half of the field. applies an increasing velocity change
    public void ApplyPush(float magnitude)
    {
        var velocityChange = new Vector2(magnitude, 0);
        rb.velocity -= velocityChange;
    }

    //checks the float given, restricting which directions the random movement can go if too high or too low
    //needs improving (i.e. not hard coded)
    private float CalculateRandomMovement(float currentPos)
    {
        if (currentPos > 6)
        {
            return Random.Range(0, moveSpeed);
        }
        else if (currentPos < -6)
        {
            return Random.Range(-moveSpeed, 0);
        }
        else
        {
            return Random.Range(-moveSpeed, moveSpeed);
        }
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
