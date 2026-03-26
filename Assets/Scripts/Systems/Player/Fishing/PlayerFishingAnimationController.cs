using UnityEngine;

public class PlayerFishingAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    private const string WAIT_FOR_FISH_TRIGGER = "Wait";
    private const string WARNING_TRIGGER = "Warning";
    private const string PULLING_ROD_TRIGGER = "Pulling";
    private const string SUCCESS_TRIGGER = "Success";
    private const string FAIL_TRIGGER = "Fail";
    private const string BACK_TO_IDLE_TRIGGER = "Idle";

    private void OnEnable()
    {
        FishingManager.OnWaitForFish += FishingManager_OnWaitForFish;
        FishingManager.OnPullingRod += FishingManager_OnPullingRod;
        FishingManager.OnFishingFail += FishingManager_OnFishingFail;
        FishingManager.OnFishingSuccess += FishingManager_OnFishingSuccess;
        FishingManager.OnFishingInterval += FishingManager_OnFishingInterval;

        FishingUI.OnPlayStart += FishingUI_OnPlayStart;
    }

    private void OnDisable()
    {
        FishingManager.OnWaitForFish -= FishingManager_OnWaitForFish;
        FishingManager.OnPullingRod -= FishingManager_OnPullingRod;
        FishingManager.OnFishingFail -= FishingManager_OnFishingFail;
        FishingManager.OnFishingSuccess -= FishingManager_OnFishingSuccess;
        FishingManager.OnFishingInterval -= FishingManager_OnFishingInterval;

        FishingUI.OnPlayStart -= FishingUI_OnPlayStart;
    }

    private void WaitForFish()
    {
        animator.ResetTrigger(PULLING_ROD_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.ResetTrigger(WARNING_TRIGGER);
        animator.SetTrigger(WAIT_FOR_FISH_TRIGGER);
    }

    private void PullingRod()
    {
        animator.ResetTrigger(WAIT_FOR_FISH_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.ResetTrigger(WARNING_TRIGGER);
        animator.SetTrigger(PULLING_ROD_TRIGGER);
    }

    private void Success()
    {
        animator.ResetTrigger(WAIT_FOR_FISH_TRIGGER);
        animator.ResetTrigger(PULLING_ROD_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.ResetTrigger(WARNING_TRIGGER);
        animator.SetTrigger(SUCCESS_TRIGGER);
    }

    private void Fail()
    {
        animator.ResetTrigger(WAIT_FOR_FISH_TRIGGER);
        animator.ResetTrigger(PULLING_ROD_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.ResetTrigger(WARNING_TRIGGER);
        animator.SetTrigger(FAIL_TRIGGER);
    }

    private void BackToIdle()
    {
        animator.ResetTrigger(WAIT_FOR_FISH_TRIGGER);
        animator.ResetTrigger(PULLING_ROD_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(WARNING_TRIGGER);
        animator.SetTrigger(BACK_TO_IDLE_TRIGGER);
    }

    private void Warning()
    {
        animator.ResetTrigger(WAIT_FOR_FISH_TRIGGER);
        animator.ResetTrigger(PULLING_ROD_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.SetTrigger(WARNING_TRIGGER);
    }

    private void FishingManager_OnWaitForFish(object sender, System.EventArgs e)
    {
        WaitForFish();
    }

    private void FishingManager_OnPullingRod(object sender, System.EventArgs e)
    {
        Warning();
    }

    private void FishingManager_OnFishingSuccess(object sender, System.EventArgs e)
    {
        Success();
    }

    private void FishingManager_OnFishingFail(object sender, System.EventArgs e)
    {
        Fail();
    }

    private void FishingManager_OnFishingInterval(object sender, System.EventArgs e)
    {
        BackToIdle();
    }

    private void FishingUI_OnPlayStart(object sender, System.EventArgs e)
    {
        PullingRod();
    }
}
