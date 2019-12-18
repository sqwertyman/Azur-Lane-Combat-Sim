using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pushes friendly tagged ships away
public class PushZone : MonoBehaviour
{
    public float force;

    private int counter;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Friendly")
        {
            counter = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Friendly")
        {
            counter += 1;
            if (collision.TryGetComponent(out LeadMovement movement))
            {
                movement.ApplyPush(counter * force);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Friendly")
        {
            counter = 0;
        }
    }
}
