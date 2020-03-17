using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    //public Color colour = new Color(0.8f, 0.8f, 0.1f);
    public float scrollSpeed = 0.05f;
    public float duration = 1.5f;
    public float depthOffset;

    private float alpha;
    private new Renderer renderer;
    private TextMesh text;
    private Color colour;

    void Awake()
    {
        text = gameObject.GetComponent<TextMesh>();
        renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = colour;
        alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //repeats until completely disappeared. scrolls the number upwards and fades
        if (alpha > 0)
        {
            transform.position += new Vector3(0, scrollSpeed*Time.deltaTime, 0);
            alpha -= Time.deltaTime / duration;
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //used to set details on instantiation
    public void Init(int damage, Color textColour)
    {
        if (damage == 0)
            text.text = "miss";
        else
            text.text = damage.ToString();

        renderer.material.color = textColour;
        transform.position += new Vector3(0, 0, depthOffset);

        //increase size if damage is large. could be more complex with gradual scaling
        if (damage > 200)
        {
            text.fontSize += 10;
        }
    }
}
