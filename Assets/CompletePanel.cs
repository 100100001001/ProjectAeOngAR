using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletePanel : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);

        
        if (GetInferenceFromModel.result == 1 || GetInferenceFromModel.result == 2 || GetInferenceFromModel.result == 3)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

        }
        else if (GetInferenceFromModel.result == 3 || GetInferenceFromModel.result == 4)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

    }
}
