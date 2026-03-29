using UnityEngine;

public class PullingFishSFXManager : CustomSFXManager
{
    private void OnEnable()
    {
        FishingUI.OnPlayStart += FishingUI_OnPlayStart;
        FishingUI.OnTiltFail += FishingUI_OnTiltFail;
        FishingUI.OnAllTiltsSuccess += FishingUI_OnAllTiltsSuccess;
    }

    private void OnDisable()
    {
        FishingUI.OnPlayStart -= FishingUI_OnPlayStart;
        FishingUI.OnTiltFail -= FishingUI_OnTiltFail;
        FishingUI.OnAllTiltsSuccess -= FishingUI_OnAllTiltsSuccess;
    }

    private void FishingUI_OnPlayStart(object sender, System.EventArgs e)
    {
        ReplaceAudioClip(SFXPool.pullingFish);
    }

    private void FishingUI_OnTiltFail(object sender, System.EventArgs e)
    {
        StopAudioSource();
    }

    private void FishingUI_OnAllTiltsSuccess(object sender, System.EventArgs e)
    {
        StopAudioSource();
    }
}
