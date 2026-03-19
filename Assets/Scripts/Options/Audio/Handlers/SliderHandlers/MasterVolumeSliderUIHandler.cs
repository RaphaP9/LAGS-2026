using UnityEngine;
using System;

public class MasterVolumeSliderUIHandler : VolumeSliderUIHandler
{
    public static event EventHandler OnMasterSliderDragEnd;
    public static event EventHandler OnMasterSliderPointerUp;

    protected override void OnEnable()
    {
        base.OnEnable();
        MasterVolumeManager.OnMasterVolumeManagerInitialized += MasterVolumeManager_OnMasterVolumeManagerInitialized;
        MasterVolumeManager.OnMasterVolumeChanged += MasterVolumeManager_OnMasterVolumeChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        MasterVolumeManager.OnMasterVolumeManagerInitialized -= MasterVolumeManager_OnMasterVolumeManagerInitialized;
        MasterVolumeManager.OnMasterVolumeChanged -= MasterVolumeManager_OnMasterVolumeChanged;
    }

    protected override VolumeManager GetVolumeManager() => MasterVolumeManager.Instance;
    protected override void OnDragEndMethod() => OnMasterSliderDragEnd?.Invoke(this, EventArgs.Empty);
    protected override void OnPointerUpMethod() => OnMasterSliderPointerUp?.Invoke(this, EventArgs.Empty);

    private void MasterVolumeManager_OnMasterVolumeManagerInitialized(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void MasterVolumeManager_OnMasterVolumeChanged(object sender, VolumeManager.OnVolumeChangedEventArgs e)
    {
        //UpdateVisual();
    }
}
