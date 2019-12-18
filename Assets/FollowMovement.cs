using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour
{
    public GameObject target;

    private Rigidbody2D rb;
    private float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //moves this ship towards the lead ship (target) gradually
    void FixedUpdate()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        Vector3 deltaPosition = moveSpeed * dir * Time.fixedDeltaTime;
        rb.MovePosition(Vector2.Lerp(transform.position, target.transform.position, deltaPosition.magnitude/10)); //multiplied for now to adjust
    }

    public void Init(GameObject newTarget, float fleetSpeed)
    {
        moveSpeed = fleetSpeed;
        target = newTarget;
    }
}
