using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCastom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action onClick;
    bool isClick = false;

    private void Update()
    {
        if (isClick) { onClick?.Invoke(); }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClick = false;
    }
}
