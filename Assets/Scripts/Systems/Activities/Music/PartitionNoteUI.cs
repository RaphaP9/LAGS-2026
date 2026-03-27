using System;
using TMPro;
using UnityEngine;

public class PartitionNoteUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MusicNoteSO musicNoteSO;
    [SerializeField] private TextMeshProUGUI noteNameText;
    [SerializeField] private Animator animator;

    private const string FAIL_TRIGGER = "Fail";
    private const string SUCCESS_TRIGGER = "Success";

    public MusicNoteSO MusicNoteSO => musicNoteSO;

    public static event EventHandler<OnPartitionNoteEventArgs> OnPartitionNoteSuccess;
    public static event EventHandler<OnPartitionNoteEventArgs> OnPartitionNoteFail;

    public class OnPartitionNoteEventArgs : EventArgs
    {
        public MusicNoteSO musicNoteSO;
    }

    private void Start()
    {
        InitializePartitionNoteUI();
    }

    private void InitializePartitionNoteUI()
    {
        noteNameText.text = musicNoteSO.noteName;
    }

    private void FailUI()
    {
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.SetTrigger(FAIL_TRIGGER);
    }

    private void SuccessUI()
    {
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.SetTrigger(SUCCESS_TRIGGER);
    }

    public void FailPartitionNote()
    {
        OnPartitionNoteFail?.Invoke(this, new OnPartitionNoteEventArgs { musicNoteSO = musicNoteSO });
        FailUI();
    }

    public void SuccessPartitionNote() 
    {
        OnPartitionNoteSuccess?.Invoke(this, new OnPartitionNoteEventArgs { musicNoteSO = musicNoteSO });
        SuccessUI(); 
    }
}
