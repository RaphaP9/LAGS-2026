using UnityEngine;
using System.Collections.Generic;

public class DialogueNPCAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField] private List<DialogueSO> associatedDialogues;

    private const string IDLE_TRIGGER = "Idle";
    private const string DIALOGUE_TRIGGER = "Dialogue";

    private void OnEnable()
    {
        DialogueManager.OnDialogueBegin += DialogueManager_OnDialogueBegin;
        DialogueManager.OnDialogueEnd += DialogueManager_OnDialogueEnd;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueBegin -= DialogueManager_OnDialogueBegin;
        DialogueManager.OnDialogueEnd -= DialogueManager_OnDialogueEnd;
    }

    private void Idle()
    {
        animator.ResetTrigger(DIALOGUE_TRIGGER);
        animator.SetTrigger(IDLE_TRIGGER);
    }

    private void Dialogue()
    {
        animator.ResetTrigger(IDLE_TRIGGER);
        animator.SetTrigger(DIALOGUE_TRIGGER);
    }

    private bool DialogueIsAssociated(DialogueSO dialogueSO)
    {
        return associatedDialogues.Contains(dialogueSO);
    }

    private void DialogueManager_OnDialogueBegin(object sender, DialogueManager.OnDialogueEventArgs e)
    {
        if (!DialogueIsAssociated(e.dialogueSO)) return;
        Dialogue();
    }

    private void DialogueManager_OnDialogueEnd(object sender, DialogueManager.OnDialogueEventArgs e)
    {
        if (!DialogueIsAssociated(e.dialogueSO)) return;
        Idle();
    }
}
