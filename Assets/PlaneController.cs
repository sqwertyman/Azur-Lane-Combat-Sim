using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private int speed;
    private Rigidbody2D rb;

    public void Init(int speed)
    {
        this.speed = speed;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
}
