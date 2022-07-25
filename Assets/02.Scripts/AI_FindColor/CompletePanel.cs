using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletePanel : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        
        if (GetInferenceFromModel.instance.prediction.predictedValue == 0 || GetInferenceFromModel.instance.prediction.predictedValue == 1 || GetInferenceFromModel.instance.prediction.predictedValue == 2)
        //if (GetInferenceFromModel.resultValue == 0 || GetInferenceFromModel.resultValue == 1 || GetInferenceFromModel.resultValue == 2)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else gameObject.transform.GetChild(1).gameObject.SetActive(true);

    }
}
