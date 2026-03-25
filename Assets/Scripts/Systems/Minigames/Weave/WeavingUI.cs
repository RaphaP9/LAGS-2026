using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeavingUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private MinigamesInput minigamesInput;
    [SerializeField] private Transform loomUIHolder;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float startingTime;

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


    public void StartWeavingGame()
    {
        StartCoroutine(WeavingUIGameCoroutine());
    }

    private IEnumerator WeavingUIGameCoroutine()
    {
        SetState(State.Starting);

        ClearLoomUI();
        CreateLoomUI();

        ShowUI();

        yield return new WaitForSeconds(startingTime);

        SetState(State.Playing);
    }

    private void CreateLoomUI()
    {
        Transform chosenRandomLoomUI = GeneralUtilities.ChooseRandomElementFromList(loomUIPrefabs);
        Transform loomUITransform = Instantiate(chosenRandomLoomUI, loomUIHolder);

        if (loomUITransform == null) return;

        LoomUI loomUI = loomUITransform.GetComponent<LoomUI>();

        currentLoomUI = loomUI;
    }

    private void ClearLoomUI()
    {
        currentLoomUI = null;

        foreach(Transform child in loomUIHolder)
        {
            Destroy(child);
        }
    }

    private void SetState(State state) => this.state = state;

    private void Fail()
    {
        OnWeaveSuccess?.Invoke(this, EventArgs.Empty);
        FailUI();
    }

    private void Success()
    {
        OnWeaveFail?.Invoke(this, EventArgs.Empty);
        SuccessUI();
    }


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
}
