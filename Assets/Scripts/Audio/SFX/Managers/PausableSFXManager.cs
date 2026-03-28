using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableSFXManager : SFXManager
{
    public static PausableSFXManager Instance { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        MusicNoteUI.OnNotePlayed += MusicNoteUI_OnNotePlayed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        MusicNoteUI.OnNotePlayed -= MusicNoteUI_OnNotePlayed;
    }

    #region Singleton Settings
    protected override void Awake()
    {
        base.Awake();
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
            //Debug.LogWarning("There is more than one AudioManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }
    #endregion

    #region Music Minigame
    private void MusicNoteUI_OnNotePlayed(object sender, MusicNoteUI.OnNoteEventArgs e)
    {
        PlaySound(e.musicNoteSO.soundClip);
    }

    #endregion
}
