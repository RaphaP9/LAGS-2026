using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenesVolumeManager : VolumeManager
{
    public static CutscenesVolumeManager Instance { get; private set; }

    private const string CUTSCENES_VOLUME = "CutscenesVolume";

    public static event EventHandler OnCutscenesVolumeManagerInitialized;
    public static event EventHandler<OnVolumeChangedEventArgs> OnCutscenesVolumeChanged;

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one CutscenesVolumeManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    protected override string GetVolumePropertyName() => CUTSCENES_VOLUME;
    protected override void OnVolumeManagerInitialized(float volume)
    {
        OnCutscenesVolumeManagerInitialized?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnVolumeChanged(float volume)
    {
        OnCutscenesVolumeChanged?.Invoke(this, new OnVolumeChangedEventArgs { newVolume = volume });
    }
}
