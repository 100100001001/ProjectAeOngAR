using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
}
