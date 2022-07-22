using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void GoMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoFindItem()
    {
        SceneManager.LoadScene("MainCopy_FindColor");
    }
}
