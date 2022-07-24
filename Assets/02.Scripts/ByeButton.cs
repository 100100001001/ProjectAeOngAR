using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ByeButton : MonoBehaviour
{
    public static bool bye = false;
    public GameObject endingPanel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        bye = true;
        endingPanel.SetActive(true);
    }
}