using UnityEngine;

public class PlayerMusicAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    private const string WAIT_FOR_PARTITURE_TRIGGER = "Wait";
    private const string PLAYING_TRIGGER = "Playing";
    private const string SUCCESS_TRIGGER = "Success";
    private const string FAIL_TRIGGER = "Fail";
    private const string BACK_TO_IDLE_TRIGGER = "Idle";

    private void OnEnable()
    {
        MusicMinigameManager.OnWaitForPartition += MusicMinigameManager_OnWaitForPartition;
        MusicMinigameManager.OnPlayingMusic += MusicMinigameManager_OnPlayingMusic;
        MusicMinigameManager.OnMusicFail += MusicMinigameManager_OnMusicFail;
        MusicMinigameManager.OnMusicSuccess += MusicMinigameManager_OnMusicSuccess;
        MusicMinigameManager.OnMusicInterval += MusicMinigameManager_OnMusicInterval;
    }

    private void OnDisable()
    {
        MusicMinigameManager.OnWaitForPartition -= MusicMinigameManager_OnWaitForPartition;
        MusicMinigameManager.OnPlayingMusic -= MusicMinigameManager_OnPlayingMusic;
        MusicMinigameManager.OnMusicFail -= MusicMinigameManager_OnMusicFail;
        MusicMinigameManager.OnMusicSuccess -= MusicMinigameManager_OnMusicSuccess;
        MusicMinigameManager.OnMusicInterval -= MusicMinigameManager_OnMusicInterval;
    }

    private void WaitForPartiture()
    {
        animator.ResetTrigger(PLAYING_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.SetTrigger(WAIT_FOR_PARTITURE_TRIGGER);
    }

    private void Playing()
    {
        animator.ResetTrigger(WAIT_FOR_PARTITURE_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.SetTrigger(PLAYING_TRIGGER);
    }

    private void Success()
    {
        animator.ResetTrigger(WAIT_FOR_PARTITURE_TRIGGER);
        animator.ResetTrigger(PLAYING_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.SetTrigger(SUCCESS_TRIGGER);
    }

    private void Fail()
    {
        animator.ResetTrigger(WAIT_FOR_PARTITURE_TRIGGER);
        animator.ResetTrigger(PLAYING_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.SetTrigger(FAIL_TRIGGER);
    }

    private void BackToIdle()
    {
        animator.ResetTrigger(WAIT_FOR_PARTITURE_TRIGGER);
        animator.ResetTrigger(PLAYING_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.SetTrigger(BACK_TO_IDLE_TRIGGER);
    }

    private void MusicMinigameManager_OnWaitForPartition(object sender, System.EventArgs e)
    {
        WaitForPartiture();
    }

    private void MusicMinigameManager_OnPlayingMusic(object sender, System.EventArgs e)
    {
        Playing();
    }

    private void MusicMinigameManager_OnMusicSuccess(object sender, System.EventArgs e)
    {
        Success();
    }

    private void MusicMinigameManager_OnMusicFail(object sender, System.EventArgs e)
    {
        Fail();
    }

    private void MusicMinigameManager_OnMusicInterval(object sender, System.EventArgs e)
    {
        BackToIdle();
    }
}
