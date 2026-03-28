using UnityEngine;

public class FishingMinigameKeybindIndicatorUI : MinigameKeybindIndicatorUI
{
    private void OnEnable()
    {
        FishingManager.OnPullingRod += FishingManager_OnPullingRod;
        FishingUI.OnAllTiltsSuccess += FishingUI_OnAllTiltsSuccess;
        FishingUI.OnTiltFail += FishingUI_OnTiltFail;
    }

    private void OnDisable()
    {
        FishingManager.OnPullingRod -= FishingManager_OnPullingRod;
        FishingUI.OnAllTiltsSuccess -= FishingUI_OnAllTiltsSuccess;
        FishingUI.OnTiltFail -= FishingUI_OnTiltFail;
    }

    private void FishingManager_OnPullingRod(object sender, System.EventArgs e)
    {
        Show();
    }

    private void FishingUI_OnAllTiltsSuccess(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void FishingUI_OnTiltFail(object sender, System.EventArgs e)
    {
        Hide();
    }
}
