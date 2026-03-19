using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class EventDetectorSlider : Slider, IBeginDragHandler, IEndDragHandler
{
    public event EventHandler OnDragStart;
    public event EventHandler OnDragEnd;
    public event EventHandler<OnPointerUpEventArgs> OnUpPointer;

    private bool isDragging = false;

    public class OnPointerUpEventArgs : EventArgs
    {
        public bool isDraggingWhilePointerUp;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDragStart?.Invoke(this, EventArgs.Empty);
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEnd?.Invoke(this, EventArgs.Empty);
        isDragging = false;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        OnUpPointer?.Invoke(this, new OnPointerUpEventArgs { isDraggingWhilePointerUp = isDragging});
    }
}