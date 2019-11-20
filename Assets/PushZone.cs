using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushZone : MonoBehaviour
{
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.attachedRigidbody.AddForce(new Vector2(-force, 0));
        }
    }
}
