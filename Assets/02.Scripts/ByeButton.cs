using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ByeButton : MonoBehaviour
{
    public static bool bye = false;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        bye = true;
        SceneManager.LoadScene("Main");
    }
}