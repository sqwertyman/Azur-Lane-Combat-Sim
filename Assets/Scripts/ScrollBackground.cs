using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public Vector2 scrollSpeed = Vector2.zero;

    public void OnEnable()
    {
        GetComponent<Renderer>().material.SetVector("_ScrollSpeed", scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
