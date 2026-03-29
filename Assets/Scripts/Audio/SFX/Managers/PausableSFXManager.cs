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

        TotoraCropHandler.OnAnyTotoraCropHarvested += TotoraCropHandler_OnAnyTotoraCropHarvested;

        MusicNoteUI.OnNotePlayed += MusicNoteUI_OnNotePlayed;

        FishingManager.OnFishingSuccess += FishingManager_OnFishingSuccess;
        FishingManager.OnFishingFail += FishingManager_OnFishingFail;
        FishingManager.OnWaitForFish += FishingManager_OnWaitForFish;
        FishingManager.OnPullingRod += FishingManager_OnPullingRod;

        LoomPointUI.OnPointWoven += LoomPointUI_OnPointWoven;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        TotoraCropHandler.OnAnyTotoraCropHarvested -= TotoraCropHandler_OnAnyTotoraCropHarvested;

        MusicNoteUI.OnNotePlayed -= MusicNoteUI_OnNotePlayed;

        FishingManager.OnFishingSuccess -= FishingManager_OnFishingSuccess;
        FishingManager.OnFishingFail -= FishingManager_OnFishingFail;
        FishingManager.OnWaitForFish -= FishingManager_OnWaitForFish;
        FishingManager.OnPullingRod -= FishingManager_OnPullingRod;

        LoomPointUI.OnPointWoven -= LoomPointUI_OnPointWoven;
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

    #region Harvesting
    private void TotoraCropHandler_OnAnyTotoraCropHarvested(object sender, TotoraCropHandler.OnTotoraCropEventArgs e)
    {
        PlaySound(SFXPool.playerHarvesting);
    }
    #endregion
    #region Music Minigame
    private void MusicNoteUI_OnNotePlayed(object sender, MusicNoteUI.OnNoteEventArgs e)
    {
        PlaySound(e.musicNoteSO.soundClip);
    }

    #endregion

    #region Fishing Minigame
    private void FishingManager_OnFishingSuccess(object sender, EventArgs e)
    {
        PlaySound(SFXPool.fishingSuccess);
    }

    private void FishingManager_OnFishingFail(object sender, EventArgs e)
    {
        PlaySound(SFXPool.fishingFail);
    }
    private void FishingManager_OnWaitForFish(object sender, EventArgs e)
    {
        PlaySound(SFXPool.throwFishingRod);
    }

    private void FishingManager_OnPullingRod(object sender, EventArgs e)
    {
        PlaySound(SFXPool.startFishing);
    }
    #endregion

    #region Weaving Minigame
    private void LoomPointUI_OnPointWoven(object sender, LoomPointUI.OnPointWovenEventArgs e)
    {
        PlaySound(SFXPool.sew);
    }
    #endregion
}
