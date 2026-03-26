using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeavingUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform loomUIHolder;
    [SerializeField] private RectTransform refferenceRectTransform;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float startingTime;
    [SerializeField, Range(0f, 3f)] private float endingTime;

    [Header("Lists")]
    [SerializeField] private List<Transform> loomUIPrefabs;

    [Header("Runtime Filled")]
    [SerializeField] private LoomUI currentLoomUI;
    [SerializeField] private State state;

    public enum State { NotPlaying, Starting, Playing }

    private const string SHOW_TRIGGER = "Show";
    private const string SUCCESS_TRIGGER = "Success";
    private const string FAIL_TRIGGER = "Fail";

    public event EventHandler OnWeaveSuccess;
    public event EventHandler OnWeaveFail;

    private bool loomSuccess = false;
    private bool loomFail = false;

    private void OnEnable()
    {
        LoomUI.OnLoomSuccess += LoomUI_OnLoomSuccess;
        LoomUI.OnLoomFail += LoomUI_OnLoomFail;
    }

    private void OnDisable()
    {
        LoomUI.OnLoomSuccess -= LoomUI_OnLoomSuccess;
        LoomUI.OnLoomFail -= LoomUI_OnLoomFail;
    }

    public void StartWeavingGame()
    {
        StartCoroutine(WeavingUIGameCoroutine());
    }

    private IEnumerator WeavingUIGameCoroutine()
    {
        SetState(State.Starting);

        ClearLoomUI();
        CreateLoomUI(refferenceRectTransform);

        ShowUI();

        yield return new WaitForSeconds(startingTime);

        SetState(State.Playing);

        yield return new WaitUntil(() => loomSuccess || loomFail);

        SetState(State.NotPlaying);

        yield return new WaitForSeconds(endingTime);

        if (loomSuccess) Success();
        if (loomFail) Fail();

        loomSuccess = false; 
        loomFail = false;
    }

    private void CreateLoomUI(RectTransform refferenceRectTransform)
    {
        Transform chosenRandomLoomUI = GeneralUtilities.ChooseRandomElementFromList(loomUIPrefabs);
        Transform loomUITransform = Instantiate(chosenRandomLoomUI, loomUIHolder);

        if (loomUITransform == null) return;

        LoomUI loomUI = loomUITransform.GetComponent<LoomUI>();

        if(loomUI == null) return;

        loomUI.InitializeLoomUI(refferenceRectTransform, this);

        currentLoomUI = loomUI;
    }

    private void ClearLoomUI()
    {
        currentLoomUI = null;

        foreach(Transform child in loomUIHolder)
        {
            Destroy(child.gameObject);
        }
    }

    private void SetState(State state) => this.state = state;

    private void Fail()
    {
        OnWeaveFail?.Invoke(this, EventArgs.Empty);
        FailUI();
    }

    private void Success()
    {
        OnWeaveSuccess?.Invoke(this, EventArgs.Empty);
        SuccessUI();
    }

    public bool CanWeave() => state == State.Playing;

    #region Animations
    public void ShowUI()
    {
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void SuccessUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.SetTrigger(SUCCESS_TRIGGER);
    }

    public void FailUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.SetTrigger(FAIL_TRIGGER);
    }
    #endregion

    private void LoomUI_OnLoomSuccess(object sender, EventArgs e)
    {
        loomSuccess = true;
    }
    private void LoomUI_OnLoomFail(object sender, EventArgs e)
    {
        loomFail = true;
    }
}
