using UnityEngine;

public class PlayerWeavingAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    private const string WAIT_FOR_LOOM_TRIGGER = "Wait";
    private const string WEAVING_TRIGGER = "Weaving";
    private const string SUCCESS_TRIGGER = "Success";
    private const string FAIL_TRIGGER = "Fail";
    private const string BACK_TO_IDLE_TRIGGER = "Idle";

    private void OnEnable()
    {
        WeavingManager.OnWaitForLoom += WeavingManager_OnWaitForLoom;
        WeavingManager.OnWeaving += WeavingManager_OnWeaving;
        WeavingManager.OnWeaveFail += WeavingManager_OnWeaveFail;
        WeavingManager.OnWeaveSuccess += WeavingManager_OnWeaveSuccess;
        WeavingManager.OnWeaveInterval += WeavingManager_OnWeaveInterval;
    }

    private void OnDisable()
    {
        WeavingManager.OnWaitForLoom -= WeavingManager_OnWaitForLoom;
        WeavingManager.OnWeaving -= WeavingManager_OnWeaving;
        WeavingManager.OnWeaveFail -= WeavingManager_OnWeaveFail;
        WeavingManager.OnWeaveSuccess -= WeavingManager_OnWeaveSuccess;
        WeavingManager.OnWeaveInterval -= WeavingManager_OnWeaveInterval;
    }

    private void WaitForLoom()
    {
        animator.ResetTrigger(WEAVING_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.SetTrigger(WAIT_FOR_LOOM_TRIGGER);
    }

    private void Weaving()
    {
        animator.ResetTrigger(WAIT_FOR_LOOM_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.SetTrigger(WEAVING_TRIGGER);
    }

    private void Success()
    {
        animator.ResetTrigger(WAIT_FOR_LOOM_TRIGGER);
        animator.ResetTrigger(WEAVING_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.SetTrigger(SUCCESS_TRIGGER);
    }

    private void Fail()
    {
        animator.ResetTrigger(WAIT_FOR_LOOM_TRIGGER);
        animator.ResetTrigger(WEAVING_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(BACK_TO_IDLE_TRIGGER);
        animator.SetTrigger(FAIL_TRIGGER);
    }

    private void BackToIdle()
    {
        animator.ResetTrigger(WAIT_FOR_LOOM_TRIGGER);
        animator.ResetTrigger(WEAVING_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.SetTrigger(BACK_TO_IDLE_TRIGGER);
    }

    private void WeavingManager_OnWaitForLoom(object sender, System.EventArgs e)
    {
        WaitForLoom();
    }

    private void WeavingManager_OnWeaving(object sender, System.EventArgs e)
    {
        Weaving();
    }

    private void WeavingManager_OnWeaveSuccess(object sender, System.EventArgs e)
    {
        Success();
    }

    private void WeavingManager_OnWeaveFail(object sender, System.EventArgs e)
    {
        Fail();
    }

    private void WeavingManager_OnWeaveInterval(object sender, System.EventArgs e)
    {
        BackToIdle();
    }
}
