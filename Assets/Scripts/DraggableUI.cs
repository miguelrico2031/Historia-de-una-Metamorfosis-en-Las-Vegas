
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IDragHandler
{
    private Canvas _canvas;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
    }
    
    
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("hola drag");
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
}
