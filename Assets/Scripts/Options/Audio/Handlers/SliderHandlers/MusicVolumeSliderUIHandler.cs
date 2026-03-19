using UnityEngine;
using System;

public class MusicVolumeSliderUIHandler : VolumeSliderUIHandler
{
    public static event EventHandler OnMusicSliderDragEnd;
    public static event EventHandler OnMusicSliderPointerUp;

    protected override void OnEnable()
    {
        base.OnEnable();
        MusicVolumeManager.OnMusicVolumeManagerInitialized += MusicVolumeManager_OnMusicVolumeManagerInitialized;
        MusicVolumeManager.OnMusicVolumeChanged += MusicVolumeManager_OnMusicVolumeChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        MusicVolumeManager.OnMusicVolumeManagerInitialized -= MusicVolumeManager_OnMusicVolumeManagerInitialized;
        MusicVolumeManager.OnMusicVolumeChanged -= MusicVolumeManager_OnMusicVolumeChanged;
    }

    protected override VolumeManager GetVolumeManager() => MusicVolumeManager.Instance;
    protected override void OnDragEndMethod() => OnMusicSliderDragEnd?.Invoke(this, EventArgs.Empty);
    protected override void OnPointerUpMethod() => OnMusicSliderPointerUp?.Invoke(this, EventArgs.Empty);

    private void MusicVolumeManager_OnMusicVolumeManagerInitialized(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void MusicVolumeManager_OnMusicVolumeChanged(object sender, VolumeManager.OnVolumeChangedEventArgs e)
    {
        //UpdateVisual();
    }
}
