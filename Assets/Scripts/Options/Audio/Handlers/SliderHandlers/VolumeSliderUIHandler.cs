using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class VolumeSliderUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] protected EventDetectorSlider eventDetectorSlider;

    protected virtual void OnEnable()
    {
        eventDetectorSlider.OnDragEnd += EventDetectorSlider_OnDragEnd;
        eventDetectorSlider.OnUpPointer += EventDetectorSlider_OnUpPointer;
    }

    protected virtual void OnDisable()
    {
        eventDetectorSlider.OnDragEnd -= EventDetectorSlider_OnDragEnd;
        eventDetectorSlider.OnUpPointer -= EventDetectorSlider_OnUpPointer;
    }

    private void Awake()
    {
        InitializeSliderListeners();
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void InitializeSliderListeners()
    {
        eventDetectorSlider.onValueChanged.AddListener(ChangeVolume);
    }

    protected abstract VolumeManager GetVolumeManager();

    private void ChangeVolume(float sliderValue)  => GetVolumeManager().ChangeVolume(sliderValue, false); //Do not save to player prefs: onValueChange performs continuously (not ideal for saving operations)

    protected void UpdateVisual()
    {
        float currentVolume = GetVolumeManager().GetLinearVolume();
        eventDetectorSlider.SetValueWithoutNotify(currentVolume);
    }

    protected abstract void OnDragEndMethod();
    protected abstract void OnPointerUpMethod();

    #region Subscriptions
    private void EventDetectorSlider_OnDragEnd(object sender, EventArgs e)
    {
        GetVolumeManager().SaveVolumePlayerPrefs(eventDetectorSlider.value);
        OnDragEndMethod();
    }

    private void EventDetectorSlider_OnUpPointer(object sender, EventDetectorSlider.OnPointerUpEventArgs e)
    {
        if (e.isDraggingWhilePointerUp) return; //If was dragging while pointer up, OnDragEnd will be executed and this method (EventDetectorSlider_OnUpPointer) is redundant

        GetVolumeManager().SaveVolumePlayerPrefs(eventDetectorSlider.value);
        OnPointerUpMethod();
    }
    #endregion
}
