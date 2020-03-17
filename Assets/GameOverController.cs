using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameObject victoryText, defeatText;

    public void Init(bool victory)
    {
        victoryText.SetActive(victory);
        defeatText.SetActive(!victory);
    }
}
