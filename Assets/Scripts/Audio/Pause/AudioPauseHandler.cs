using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPauseHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<AudioSource> audioSourcesToPause;

    [Header("Debug")]
    [SerializeField] private bool debug;

    public static event EventHandler OnPauseAudio;
    public static event EventHandler OnResumeAudio;

    private void OnEnable()
    {
        PauseManager.OnGamePaused += PauseManager_OnGamePaused;
        PauseManager.OnGameResumed += PauseManager_OnGameResumed;
    }

    private void OnDisable()
    {
        PauseManager.OnGamePaused -= PauseManager_OnGamePaused;
        PauseManager.OnGameResumed -= PauseManager_OnGameResumed;
    }

    private void PauseSFXList()
    {
        foreach (AudioSource audioSource in audioSourcesToPause)
        {
            audioSource.Pause();
        }
    }

    private void UnpauseSFXList()
    {
        foreach (AudioSource audioSource in audioSourcesToPause)
        {
            audioSource.UnPause();
        }
    }
 
    private void PauseManager_OnGamePaused(object sender, System.EventArgs e)
    {
        PauseSFXList();
        OnPauseAudio?.Invoke(this, EventArgs.Empty);
    }

    private void PauseManager_OnGameResumed(object sender, System.EventArgs e)
    {
        UnpauseSFXList();
        OnResumeAudio?.Invoke(this, EventArgs.Empty);
    }
}
