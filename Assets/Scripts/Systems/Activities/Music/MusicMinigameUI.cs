using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMinigameUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform partitionUIHolder;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float startingTime;
    [SerializeField, Range(0f, 3f)] private float endingTime;

    [Header("Lists")]
    [SerializeField] private List<Transform> partitionUIPrefabs;

    [Header("Runtime Filled")]
    [SerializeField] private PartitionUI currentPartitionUI;
    [SerializeField] private State state;

    public enum State { NotPlaying, Starting, Playing }

    private const string SHOW_TRIGGER = "Show";
    private const string SUCCESS_TRIGGER = "Success";
    private const string FAIL_TRIGGER = "Fail";

    public event EventHandler OnMusicSuccess;
    public event EventHandler OnMusicFail;

    private bool partitionSuccess = false;
    private bool partitionFail = false;

    private void OnEnable()
    {
        PartitionUI.OnPartitionSuccess += PartitionUI_OnPartitionSuccess;
        PartitionUI.OnPartitionFail += PartitionUI_OnPartitionFail;
    }

    private void OnDisable()
    {
        PartitionUI.OnPartitionSuccess -= PartitionUI_OnPartitionSuccess;
        PartitionUI.OnPartitionFail -= PartitionUI_OnPartitionFail;
    }

    public void StartMusicGame()
    {
        StartCoroutine(MusicUIGameCoroutine());
    }

    private IEnumerator MusicUIGameCoroutine()
    {
        SetState(State.Starting);

        ClearPartitionUI();
        CreatePartitionUI();

        ShowUI();

        yield return new WaitForSeconds(startingTime);

        SetState(State.Playing);

        yield return new WaitUntil(() => partitionSuccess || partitionFail);

        SetState(State.NotPlaying);

        yield return new WaitForSeconds(endingTime);

        if (partitionSuccess) Success();
        if (partitionFail) Fail();

        partitionSuccess = false;
        partitionFail = false;
    }

    private void CreatePartitionUI()
    {
        Transform chosenRandomPartitionUI = GeneralUtilities.ChooseRandomElementFromList(partitionUIPrefabs);
        Transform partitionUITransform = Instantiate(chosenRandomPartitionUI, partitionUIHolder);

        if (partitionUITransform == null) return;

        PartitionUI partitionUI = partitionUITransform.GetComponent<PartitionUI>();

        if (partitionUI == null) return;

        //partitionUI.InitializePartitionUI(refferenceRectTransform, this);

        currentPartitionUI = partitionUI;
    }

    private void ClearPartitionUI()
    {
        currentPartitionUI = null;

        foreach (Transform child in partitionUIHolder)
        {
            Destroy(child.gameObject);
        }
    }

    private void SetState(State state) => this.state = state;

    private void Fail()
    {
        OnMusicFail?.Invoke(this, EventArgs.Empty);
        FailUI();
    }

    private void Success()
    {
        OnMusicSuccess?.Invoke(this, EventArgs.Empty);
        SuccessUI();
    }

    public bool CanPlayNote() => state == State.Playing;

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

    private void PartitionUI_OnPartitionSuccess(object sender, EventArgs e)
    {
        partitionSuccess = true;
    }
    private void PartitionUI_OnPartitionFail(object sender, EventArgs e)
    {
        partitionFail = true;
    }
}
