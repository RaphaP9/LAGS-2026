using UnityEngine;

public class MusicMinigameKeybindIndicatorUI : MinigameKeybindIndicatorUI
{
    private void OnEnable()
    {
        MusicMinigameManager.OnPlayingMusic += MusicMinigameManager_OnPlayingMusic;
        PartitionUI.OnPartitionFail += PartitionUI_OnPartitionFail;
        PartitionUI.OnPartitionSuccess += PartitionUI_OnPartitionSuccess;
    }

    private void OnDisable()
    {
        MusicMinigameManager.OnPlayingMusic -= MusicMinigameManager_OnPlayingMusic;
        PartitionUI.OnPartitionFail -= PartitionUI_OnPartitionFail;
        PartitionUI.OnPartitionSuccess -= PartitionUI_OnPartitionSuccess;
    }

    private void MusicMinigameManager_OnPlayingMusic(object sender, System.EventArgs e)
    {
        Show();
    }

    private void PartitionUI_OnPartitionFail(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void PartitionUI_OnPartitionSuccess(object sender, System.EventArgs e)
    {
        Hide();
    }
}
