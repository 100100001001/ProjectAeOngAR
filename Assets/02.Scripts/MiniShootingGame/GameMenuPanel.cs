using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuPanel : MonoBehaviour
{
    public void MenuOn()
    {
        Time.timeScale = 0f;
    }

    public void MenuOff()
    {
        Time.timeScale = 1f;

    }

}
