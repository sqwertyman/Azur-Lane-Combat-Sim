using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu, fleetMenu, enemyMenu;

    // Start is called before the first frame update
    void Start()
    {
        Database.Start();

        if (!Directory.Exists(Database.savePath))
        {
            Directory.CreateDirectory(Database.savePath);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenFleetMenu()
    {
        fleetMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenEnemyMenu()
    {
        enemyMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseFleetMenu()
    {
        bool canStart = fleetMenu.GetComponent<FleetMenuController>().ExitFleetMenu();
        if (canStart)
        {
            mainMenu.SetActive(true);
            fleetMenu.SetActive(false);
        }
        else
        {
            print("Must select at least one main and vanguard ship.");
        }
    }

    public void CloseEnemyMenu()
    {
        enemyMenu.GetComponent<EnemyMenuController>().ExitFleetMenu();
        mainMenu.SetActive(true);
        enemyMenu.SetActive(false);
    }

    //called by start button
    public void StartGame()
    {
        SceneManager.LoadScene("TestingScene", LoadSceneMode.Single);
    }
}
