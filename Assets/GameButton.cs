using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    void Replay()
    {
        SceneManager.LoadScene("MainCopy_Game_Bomb");

    }

    public void GoToTheMain()
    {
        SceneManager.LoadScene("Main");
    }
}
