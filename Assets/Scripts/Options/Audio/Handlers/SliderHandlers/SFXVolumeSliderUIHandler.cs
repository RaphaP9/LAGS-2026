using System;
using UnityEngine;

public class SFXVolumeSliderUIHandler : VolumeSliderUIHandler
{
    public static event EventHandler OnSFXSliderDragEnd;
    public static event EventHandler OnSFXSliderPointerUp;

    protected override void OnEnable()
    {
        base.OnEnable();
        SFXVolumeManager.OnSFXVolumeManagerInitialized += SFXVolumeManager_OnSFXVolumeManagerInitialized;
        SFXVolumeManager.OnSFXVolumeChanged += SFXVolumeManager_OnSFXVolumeChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        SFXVolumeManager.OnSFXVolumeManagerInitialized -= SFXVolumeManager_OnSFXVolumeManagerInitialized;
        SFXVolumeManager.OnSFXVolumeChanged -= SFXVolumeManager_OnSFXVolumeChanged;
    }

    protected override VolumeManager GetVolumeManager() => SFXVolumeManager.Instance;
    protected override void OnDragEndMethod() => OnSFXSliderDragEnd?.Invoke(this, EventArgs.Empty); 
    protected override void OnPointerUpMethod() => OnSFXSliderPointerUp?.Invoke(this, EventArgs.Empty);

    private void SFXVolumeManager_OnSFXVolumeManagerInitialized(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void SFXVolumeManager_OnSFXVolumeChanged(object sender, VolumeManager.OnVolumeChangedEventArgs e)
    {
        //UpdateVisual();
    }
}
