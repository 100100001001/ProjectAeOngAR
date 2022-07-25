using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    public GameObject shootScript;

    public void Replay()
    {
        shootScript.GetComponent<PlayShootingGame>().enabled = false;
        shootScript.GetComponent<PlayShootingGame>().enabled = true;

        Time.timeScale = 1f;
    }

    public void GoToTheMain()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
}
