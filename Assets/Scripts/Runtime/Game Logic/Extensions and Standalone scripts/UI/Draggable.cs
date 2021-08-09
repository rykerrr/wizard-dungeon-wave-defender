using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private RectTransform thisTransform;
    private Vector2 delta;

    private void Awake()
    {
        thisTransform = (RectTransform)transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        delta = (Vector2)thisTransform.position - eventData.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        var mousePos = Mouse.current.position.ReadValue();
        
        thisTransform.position = mousePos + delta;
    }
}
