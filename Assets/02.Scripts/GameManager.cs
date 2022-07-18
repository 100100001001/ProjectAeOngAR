using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuPanel;

    public void UIControl(string type)
    {
        // �޴� ��ư�� ������ ��

        switch (type)
        {
            case "menuOn":
                menuPanel.SetActive(true);
                Time.timeScale = 0f;
                break;
            case "menuOff":
                menuPanel.SetActive(false);
                Time.timeScale = 1f;
                break;
            case "exit":
                Application.Quit();
                break;
        }
    }

}
