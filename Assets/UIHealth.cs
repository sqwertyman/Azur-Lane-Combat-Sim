using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public Text text;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetComponent<GameController>().GetEnemy())
        {
            text.text = "ENEMY HEALTH: " + gameController.GetComponent<GameController>().GetEnemy().GetComponent<ShipController>().GetHealth();
        }
    }
}
