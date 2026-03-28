using UnityEngine;

public class WeavingMinigameKeybingIndicatorUI : MinigameKeybindIndicatorUI
{
    private void OnEnable()
    {
        WeavingManager.OnWeaving += WeavingManager_OnWeaving;
        LoomUI.OnLoomFail += LoomUI_OnLoomFail;
        LoomUI.OnLoomSuccess += LoomUI_OnLoomSuccess;
    }

    private void OnDisable()
    {
        WeavingManager.OnWeaving -= WeavingManager_OnWeaving;
        LoomUI.OnLoomFail -= LoomUI_OnLoomFail;
        LoomUI.OnLoomSuccess -= LoomUI_OnLoomSuccess;
    }

    private void WeavingManager_OnWeaving(object sender, System.EventArgs e)
    {
        Show();
    }

    private void LoomUI_OnLoomFail(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void LoomUI_OnLoomSuccess(object sender, System.EventArgs e)
    {
        Hide();
    }
}
