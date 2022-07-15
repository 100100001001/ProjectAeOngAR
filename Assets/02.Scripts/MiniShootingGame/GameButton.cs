using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    public GameObject shootScript;

    public void Replay()
    {
        shootScript.GetComponent<TimerSlider>().enabled = false;
        shootScript.GetComponent<TimerSlider>().enabled = true;
    }

    public void GoToTheMain()
    {
        StatusBar.instance.ActiveValue(true, 20);
        SceneManager.LoadScene("Main");
    }
}
