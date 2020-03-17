using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public void Resume()
    {
        gameObject.GetComponent<GameController>().SwitchPauseState();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
