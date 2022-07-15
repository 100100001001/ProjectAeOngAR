using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTest : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;

    // Update is called once per frame
    void Update()
    {
        itemNameText.text = TimerSlider.itemName;
    }
}
