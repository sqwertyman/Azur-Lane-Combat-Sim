using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//handles large enemy health info at top of screen
public class UIHealth : MonoBehaviour
{
    public Image healthBar;
    public Text healthText;
    public Text nameText;
    public GameObject gameController;
    public GameObject enemyInfo;

    private GameController gcScript;
    private ShipController currentEnemy;

    private void Start()
    {
        gcScript = gameController.GetComponent<GameController>();
    }

    //refreshes enemy info, or hides everything if there is no enemy
    void Update()
    {
        if (gcScript.GetEnemy())
        {
            enemyInfo.SetActive(true);

            currentEnemy = gcScript.GetEnemy().GetComponent<ShipController>();
            healthText.text = currentEnemy.GetHealth() + " / " + currentEnemy.GetMaxHealth();
            healthBar.fillAmount = (float)currentEnemy.GetHealth() / currentEnemy.GetMaxHealth();
            nameText.text = currentEnemy.name;
        }
        else
        {
            enemyInfo.SetActive(false);
        }
    }
}
